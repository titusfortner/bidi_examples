using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Network;

namespace Selenium.Request;

[TestClass]
public class ModifyMethodTest : TestBase
{
    [TestMethod]
    public async Task ModifyHttpMethod()
    {
        await using var intercept = await Bidi.Network.InterceptRequestAsync(async req =>
        {
            await req.ContinueAsync(new ContinueRequestOptions { Method = "HEAD" });
        });

        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     UrlPattern.ALL, route =>
        //     {
        //          return route.Next(new HttpRequest(route.Request) { Method = "HEAD" })
        //      }
        // );
        
        await Driver.Navigate().GoToUrlAsync("https://selenium.dev");

        Assert.AreEqual("", Driver.FindElement(By.TagName("body")).Text);
    }
}