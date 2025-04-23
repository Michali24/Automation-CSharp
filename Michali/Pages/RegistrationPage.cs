using Moriya.Tests;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moriya.Pages
{
    public class RegistrationPage
    {
        private IWebDriver driver;

        public RegistrationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Register(string userName, string password)
        {
            driver.FindElement(By.Id("sign-username")).SendKeys(userName);
            driver.FindElement(By.Id("sign-password")).SendKeys(password);

            driver.FindElement(By.XPath("//button[text() = 'Sign up']")).Click();

        }

        public string VerifyMessageAfterSignup(IWebDriver driver)
        {
            string alertText = null;

            try
            {

                if (FunctionLibrary.IsAlertShown(driver))
                    alertText = FunctionLibrary.CheckAlertMessage(driver);

                switch (alertText)
                {
                    case "Sign up successful.":
                        FunctionLibrary.ClickAlert(driver, "אישור");
                        return alertText;

                    case "This user already exist.":
                        FunctionLibrary.ClickAlert(driver, "אישור");
                        driver.FindElement(By.Id("sign-username")).Clear();
                        driver.FindElement(By.Id("sign-password")).Clear();
                        return alertText;

                    case "Please fill out Username and Password.":
                        FunctionLibrary.ClickAlert(driver, "אישור");
                        return alertText;

                    default:
                        Console.WriteLine("  size!");
                        return alertText;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }
}
