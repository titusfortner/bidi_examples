using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playwright.Request;

[TestClass]
public class WaitTest : TestBase
{
    [TestMethod]
    public async Task WaitAndGetResult()
    {
        var request = await Page!.RunAndWaitForRequestAsync(
            async () => await Page.GotoAsync("https://selenium.dev"),
            r => r.ResourceType == "image"
        );

        Assert.IsTrue(request.Url.Contains("sponsors"));
    }
}