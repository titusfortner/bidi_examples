    using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Network;

namespace Selenium.Request;

[TestClass]
public class ModifyPostTest : TestBase
{
    [TestMethod]
    public async Task ModifyPostBody()
    {
        await Driver.Navigate().GoToUrlAsync("https://httpbin.org/forms/post");

        const string newContent = "custname=&custtel=&custemail=fake@example.com&delivery=&comments=";
        
        var bodyAsBase64 = new Base64BytesValue(Encoding.UTF8.GetBytes(newContent)); 
        var byteLen = Encoding.UTF8.GetByteCount(newContent);
        
        await using var intercept = await Bidi.Network.InterceptRequestAsync(async req =>
        {
            if (req.Request.Url.Contains("httpbin.org/post"))
            {
                var headers = new List<Header?>(req.Request.Headers);
                var remove = headers.Find(h => h?.Name == "Content-Length");
                headers.Remove(remove);
                headers.Add(new Header("Content-Length", byteLen.ToString()));

                await req.ContinueAsync(new ContinueRequestOptions { Body = bodyAsBase64, Headers = headers! });
            }
            else
            {
                await req.ContinueAsync();
            }
        });

        // var options = new RequestInterceptOptions {
        //    UrlPatterns = new List<UrlPattern> {
        //        new UrlPattern(new Dictionary<string, string> {
        //            { "hostname", "httpbin.org" },
        //            { "path", "post/"}
        //        })
        //    }
        // };
        // await using var registration = await driver.Network.AddRequestHandlerAsync(
        //     options,
        //     req =>
        //     {
        //          if (req.Method == "POST")
        //          {
        //              req.Content = newContent
        //          }
        //     });

        Driver.FindElement(By.Name("custemail")).SendKeys("real@example.com");
        Driver.FindElement(By.TagName("button")).Click();

        Assert.AreEqual(
            "custemail \"fake@example.com\"",
            Driver.FindElement(By.Id("/form/custemail")).Text);
    }
}