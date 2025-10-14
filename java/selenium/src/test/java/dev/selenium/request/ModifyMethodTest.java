package dev.selenium.request;

import dev.selenium.TestBase;
import java.net.URI;
import java.util.function.Predicate;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.remote.http.HttpMethod;
import org.openqa.selenium.remote.http.HttpRequest;

public class ModifyMethodTest extends TestBase {

  @Test
  public void modifyHttpMethod() {
    Predicate<URI> filter = uri -> uri.getHost().equals("selenium.dev");

    ((FirefoxDriver) driver)
        .network()
        .addRequestHandler(filter, req -> new HttpRequest(HttpMethod.HEAD, req.getUri()));

      // .  RequestInterceptOptions options =
      // RequestInterceptOptionsBuilder.addFilter(Map.of("host", "httpbin.org").build();
    //    driver.network().addRequestHandler(options, req -> req.setMethod("HEAD"));

    driver.get("https://selenium.dev");

    Assertions.assertEquals("", driver.findElement(By.tagName("body")).getText());
  }
}
