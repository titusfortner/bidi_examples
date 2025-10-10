package dev.selenium.request;

import com.microsoft.playwright.Request;
import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class WaitTest extends TestBase {

  @Test
  public void waitAndGetResult() {
    Request request =
        page.waitForRequest(
            r -> "image".equals(r.resourceType()), () -> {
                    page.navigate("https://selenium.dev");
                });

    Assertions.assertTrue(request.url().contains("sponsors"));
  }
}
