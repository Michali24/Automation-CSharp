using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moriya.Pages
{
    public class CartPage
    {
        IWebDriver driver;

        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }


        public bool IsProductInCart(string productTitle)
        {
            try
            {
                return driver.FindElement(By.XPath($"//td[text()='{productTitle}']")).Displayed;
            }
            catch
            {
                Console.WriteLine("Product is not found");
                return false;
            }
        }
        public bool VerifyProductHasBeenAddedToCart(IWebDriver driver, string productTitle, string productPrice)
        {

            IList<IWebElement> tableRow;
            string title = null;
            string price = null;

            try
            {
                IWebElement table = driver.FindElement(By.XPath("//*[@id = 'tbodyid']"));
                tableRow = table.FindElements(By.TagName("tr"));

                foreach (var rowItem in tableRow)
                {
                    IList<IWebElement> rowTD;

                    rowTD = rowItem.FindElements(By.TagName("td"));

                    if (rowTD.Count > 1)
                    {
                        title = rowTD[1].Text;
                        price = rowTD[2].Text;

                    }

                    if (title != null && price != null)
                        if (title.Equals(productTitle) && price.Equals(productPrice))
                            return true;
                }

                return false;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void ClickOnPlaceOrder(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//button[text() = 'Place Order']")).Click();

        }
    }
}
