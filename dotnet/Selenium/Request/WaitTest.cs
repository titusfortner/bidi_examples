using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.BiDi.Network;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Request;

[TestClass]
public class WaitTest : TestBase
{
    [TestMethod]
    public async Task WaitAndGetRequest()
    {
        InterceptedRequest request = null;

        await using var intercept = await Bidi.Network.InterceptRequestAsync(async req =>
        {
            if (req.Request.Url.EndsWith(".png"))
            {
                request = req;
            }

            await req.ContinueAsync();
        });

        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     filters: UrlPattern.ALL,        
        //     handler: route =>
        //     {
        //         if (route.Request.Url.EndsWith(".png")
        //         {
        //             return route.Complete();
        //         }
        //         return route.Next();
        //     });

        await Driver.Navigate().GoToUrlAsync("https://selenium.dev");

        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        // Can return values from the intercept
        wait.Until(x => request != null);
        // request = wait.Until(x => registration.getResult() != null)

        // We want to support the ability to stop processing after the first match
        Assert.IsTrue(request!.Request.Url.Contains("sponsors"), "Processing did not stop on first match");
    }
}