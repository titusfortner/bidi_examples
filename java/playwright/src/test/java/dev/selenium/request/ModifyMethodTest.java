package dev.selenium.request;

import com.microsoft.playwright.Route;
import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class ModifyMethodTest extends TestBase {

  @Test
  public void modifyHttpMethod() {
    page.route(
        "https://selenium.dev/**",
        route -> {
          route.resume(new Route.ResumeOptions().setMethod("HEAD"));
        });

    page.navigate("https://selenium.dev");
    Assertions.assertEquals("", page.textContent("body"));
  }
}
