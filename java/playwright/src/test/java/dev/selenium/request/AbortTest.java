package dev.selenium.request;

import com.microsoft.playwright.Route;
import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class AbortTest extends TestBase {

  @Test
  public void unconditionalAbort() {
    page.route("**/*.css", Route::abort);

    page.navigate("https://selenium.dev");
    double height = page.locator("svg").first().boundingBox().height;
    Assertions.assertTrue(height > 30);
  }

  @Test
  public void conditionalAbort() {
    page.route(
        "**/*",
        r -> {
          if ("image".equals(r.request().resourceType())) {
            r.abort();
          } else {
            r.resume();
          }
        });

    page.navigate("https://selenium.dev");

    int height = (int) page.locator("img").first().evaluate("img => img.naturalHeight");
    Assertions.assertEquals(0, height);
  }
}
