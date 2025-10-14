using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Network;

namespace Selenium.Request;

[TestClass]
public class ModifyHeaderTest : TestBase
{
    [TestMethod]
    public async Task AddHeader()
    {
        await using var intercept = await Bidi.Network.InterceptRequestAsync(async req =>
        {
            var headers = new List<Header>(req.Request.Headers) { new("X-Test", "true") };

            await req.ContinueAsync(new ContinueRequestOptions { Headers = headers });
        });

        // var options = new RequestInterceptOptions {
        //               UrlPatterns = new List<UrlPattern> { new UrlPattern("https://httbin.org/") },
        //           };
        //
        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     options,
        //     req =>
        //     {
        //          req.AddHeader("X-Test", "true");
        //      }
        // );

        await Driver.Navigate().GoToUrlAsync("https://httpbin.org/headers");
        Assert.AreEqual("X-Test \"true\"", Driver.FindElement(By.Id("/headers/X-Test")).Text);
    }

    [TestMethod]
    public async Task RemoveHeader()
    {
        await using var intercept = await Bidi.Network.InterceptRequestAsync(async req =>
        {
            var headers = new List<Header?>(req.Request.Headers);
            var remove = headers.Find(h => h?.Name == "Upgrade-Insecure-Requests");
            headers.Remove(remove);

            await req.ContinueAsync(new ContinueRequestOptions { Headers = headers! });
        });

        
        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     options,
        //     req =>
        //     {
        //          req.removeHeader("Upgrade-Insecure-Requests")
        //      }
        // );

        await Driver.Navigate().GoToUrlAsync("https://httpbin.org/headers");

        Assert.AreEqual(0, Driver.FindElements(By.Id("/headers/Upgrade-Insecure-Requests")).Count);
    }
}