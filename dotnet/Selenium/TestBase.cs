using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Firefox;

namespace Selenium;

[TestClass]
public class TestBase
{
    protected IWebDriver Driver = null!;
    protected BiDi Bidi = null!;
    protected BrowsingContext Context = null!;
 
    protected async Task StartFirefox()
    {
        var options = new FirefoxOptions()
        {
            UseWebSocketUrl = true
        };
        Driver = new FirefoxDriver(options);
        Bidi = await Driver.AsBiDiAsync();
        Context = (await Bidi.BrowsingContext.GetTreeAsync()).Contexts[0].Context;
    }

    [TestInitialize]
    public async Task Setup()
    {
        await StartFirefox();
    }

    [TestCleanup]
    public async Task CloseContext()
    {
        await BiDi.DisposeAsync();
        Driver.Quit();
    }
}
