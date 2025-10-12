using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playwright;

[TestClass]
public class TestBase
{
    protected static IPlaywright? Playwright;
    protected static IBrowser? Browser;
    protected IBrowserContext? Context;
    protected IPage? Page;

    [AssemblyInitialize]
    public static async Task LaunchBrowser(TestContext testContext)
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
    }

    [AssemblyCleanup]
    public static async Task CloseBrowser()
    {
        if (Browser != null)
        {
            await Browser.CloseAsync();
        }
        Playwright?.Dispose();
    }

    [TestInitialize]
    public async Task CreateContextAndPage()
    {
        Context = await Browser!.NewContextAsync();
        Page = await Context.NewPageAsync();
    }

    [TestCleanup]
    public async Task CloseContext()
    {
        await Context!.CloseAsync();
    }
}