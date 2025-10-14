using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Selenium.Request;

[TestClass]
public class LogTest : TestBase
{
    [TestMethod]
    public async Task LogRequests()
    {
        var requests = new List<string>();
        
        await using var subscription = await Context.Network.OnBeforeRequestSentAsync(req =>
        {
            requests.Add(req.Request.Url);
        });

        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     ObserverOptionsBuilder.Defaults(),
        //     req =>
        //     {
        //         requests.Add(req.Url)
        //     });

        await Driver.Navigate().GoToUrlAsync("https://demo.playwright.dev/todomvc/");

        Assert.AreEqual(5, requests.Count);
    }
}