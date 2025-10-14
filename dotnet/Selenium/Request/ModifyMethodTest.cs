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

        // var options = new RequestInterceptOptions {
        //               UrlPatterns = new List<UrlPattern> { new UrlPattern("https://www.selenium.dev/") },
        //           };
        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     options,
        // .   req =>
        //     {
        //          req.Method = "HEAD";
        //      }
        // );
        
        await Driver.Navigate().GoToUrlAsync("https://selenium.dev");

        Assert.AreEqual("", Driver.FindElement(By.TagName("body")).Text);
    }
}