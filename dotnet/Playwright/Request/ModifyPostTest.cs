using System.Text;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Playwright.Request;

[TestClass]
public class ModifyPostTest : TestBase
{
    [TestMethod]
    public async Task ModifyPostBody()
    {
        await Page!.GotoAsync("https://httpbin.org/forms/post");
        await Page.FillAsync("input[name=custemail]", "real@example.com");

        await Page.RouteAsync("**/post", async route =>
        {
            var overrideBody = "custname=&custtel=&custemail=fake@example.com&delivery=&comments=";
            await route.ContinueAsync(new RouteContinueOptions { PostData = Encoding.UTF8.GetBytes(overrideBody) });
        });

        await Page.ClickAsync("button");

        var expected = "\"custemail\": \"fake@example.com\"";
        var bodyText = await Page.TextContentAsync("body");
        Assert.IsTrue(bodyText!.Contains(expected));
    }
}