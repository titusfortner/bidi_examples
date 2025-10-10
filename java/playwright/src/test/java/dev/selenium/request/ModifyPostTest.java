package dev.selenium.request;

import com.microsoft.playwright.Route;
import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class ModifyPostTest extends TestBase {

  @Test
  public void modifyPostBody() {
    page.navigate("https://httpbin.org/forms/post");
    page.fill("input[name=custemail]", "real@example.com");

    context.route(
        "**/post",
        route -> {
          String overrideBody = "custname=&custtel=&custemail=fake@example.com&delivery=&comments=";

          route.resume(new Route.ResumeOptions().setPostData(overrideBody));
        });
    page.click("button");

    String bodyText = page.innerText("body").replaceAll("\\s+", "");
    String expected = "\"custemail\":\"fake@example.com\"";
    Assertions.assertTrue(bodyText.contains(expected));
  }
}
