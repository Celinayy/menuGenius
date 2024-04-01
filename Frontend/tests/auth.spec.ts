import { Page, expect, test } from "@playwright/test";
import { fakerHU as faker } from "@faker-js/faker";
import { login, logout, register } from "./utils";

test.beforeEach(async ({ page }) => {
    // Disable animations for tests
    await page.emulateMedia({
        reducedMotion: "reduce",
    });
});

test("Regisztráció", async ({ page }) => {
    await page.goto("/");

    const fullName = faker.person.fullName();
    const email = faker.internet.email();
    const phone = faker.phone.number();
    const password = faker.internet.password();

    await register(page, fullName, email, phone, password);

    await expect(page.getByText("Sikeres regisztráció!")).toBeVisible();
});

test("Bejelentkezés", async ({ page }) => {
    await page.goto("/");

    const fullName = faker.person.fullName();
    const email = faker.internet.email();
    const phone = faker.phone.number();
    const password = faker.internet.password();

    await register(page, fullName, email, phone, password);
    await login(page, email, password);

    await expect(page.getByText("Sikeres bejelentkezés!")).toBeVisible();
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
