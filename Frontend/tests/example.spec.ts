import { test, expect } from '@playwright/test';

test('has title', async ({ page }) => {
  await page.goto('http://localhost:4200/');

  // Expect a title "to contain" a substring.
  await expect(page).toHaveTitle('MenuGenius');
});

test('"Kínálatunk" megnyitása teszt', async ({ page }) => {
  await page.goto('http://localhost:4200/');

  // Click the get started link.
  await page.getByTestId('openProductsButton').click()
  await expect(page).toHaveURL('http://localhost:4200/products');


});
