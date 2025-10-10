package dev.selenium.request;

import dev.selenium.TestBase;
import java.util.ArrayList;
import java.util.List;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class LogTest extends TestBase {

  @Test
  public void logRequests() {
    List<String> requests = new ArrayList<>();
    context.onRequest(request -> requests.add(request.url()));

    page.navigate("https://demo.playwright.dev/todomvc/");

    Assertions.assertEquals(5, requests.size());
  }
}
