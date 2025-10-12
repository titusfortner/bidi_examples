using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Network;

namespace Selenium;

[TestClass]
public class BasicAuthTest : TestBase
{
    [TestMethod]
    public async Task BasicAuth()
    {
        await using var intercept = await Bidi.Network.InterceptAuthAsync(async auth =>
        {
            await auth.ContinueAsync(new AuthCredentials("admin", "admin"));
        });

        // await using var registration = await driver.Network.AddAuthenticationHandlerAsync(
        //     UrlPattern.ALL,        
        //     new AuthCredentials("admin", "admin")
        // );

        await Driver.Navigate().GoToUrlAsync("https://the-internet.herokuapp.com/basic_auth");

        Assert.AreEqual(
            "Congratulations! You must have the proper credentials.",
            Driver.FindElement(By.TagName("p")).Text);
    }
}