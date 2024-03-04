import { Page, test } from "@playwright/test";
import { faker } from "@faker-js/faker";

const register = async (page: Page, fullName: string, email: string, phone: string, password: string) => {
    await page.goto("/");

    await page.getByTestId("open-auth-dialog-button").click();

    await page.getByTestId("open-register-window-button").click();

    await page.getByTestId("full-name-input").fill(fullName);
    await page.getByTestId("email-input").fill(email);
    await page.getByTestId("phone-input").fill(phone);
    await page.getByTestId("password-input").fill(password);
    await page.getByTestId("password-again-input").fill(password);
    await page.getByTestId("register-button").click();
};

const login = async (page: Page, email: string, password: string) => {
    await page.goto("/");

    await page.getByTestId("open-auth-dialog-button").click();

    await page.getByTestId("open-login-window-button").click();

    await page.getByTestId("email-input").fill(email);
    await page.getByTestId("password-input").fill(password);
    await page.getByTestId("login-button").click();
};

test("Regisztráció", async ({ page }) => {
    await page.goto("/");

    const fullName = faker.person.fullName();
    const email = faker.internet.email();
    const phone = faker.phone.number();
    const password = faker.internet.password();

    await register(page, fullName, email, phone, password);
});

test("Bejelentkezés", async ({ page }) => {
    const fullName = faker.person.fullName();
    const email = faker.internet.email();
    const phone = faker.phone.number();
    const password = faker.internet.password();

    await register(page, fullName, email, phone, password);
    await login(page, email, password);
});
