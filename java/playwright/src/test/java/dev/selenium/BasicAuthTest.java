package dev.selenium;

import com.microsoft.playwright.Browser;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.*;

public class BasicAuthTest extends TestBase {

  @Test
  public void basicAuthContext() {
    Browser.NewContextOptions options =
        new Browser.NewContextOptions().setHttpCredentials("admin", "admin");

    context.close();
    context = browser.newContext(options);
    page = context.newPage();

    page.navigate("https://the-internet.herokuapp.com/basic_auth");

    Assertions.assertEquals(
        "Congratulations! You must have the proper credentials.", page.locator("p").innerText());
  }
}
