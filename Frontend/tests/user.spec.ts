import { Page, expect, test } from "@playwright/test";
import { faker } from "@faker-js/faker";
import { login, register } from "./utils";

test("Felhasználó törlése", async ({ page }) => {
    await page.goto("/");

    const fullName = faker.person.fullName();
    const email = faker.internet.email();
    const phone = faker.phone.number();
    const password = faker.internet.password();

    await register(page, fullName, email, phone, password);
    await login(page, email, password);

    await page.getByTestId("open-profile-button").click();
    await page.getByTestId("open-settings-button").click();

    await page.getByTestId("password-input").fill(password);
    await page.getByTestId("password-again-input").fill(password);

    await page.getByTestId("delete-user-button").click();
    await expect(page.getByText("Sikeresen törölte a felhasználóját!")).toBeVisible();
});

