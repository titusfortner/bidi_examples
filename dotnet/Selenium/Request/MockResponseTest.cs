using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Selenium.Request;

[TestClass]
public class MockResponseTest : TestBase
{
    [TestMethod]
    public async Task MockResponseBody()
    {
        const string mockBody = "<h1>Mock Response</h1>";

        await using var intercept = await Bidi.Network.InterceptRequestAsync(async req =>
        {
            await req.ProvideResponseAsync(new() { Body = mockBody });
        });

        // var options = new RequestInterceptOptions {
        //               UrlPatterns = new List<UrlPattern> { new UrlPattern("https://selenium.dev/") },
        //           };
        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     options,
        //     req =>
        //     {
        //         req.Body = mockBody);
        //     });
        
        await Driver.Navigate().GoToUrlAsync("https://selenium.dev/");
        Assert.AreEqual("Mock Response", Driver.FindElement(By.TagName("h1")).Text);
    }
}