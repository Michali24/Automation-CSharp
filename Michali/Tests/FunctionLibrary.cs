using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moriya.Tests
{
    public class FunctionLibrary
    {
        public static bool IsAlertShown(IWebDriver driver)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

                driver.SwitchTo().Alert();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static string CheckAlertMessage(IWebDriver driver)
        {
            string text;
            try
            {
                IAlert alert;
                Thread.Sleep(1000);

                alert = driver.SwitchTo().Alert();
                text = alert.Text;

                return text;

            }
            catch (Exception e)
            {

                return e.Message;
            }
        }
        public static bool ClickAlert(IWebDriver driver, string type)
        {
            IAlert alert;
            alert = driver.SwitchTo().Alert();
            if (type == "אישור")
            {
                alert.Accept();
                return true;
            }

            else if (type == "Cancel")
            {
                alert.Dismiss();
                return true;
            }
            return false;
        }


    }
}
