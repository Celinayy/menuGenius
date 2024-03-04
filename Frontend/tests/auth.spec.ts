import { Page, expect, test } from "@playwright/test";
import { faker } from "@faker-js/faker";
import { login, logout, register } from "./utils";

test("Regisztráció", async ({ page }) => {
    await page.goto("/");

    const fullName = faker.person.fullName();
    const email = faker.internet.email();
    const phone = faker.phone.number();
    const password = faker.internet.password();

    await register(page, fullName, email, phone, password);
});

test("Bejelentkezés", async ({ page }) => {
    await page.goto("/");

    const fullName = faker.person.fullName();
    const email = faker.internet.email();
    const phone = faker.phone.number();
    const password = faker.internet.password();

    await register(page, fullName, email, phone, password);
    await login(page, email, password);
});

test("Kijelentkezés", async ({ page }) => {
    await page.goto("/");

    const fullName = faker.person.fullName();
    const email = faker.internet.email();
    const phone = faker.phone.number();
    const password = faker.internet.password();

    await register(page, fullName, email, phone, password);
    await login(page, email, password);
    await logout(page);

    await expect(page.getByText("Sikeres kijelentkezés!")).toBeVisible();
});
