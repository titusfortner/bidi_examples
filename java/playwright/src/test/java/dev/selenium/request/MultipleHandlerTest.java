package dev.selenium.request;

import com.microsoft.playwright.Route;
import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.Map;

public class MultipleHandlerTest extends TestBase {

  @Test
  public void modifyMultiple() {
    page.route(
        "https://httpbin.org/**",
        route -> {
          Map<String, String> headers = route.request().headers();
          headers.put("X-Test", "true");
          route.resume(new Route.ResumeOptions().setHeaders(headers));
        });

    page.route(
        "https://httpbin.org/**",
        route -> {
          Map<String, String> headers = route.request().headers();
          headers.remove("upgrade-insecure-requests");
          route.fallback(new Route.FallbackOptions().setHeaders(headers));
        });

    page.navigate("https://httpbin.org/headers");

    String bodyText = page.innerText("body").replaceAll("\\s+", "");
    Assertions.assertFalse(bodyText.contains("Upgrade-Insecure-Requests"));
    Assertions.assertTrue(bodyText.contains("\"X-Test\":\"true\""));
  }
}
