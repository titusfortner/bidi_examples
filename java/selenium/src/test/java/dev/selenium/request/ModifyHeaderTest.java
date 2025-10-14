package dev.selenium.request;

import dev.selenium.TestBase;
import java.net.URI;
import java.util.function.Predicate;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.firefox.FirefoxDriver;

public class ModifyHeaderTest extends TestBase {

  @Test
  public void addHeader() {
    Predicate<URI> filter = uri -> uri.getHost().equals("httpbin.org");

    ((FirefoxDriver) driver)
        .network()
        .addRequestHandler(filter, req -> req.addHeader("X-Test", "true"));

    // .  RequestInterceptOptions options =
    // RequestInterceptOptionsBuilder.addFilter(Map.of("host", "httpbin.org").build();
    //    driver
    //        .network()
    //        .addRequestHandler(options, req -> req.addHeader("X-Test", "true")));

    driver.get("https://httpbin.org/headers");
    Assertions.assertEquals(
        "X-Test \"true\"", driver.findElement(By.id("/headers/X-Test")).getText());
  }

  @Test
  public void removeHeader() {
    Predicate<URI> filter = uri -> uri.getHost().equals("httpbin.org");

    ((FirefoxDriver) driver)
        .network()
        .addRequestHandler(filter, req -> req.removeHeader("upgrade-insecure-requests"));

    // .  RequestInterceptOptions options =
    // RequestInterceptOptionsBuilder.addFilter(Map.of("host", "httpbin.org").build();
    //    driver
    //        .network()
    //        .addRequestHandler(options, req-> req.removeHeader("upgrade-insecure-requests"));

    driver.get("https://httpbin.org/headers");

    Assertions.assertTrue(
        driver.findElements(By.id("/headers/Upgrade-Insecure-Requests")).isEmpty());
  }
}
