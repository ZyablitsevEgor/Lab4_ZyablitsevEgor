using System;
using ClassLibraryLab4;
using System.Net.Http;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Linq;

namespace ParserLab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://4pda.to/forum/index.php?showtopic=521153";

            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(url);
                //Thread.Sleep(2000);//
                IList<IWebElement> names = driver.FindElements(By.ClassName("normalname"));
                IList<IWebElement> texts = driver.FindElements(By.ClassName("postcolor"));
                IList<IWebElement> idblocks = driver.FindElements(By.ClassName("ipbtable"));

                IList<IWebElement> updatedList = idblocks.Skip(1).ToList();
                List<int> ids = new List<int>();
                foreach (IWebElement idblock in updatedList)
                {
                    ids.Add(Convert.ToInt32(idblock.GetAttribute("data-post")));
                }

                using (DbContext context = new DbContext())
                {
                    for (int i = 1; i < names.Count; i++)
                    {
                        DBEntity entity = new DBEntity
                        {
                            
                            ID = ids[i],
                            Name = names[i].Text,
                            Message = texts[i].Text
                        };
                        context.Entities.Add(entity);
                    }
                    context.SaveChanges();
                }

                DataAccess dataAccess1 = new DataAccess();

                List<DBEntity> messages1 = dataAccess1.GetAllMessages();

                foreach (var message in messages1)
                {
                    Console.WriteLine($"ID: {message.ID}, Name: {message.Name}, Message: {message.Message}");
                }
            }

            //DataAccess da1 = new DataAccess();
            //da1.DeleteAllData();
        }
    }
}
