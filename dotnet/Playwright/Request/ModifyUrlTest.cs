using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playwright.Request;

[TestClass]
public class ModifyUrlTest : TestBase
{
    [TestMethod]
    public async Task RedirectUrl()
    {
        await Page!.RouteAsync("https://selenium.dev/**", async route =>
        {
            var replaced = route.Request.Url
                .Replace("https://selenium.dev", "https://deploy-preview-2198--selenium-dev.netlify.app");
            await route.ContinueAsync(new RouteContinueOptions { Url = replaced });
        });

        await Page.GotoAsync("https://selenium.dev");

        var message = "Registrations Open for SeleniumConf 2025 | March 26–28 | Join Us In-Person! Register now!";
        var actualMessage = await Page.Locator("h4").First.InnerTextAsync();
        Assert.AreEqual(message, actualMessage);
    }

    [TestMethod]
    public async Task AddArgs()
    {
        await Page!.RouteAsync("https://httpbin.org/**", async route =>
        {
            var appended = route.Request.Url + "?foo=bar";
            await route.ContinueAsync(new RouteContinueOptions { Url = appended });
        });

        await Page.GotoAsync("https://httpbin.org/anything");

        var bodyText = await Page.InnerTextAsync("body");
        Assert.IsTrue(bodyText.Contains("\"args\": {\n    \"foo\": \"bar\""));
    }
}