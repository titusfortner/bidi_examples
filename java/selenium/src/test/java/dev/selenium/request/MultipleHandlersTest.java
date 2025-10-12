package dev.selenium.request;

import dev.selenium.TestBase;
import java.net.URI;
import java.util.function.Predicate;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.firefox.FirefoxDriver;

public class MultipleHandlersTest extends TestBase {

  @Test
  public void modifyMultiple() {
    Predicate<URI> filter = uri -> uri.getHost().equals("httpbin.org");

    ((FirefoxDriver) driver)
        .network()
        .addRequestHandler(filter, req -> req.removeHeader("upgrade-insecure-requests"));

    ((FirefoxDriver) driver)
        .network()
        .addRequestHandler(filter, req -> req.addHeader("X-Test", "true"));

    //    UrlPattern filter = UrlPatternBuilder.setHost("selenium.dev").build();
    //    driver
    //        .network()
    //        .addRequestHandler(filter, route -> route.next(route.request.addHeader("X-Test",
    // "true")));
    //
    //    driver
    //        .network()
    //        .addRequestHandler(
    //            filter, req ->
    // route.next(route.request.removeHeader("upgrade-insecure-requests")));

    driver.get("https://httpbin.org/headers");

    String bodyText =
        driver.findElement(org.openqa.selenium.By.tagName("body")).getText().replaceAll("\\s+", "");
    Assertions.assertFalse(bodyText.contains("Upgrade-Insecure-Requests"));
    Assertions.assertTrue(
        bodyText.contains("\"X-Test\":\"true\""), "Does not yet support multiple handlers");
  }
}
