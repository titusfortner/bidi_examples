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

        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     filters: UrlPattern.ALL,        
        //     handler: route =>
        //     {
        //         return route.Respond(route.Request.Body = mockBody);
        //     });
        
        await Driver.Navigate().GoToUrlAsync("https://selenium.dev/");
        Assert.AreEqual("Mock Response", Driver.FindElement(By.TagName("h1")).Text);
    }
}