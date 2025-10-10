package dev.selenium.request;

import com.microsoft.playwright.Route;
import dev.selenium.TestBase;
import java.util.Map;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class ModifyHeaderTest extends TestBase {

  @Test
  public void addHeader() {
    page.route(
        "https://httpbin.org/**",
        route -> {
          Map<String, String> headers = route.request().headers();
          headers.put("X-Test", "true");
          route.resume(new Route.ResumeOptions().setHeaders(headers));
        });

    page.navigate("https://httpbin.org/headers");

    String bodyText = page.innerText("body").replaceAll("\\s+", "");
    Assertions.assertTrue(bodyText.contains("\"X-Test\":\"true\""));
  }

  @Test
  public void removeHeader() {
    page.route(
        "https://httpbin.org/**",
        route -> {
          Map<String, String> headers = route.request().headers();
          headers.remove("upgrade-insecure-requests");
          route.resume(new Route.ResumeOptions().setHeaders(headers));
        });

    page.navigate("https://httpbin.org/headers");

    Assertions.assertFalse(page.innerText("body").contains("Upgrade-Insecure-Requests"));
  }
}
