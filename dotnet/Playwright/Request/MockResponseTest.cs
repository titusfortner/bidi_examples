using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playwright.Request;

[TestClass]
public class MockResponseTest : TestBase
{
    [TestMethod]
    public async Task MockResponseBody()
    {
        var mockBody = "<h1>Mock Response</h1>";
        await Page!.RouteAsync("https://selenium.dev/", async route =>
        {
            await route.FulfillAsync(new RouteFulfillOptions { Body = mockBody });
        });

        await Page.GotoAsync("https://selenium.dev");
        var h1Text = await Page.TextContentAsync("h1");
        Assert.AreEqual("Mock Response", h1Text);
    }
}