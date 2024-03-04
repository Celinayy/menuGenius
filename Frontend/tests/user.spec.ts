import { Page, expect, test } from "@playwright/test";
import { faker } from "@faker-js/faker";
import { login, register } from "./utils";

const openSettings = async (page: Page) => {
    await page.getByTestId("open-profile-button").click();
    await page.getByTestId("open-settings-button").click();
};

test("Felhasználó törlése", async ({ page }) => {
    await page.goto("/");

    const fullName = faker.person.fullName();
    const email = faker.internet.email();
    const phone = faker.phone.number();
    const password = faker.internet.password();

    await register(page, fullName, email, phone, password);
    await login(page, email, password);
    await openSettings(page);
    
    await page.getByTestId("delete-user-button").click();
    await expect(page.getByText("Sikeresen törölte a felhasználóját!")).toBeVisible();
});


test("Jelszó megváltoztatása", async ({ page }) => {
    await page.goto("/");

    const fullName = faker.person.fullName();
    const email = faker.internet.email();
    const phone = faker.phone.number();
    const password = faker.internet.password();
    const newPassword = faker.internet.password();

    await register(page, fullName, email, phone, password);
    await login(page, email, password);
    await openSettings(page);

    await page.getByTestId("current-password-input").fill(password);
    await page.getByTestId("new-password-input").fill(newPassword);
    await page.getByTestId("new-password-again-input").fill(newPassword);

    await page.getByTestId("save-password-change-button").click();
    await expect(page.getByText("Sikeres mentés!")).toBeVisible();
});

