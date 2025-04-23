using Moriya.Tests;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moriya.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;
        public IWebElement loginusername;
        public IWebElement loginpassword;
        public IWebElement Loginbutton; //כפתור לוגין בתפריט עליון בדף הבית
        public string alertText;
        public IWebElement loginBtn; //כפתור לוגין בתוך ההודעה קופצת

        FunctionLibrary functionLibrary = new FunctionLibrary();


        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            loginusername = driver.FindElement(By.Id("loginusername"));
            loginpassword = driver.FindElement(By.Id("loginpassword"));
            loginBtn = driver.FindElement(By.CssSelector("button[onclick='logIn()']"));
        }

        public void login(string userName, string password)
        {
            loginusername.Clear();
            loginpassword.Clear();
            loginusername.SendKeys(userName);
            loginpassword.SendKeys(password);
            loginBtn.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public bool LoginValidationTest(IWebDriver driver, string userName)
        {
            try
            {
                string wrongPassword = "!@#$%^&*()!@#$%^&*()";
                loginusername.Clear();
                loginpassword.Clear();
                loginusername.SendKeys(userName);
                loginpassword.SendKeys(wrongPassword);
                loginBtn.Click();


                if (FunctionLibrary.IsAlertShown(driver))
                {
                    alertText = FunctionLibrary.CheckAlertMessage(driver);

                    switch (alertText)
                    {

                        case "User does not exist.":
                            FunctionLibrary.ClickAlert(driver, "אישור");
                            alertText = FunctionLibrary.CheckAlertMessage(driver);
                            Console.WriteLine(alertText);
                            return false;

                        case "Wrong password.":
                            FunctionLibrary.ClickAlert(driver, "אישור");
                            Console.WriteLine(alertText);
                            return true;

                        case "Please fill out Username and Password.":
                            FunctionLibrary.ClickAlert(driver, "אישור");
                            Console.WriteLine(alertText);

                            return true;

                        default:
                            Console.WriteLine("hi!");
                            return false;
                    }

                }
                return false;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;

            }
        }



    }
}
