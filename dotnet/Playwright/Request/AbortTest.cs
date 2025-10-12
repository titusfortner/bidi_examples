using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playwright.Request;

[TestClass]
public class AbortTest : TestBase
{
    [TestMethod]
    public async Task UnconditionalAbort()
    {
        await Page!.RouteAsync("**/*.css", route => route.AbortAsync());

        await Page.GotoAsync("https://selenium.dev");
        var height = (await Page.Locator("svg").First.BoundingBoxAsync())?.Height;
        Assert.IsTrue(height > 30);
    }

    [TestMethod]
    public async Task ConditionalAbort()
    {
        await Page!.RouteAsync("**/*", async route =>
        {
            if (route.Request.ResourceType == "image")
            {
                await route.AbortAsync();
            }
            else
            {
                await route.ContinueAsync();
            }
        });

        await Page.GotoAsync("https://selenium.dev");

        var height = await Page.Locator("img").First.EvaluateAsync<int>("img => img.naturalHeight");
        Assert.AreEqual(0, height);
    }
}