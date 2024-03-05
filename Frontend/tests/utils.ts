import { Page } from "@playwright/test";

export const register = async (page: Page, fullName: string, email: string, phone: string, password: string) => {
    await page.getByTestId("open-auth-dialog-button").click();

    await page.getByTestId("open-register-window-button").click();

    await page.getByTestId("full-name-input").fill(fullName);
    await page.getByTestId("email-input").fill(email);
    await page.getByTestId("phone-input").fill(phone);
    await page.getByTestId("password-input").fill(password);
    await page.getByTestId("password-again-input").fill(password);
    await page.getByTestId("register-button").click();
};

export const login = async (page: Page, email: string, password: string) => {
    await page.getByTestId("open-auth-dialog-button").click();

    await page.getByTestId("open-login-window-button").click();

    await page.getByTestId("email-input").fill(email);
    await page.getByTestId("password-input").fill(password);
    await page.getByTestId("login-button").click();
};

export const logout = async (page: Page) => {
    await page.getByTestId("open-profile-button").click();
    await page.getByTestId("logout-button").click();
};
