using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playwright;

[TestClass]
public class BasicAuthTest : TestBase
{
    [TestMethod]
    public async Task BasicAuthContext()
    {
        var options = new BrowserNewContextOptions
        {
            HttpCredentials = new HttpCredentials
            {
                Username = "admin",
                Password = "admin"
            }
        };

        await Context!.CloseAsync();
        Context = await Browser!.NewContextAsync(options);
        Page = await Context.NewPageAsync();

        await Page.GotoAsync("https://the-internet.herokuapp.com/basic_auth");

        var paragraphText = await Page.Locator("p").InnerTextAsync();
        Assert.AreEqual(
            "Congratulations! You must have the proper credentials.",
            paragraphText);
    }
}