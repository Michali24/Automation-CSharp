using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moriya.Pages
{
    public class PaymentPage
    {
        IWebDriver driver;
        string name = "michali";
        string country = "Israel";
        string city = "PT";
        string card = "452673382";
        string month = "02";
        string year = "2025";

        public PaymentPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void FillForm(IWebDriver driver)
        {
            driver.FindElement(By.Id("name")).SendKeys(name);
            driver.FindElement(By.Id("country")).SendKeys(country);
            driver.FindElement(By.Id("city")).SendKeys(city);
            driver.FindElement(By.Id("card")).SendKeys(card);
            driver.FindElement(By.Id("month")).SendKeys(month);
            driver.FindElement(By.Id("year")).SendKeys(year);
        }

        public void ClickOkButton(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@class = 'confirm btn btn-lg btn-primary']")).Click();

        }

        public void ClickOnPurchaseBtn(IWebDriver driver)
        {
            //driver.Navigate().Refresh();

            driver.FindElement(By.CssSelector("button[onclick='purchaseOrder()']")).Click();
        }

        public bool PrintId(IWebDriver driver)
        {
            try
            {
                string text = driver.FindElement(By.XPath("//*[@class = 'lead text-muted ']")).Text;



                int startPos = text.IndexOf("Id");
                int length = text.IndexOf("\r");
                string sub = text.Substring(startPos, length);


                Console.WriteLine("Order number: " + sub);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public bool ValidatePurchase(IWebDriver driver)
        {
            try
            {
                if (CheckTitle(driver))
                    if (CheckData(driver, name, card)) //בדיקת שם וכרטיס אשראי
                        return true;
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool CheckData(IWebDriver driver, string name, string card)
        {
            try
            {
                string text = driver.FindElement(By.XPath("//*[@class = 'lead text-muted ']")).Text;
                //text = "Id: 7139039\r\nAmount: 790 USD\r\nCard Number: 1234567890\r\nName: Dvora\r\nDate: 23/2/2025"
                text = text.Replace("\n", "").Replace("\r", ""); //מאפשרת להחליף תת-מחרוזת בתוך מחרוזת שלמה בערך אחר Replace פונקצית 

                //text = "Id: 7139039Amount: 790 USDCard Number: 1234567890Name: DvoraDate: 23/2/2025"
                if (text.Contains(name) && text.Contains(card))
                    return true;

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CheckTitle(IWebDriver driver)
        {
            try
            {
                string title = "Thank you for your purchase!";

                IWebElement thankYouElm = driver.FindElement(By.XPath("//*[@class = 'sweet-alert  showSweetAlert visible']"));
                var text = thankYouElm.FindElement(By.TagName("h2")).Text; //חילוץ טקסט מתוך ההודעה

                if (title.Equals(text))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
