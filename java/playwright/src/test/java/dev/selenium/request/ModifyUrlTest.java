package dev.selenium.request;

import com.microsoft.playwright.Route;
import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class ModifyUrlTest extends TestBase {

  @Test
  public void redirectUrl() {
    page.route(
        "https://selenium.dev/**",
        route -> {
          String replaced =
              route
                  .request()
                  .url()
                  .replaceFirst(
                      "https://selenium.dev",
                      "https://deploy-preview-2198--selenium-dev.netlify.app");
          route.resume(new Route.ResumeOptions().setUrl(replaced));
        });

    page.navigate("https://selenium.dev");

    String message =
        "Registrations Open for SeleniumConf 2025 | March 26–28 | Join Us In-Person! Register now!";
    Assertions.assertEquals(message, page.locator("h4").first().innerText());
  }

  @Test
  public void addArgs() {
    page.route(
        "https://httpbin.org/**",
        route -> {
          String appended = route.request().url() + "?foo=bar";
          route.resume(new Route.ResumeOptions().setUrl(appended));
        });

    page.navigate("https://httpbin.org/anything");

    String bodyText = page.innerText("body").replaceAll("\\s+", "");
    Assertions.assertTrue(bodyText.contains("\"args\":{\"foo\":\"bar\""));
  }
}
