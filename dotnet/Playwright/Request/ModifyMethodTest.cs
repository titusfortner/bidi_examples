using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playwright.Request;

[TestClass]
public class ModifyMethodTest : TestBase
{
    [TestMethod]
    public async Task ModifyHttpMethod()
    {
        await Page!.RouteAsync("https://selenium.dev/**", async route =>
        {
            await route.ContinueAsync(new RouteContinueOptions { Method = "HEAD" });
        });

        await Page.GotoAsync("https://selenium.dev");
        var bodyText = await Page.TextContentAsync("body");
        Assert.AreEqual("", bodyText);
    }
}