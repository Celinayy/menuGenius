import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainWindowComponent } from './main-window/main-window.component';
import { TableReservationWindowComponent } from './table-reservation-window/table-reservation-window.component';
import { ProductsWindowComponent } from './products-window/products-window.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { SettingsWindowComponent } from './settings-window/settings-window.component';
import { HistoryWindowComponent } from './user-reservation-window/user-reservation-window.component';
import { FavouriteFoodWindowComponent } from './favourite-food-window/favourite-food-window.component';
import { ProductsDetailsWindowComponent } from './products-details-window/products-details-window.component';
import { CartWindowComponent } from './cart-window/cart-window.component';
import { SuccesfullPaymentWindowComponent } from './succesfull-payment-window/succesfull-payment-window.component';
import { CancelPaymentWindowComponent } from './cancel-payment-window/cancel-payment-window.component';
import { FoodsWindowComponent } from './foods-window/foods-window.component';
import { DrinksWindowComponent } from './drinks-window/drinks-window.component';
import { AuthMenuWindowComponent } from './auth-menu-window/auth-menu-window.component';
import { LoginWindowComponent } from './login-window/login-window.component';
import { RegisterWindowComponent } from './register-window/register-window.component';

const routes: Routes = [
  { path: "", component: MainWindowComponent },
  { path: "auth", component: AuthMenuWindowComponent, data: { animation: "vertical" } },
  { path: "auth/login", component: LoginWindowComponent, data: { animation: "horizontal" } },
  { path: "auth/register", component: RegisterWindowComponent, data: { animation: "horizontal" } },
  { path: "tableReservation", component: TableReservationWindowComponent },
  { path: "profile", component: UserProfileComponent },
  { path: "profile/settings", component: SettingsWindowComponent },
  { path: "profile/history", component: HistoryWindowComponent },
  { path: "profile/favFood", component: FavouriteFoodWindowComponent },
  { path: "products", component: ProductsWindowComponent },
  { path: "products/:id", component: ProductsDetailsWindowComponent },
  { path: "cart", component: CartWindowComponent },
  { path: "profile/foods", component: FoodsWindowComponent },
  { path: "profile/drinks", component: DrinksWindowComponent },
  { path: "stripe/payment/success", component: SuccesfullPaymentWindowComponent },
  { path: "stripe/payment/cancel", component: CancelPaymentWindowComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
