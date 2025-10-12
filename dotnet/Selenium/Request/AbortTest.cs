using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;

namespace Selenium.Request;

[TestClass]
public class AbortTest : TestBase
{
    [TestMethod]
    public async Task ConditionalAbort()
    {
        await using var intercept = await Bidi.Network.InterceptRequestAsync(async req =>
        {
            if (req.Request.Url.EndsWith(".png"))
            {
                await req.FailAsync();
            }
            else
            {
                await req.ContinueAsync();
            }
        });

        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     filters: UrlPattern.ALL,        
        //     handler: route =>
        //     {
        //         if (route.Request.Url.EndsWith(".png")
        //         {
        //             return route.Fail();
        //         }
        //         return route.Next();
        //     });

        await Driver.Navigate().GoToUrlAsync("https://selenium.dev");

        var img = Driver.FindElement(By.CssSelector("img"));
        var naturalHeight = (long)((IJavaScriptExecutor)Driver)
            .ExecuteScript("return arguments[0].naturalHeight", img)!;
        Assert.AreEqual(0L, naturalHeight);
    }
}