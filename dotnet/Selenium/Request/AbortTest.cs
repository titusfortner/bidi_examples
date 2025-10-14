using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

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
        //     new RequestInterceptOptions(),
        //     req =>
        //     {
        //         if (req.Url.EndsWith(".png")
        //         {
        //             req.Fail();
        //         }
        //     });

        await Driver.Navigate().GoToUrlAsync("https://selenium.dev");

        var img = Driver.FindElement(By.CssSelector("img"));
        var naturalHeight = (long)((IJavaScriptExecutor)Driver)
            .ExecuteScript("return arguments[0].naturalHeight", img)!;
        Assert.AreEqual(0L, naturalHeight);
    }
}