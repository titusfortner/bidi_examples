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

        // var options = new RequestInterceptOptions {
        //               UrlPatterns = new List<UrlPattern> { new UrlPattern("https://www.selenium.dev/") },
        //           };
        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     options,
        //     req =>
        //     {
        //         if (req.Url.EndsWith(".png")
        //         {
        //             req.Complete();
        //         }
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