using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playwright.Request;

[TestClass]
public class LogTest : TestBase
{
    [TestMethod]
    public async Task LogRequests()
    {
        var requests = new List<string>();
        Page!.Request += (_, request) => requests.Add(request.Url);

        await Page.GotoAsync("https://demo.playwright.dev/todomvc/");

        Assert.AreEqual(5, requests.Count);
    }
}