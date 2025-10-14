using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Network;

namespace Selenium.Request;

[TestClass]
public class ModifyUrlTest : TestBase
{
    [TestMethod]
    public async Task RedirectUrl()
    {
        const string url = "https://deploy-preview-2198--selenium-dev.netlify.app";
        await using var intercept = await Bidi.Network.InterceptRequestAsync(async req =>
        {
            await req.ContinueAsync(new ContinueRequestOptions { Url = url });
        });

        // var options = new RequestInterceptOptions {
        //               UrlPatterns = new List<UrlPattern> { new UrlPattern("https://www.selenium.dev/") },
        //           };
        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     options,
        //     req =>
        //     {
        //          req.Url = url;
        //      }
        // );


        await Driver.Navigate().GoToUrlAsync("https://selenium.dev");

        const string message =
            "Registrations Open for SeleniumConf 2025 | March 26–28 | Join Us In-Person! Register now!";
        Assert.AreEqual(message, Driver.FindElement(By.TagName("h4")).Text);
    }
}