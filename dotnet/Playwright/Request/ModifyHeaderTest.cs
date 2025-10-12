using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playwright.Request;

[TestClass]
public class ModifyHeaderTest : TestBase
{
    [TestMethod]
    public async Task AddHeader()
    {
        await Page!.RouteAsync("https://httpbin.org/**", async route =>
        {
            var headers = new Dictionary<string, string>(await route.Request.AllHeadersAsync());
            headers["X-Test"] = "true";
            await route.ContinueAsync(new RouteContinueOptions { Headers = headers });
        });

        await Page.GotoAsync("https://httpbin.org/headers");

        var bodyText = await Page.InnerTextAsync("body");
        Assert.IsTrue(bodyText.Contains("\"X-Test\": \"true\""));
    }

    [TestMethod]
    public async Task RemoveHeader()
    {
        await Page!.RouteAsync("https://httpbin.org/**", async route =>
        {
            var headers = new Dictionary<string, string>(await route.Request.AllHeadersAsync());
            headers.Remove("upgrade-insecure-requests");
            await route.ContinueAsync(new RouteContinueOptions { Headers = headers });
        });

        await Page.GotoAsync("https://httpbin.org/headers");

        var bodyText = await Page.InnerTextAsync("body");
        Assert.IsFalse(bodyText.Contains("Upgrade-Insecure-Requests"));
    }
}