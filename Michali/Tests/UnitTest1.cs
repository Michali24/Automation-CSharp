using Moriya.Pages;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Moriya.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        private string url = "https://www.demoblaze.com/";

        string existUserName = "michali";
        string userName = "michali" + DateTime.Now;
        string password = "1234";
        string productTitle = "Sony vaio i5";
        string productPrice = "790";



        string alertText = null;


        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver(@"C:\Users\gavri\Downloads\chromedriver-win64");

            //driver = new ChromeDriver(@"C:\ProgramData");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        [TestMethod]
        [DataRow(DisplayName = "Sanity")]

        public void TestScenario()
        {
            RegistrationTest();
            LoginTestValidation();
            LoginTest();
            AddToCart();
            PlaceOrder();

        }



        [DataRow(DisplayName = "Sign Up")]
        public void RegistrationTest()
        {
            HomePage homePage = new HomePage(driver);
            homePage.GoToRegistrationPage(driver);

            RegistrationPage registrationPage = new RegistrationPage(driver);

            registrationPage.Register(existUserName, password);
            alertText = registrationPage.VerifyMessageAfterSignup(driver);
            Assert.AreNotEqual("Sign up successful.", alertText);

            registrationPage.Register("", password);
            alertText = registrationPage.VerifyMessageAfterSignup(driver);
            Assert.AreNotEqual("Sign up successful.", alertText);

            registrationPage.Register(userName, password);
            alertText = registrationPage.VerifyMessageAfterSignup(driver);
            Assert.AreEqual("Sign up successful.", alertText, "Registration failed.");
            Console.WriteLine(userName);

        }

        public void LoginTestValidation()
        {
            var homePage = new HomePage(driver);
            homePage.GoToLoginPage(driver);
            var loginPage = new LoginPage(driver);
            Assert.IsTrue(loginPage.LoginValidationTest(driver, userName));
        }

        public void LoginTest()
        {
            HomePage homePage = new HomePage(driver);
            var loginPage = new LoginPage(driver);
            loginPage.login(existUserName, password);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);


            //Assert.IsTrue(homePage.IsLoggedIn(userName), "Login failed.");

        }

        public void AddToCart()
        {
            string productAddedExpectedMsg = "Product added.";
            string alert1 = null;

            HomePage homePage = new HomePage(driver);
            homePage.SelectCategory("Laptops");
            homePage.GoToProductPage(productTitle);
            homePage.addProductToCart();



            if (FunctionLibrary.IsAlertShown(driver))
                alert1 = FunctionLibrary.CheckAlertMessage(driver);
            if (alert1.Equals(productAddedExpectedMsg))
            {
                FunctionLibrary.ClickAlert(driver, "אישור");
                Console.WriteLine("Product added");
            }
            CartPage cartPage = new CartPage(driver);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            homePage.GoToCartPage();

            //Assert.IsTrue(cartPage.IsProductInCart(productTitle), "Product not found in cart.");

        }

        public void PlaceOrder()
        {
            CartPage cartPage = new CartPage(driver);
            PaymentPage paymentPage = new PaymentPage(driver);
            HomePage homePage = new HomePage(driver);

            //if (cartPage.VerifyProductHasBeenAddedToCart(driver, productTitle, productPrice))
            //    Console.WriteLine("Product added to cart successfully.");

            cartPage.ClickOnPlaceOrder(driver);
            paymentPage.FillForm(driver);
            paymentPage.ClickOnPurchaseBtn(driver);
            paymentPage.ValidatePurchase(driver);
            Assert.IsTrue(paymentPage.PrintId(driver));
            paymentPage.ClickOkButton(driver);
            homePage.ClickOnLogOut();


        }


        //[TestCleanup]
        //public void Teardown() {
        //    driver.Quit();
        //}

    }
}