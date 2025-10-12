using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playwright.Request;

[TestClass]
public class MultipleHandlerTest : TestBase
{
    [TestMethod]
    public async Task ModifyMultiple()
    {
        await Page!.RouteAsync("https://httpbin.org/**", async route =>
        {
            var headers = new Dictionary<string, string>(await route.Request.AllHeadersAsync());
            headers["X-Test"] = "true";
            await route.ContinueAsync(new RouteContinueOptions { Headers = headers });
        });

        await Page.RouteAsync("https://httpbin.org/**", async route =>
        {
            var headers = new Dictionary<string, string>(await route.Request.AllHeadersAsync());
            headers.Remove("upgrade-insecure-requests");
            await route.FallbackAsync(new RouteFallbackOptions { Headers = headers });
        });

        await Page.GotoAsync("https://httpbin.org/headers");

        var bodyText = await Page.InnerTextAsync("body");
        Assert.IsFalse(bodyText.Contains("Upgrade-Insecure-Requests"));
        Assert.IsTrue(bodyText.Contains("\"X-Test\": \"true\""));
    }
}