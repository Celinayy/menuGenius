import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule, DatePipe } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TableReservationWindowComponent } from './table-reservation-window/table-reservation-window.component';
import { SettingsWindowComponent } from './settings-window/settings-window.component';
import { RegisterWindowComponent } from './register-window/register-window.component';
import { MenuComponent } from './menu/menu.component';
import { MainWindowComponent } from './main-window/main-window.component';
import { LoginWindowComponent } from './login-window/login-window.component';
import { HeaderComponent } from './header/header.component';
import { ContactWindowComponent } from './contact-window/contact-window.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductsWindowComponent } from './products-window/products-window.component';
import { HttpClientModule } from '@angular/common/http';
import { FilterPipe } from './filter.pipe';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { HistoryWindowComponent } from './user-reservation-window/user-reservation-window.component';
import { FavouriteFoodWindowComponent } from './favourite-food-window/favourite-food-window.component';
import { ProductsDetailsWindowComponent } from './products-details-window/products-details-window.component';
import { CartWindowComponent } from './cart-window/cart-window.component';
import { StarButtonComponent } from './star-button/star-button.component';
import { SuccesfullPaymentWindowComponent } from './succesfull-payment-window/succesfull-payment-window.component';
import { CancelPaymentWindowComponent } from './cancel-payment-window/cancel-payment-window.component';
import { DrinksWindowComponent } from './drinks-window/drinks-window.component';
import { FoodsWindowComponent } from './foods-window/foods-window.component';
import { AuthMenuWindowComponent } from './auth-menu-window/auth-menu-window.component';

export function prefersReducedMotion(): boolean {
  const mediaQueryList = window.matchMedia("(prefers-reduced-motion)");
  return mediaQueryList.matches;
}

@NgModule({
  declarations: [
    AppComponent,
    TableReservationWindowComponent,
    SettingsWindowComponent,
    RegisterWindowComponent,
    MenuComponent,
    MainWindowComponent,
    LoginWindowComponent,
    HeaderComponent,
    ContactWindowComponent,
    ProductsWindowComponent,
    FilterPipe,
    UserProfileComponent,
    HistoryWindowComponent,
    FavouriteFoodWindowComponent,
    ProductsDetailsWindowComponent,
    CartWindowComponent,
    StarButtonComponent,
    SuccesfullPaymentWindowComponent,
    CancelPaymentWindowComponent,
    DrinksWindowComponent,
    FoodsWindowComponent,
    AuthMenuWindowComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule.withConfig({
      // A teszteken belül le kell tiltani az animációkat
      disableAnimations: prefersReducedMotion(),
    }),
    CommonModule,
    ToastrModule.forRoot({
      positionClass: "toast-top-center",
    }),
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
