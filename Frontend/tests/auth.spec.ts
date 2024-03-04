import { test } from "@playwright/test";
import { faker } from "@faker-js/faker";

test("Regisztráció", async ({ page }) => {
    await page.goto("/");

    await page.getByTestId("open-auth-dialog-button").click();

    await page.getByTestId("open-register-window-button").click();

    const password = faker.internet.password();

    await page.getByTestId("full-name-input").fill(faker.person.fullName());
    await page.getByTestId("email-input").fill(faker.internet.email());
    await page.getByTestId("phone-input").fill(faker.phone.number());
    await page.getByTestId("password-input").fill(password);
    await page.getByTestId("password-again-input").fill(password);
    await page.getByTestId("register-button").click();
});

