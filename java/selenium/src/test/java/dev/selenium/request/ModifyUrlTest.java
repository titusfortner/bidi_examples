package dev.selenium.request;

import dev.selenium.TestBase;
import java.net.URI;
import java.util.function.Predicate;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.remote.http.HttpRequest;

public class ModifyUrlTest extends TestBase {

  @Test
  public void redirectUrl() {
      Predicate<URI> filter = uri -> uri.getHost().equals("selenium.dev");
      String url = "https://deploy-preview-2198--selenium-dev.netlify.app";

      ((FirefoxDriver) driver)
              .network()
              .addRequestHandler(filter, req -> new HttpRequest(req.getMethod(), url));

      //    UrlPattern filter = UrlPatternBuilder.setHost("selenium.dev").build();
      //    driver.network().addRequestHandler(filter, route ->
      // route.next(route.request().setUrl(url));

      driver.get("https://selenium.dev");

      String message = "Registrations Open for SeleniumConf 2025 | March 26–28 | Join Us In-Person! Register now!";
      Assertions.assertEquals(message, driver.findElement(By.tagName("h4")).getText());
  }
}
