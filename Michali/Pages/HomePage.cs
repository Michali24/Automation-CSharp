using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moriya.Pages
{
    public class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void GoToRegistrationPage(IWebDriver driver)
        {
            driver.Navigate().Refresh();

            driver.FindElement(By.Id("signin2")).Click();
        }

        public void GoToLoginPage(IWebDriver driver)
        {
            driver.Navigate().Refresh();

            driver.FindElement(By.Id("login2")).Click();
        }

        public void SelectCategory(string category)
        {

            driver.Navigate().Refresh();
            driver.FindElement(By.XPath(string.Format("//a[text()='{0}']", category))).Click();

        }


        public bool IsLoggedIn(string username)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("nameofuser")));
            return driver.FindElement(By.Id("nameofuser")).Text.Contains(username);
        }

        public void GoToProductPage(string productTitle)
        {
            driver.Navigate().Refresh();

            driver.FindElement(By.LinkText(productTitle)).Click();

        }

        public void addProductToCart()
        {
            driver.Navigate().Refresh();

            driver.FindElement(By.LinkText("Add to cart")).Click();

        }

        public void GoToCartPage()
        {
            driver.FindElement(By.Id("cartur")).Click();
        }

        public void ClickOnLogOut() 
        {
            driver.FindElement(By.Id("logout2")).Click();
        }
    }
}
