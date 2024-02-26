import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ToastrModule } from 'ngx-toastr';

import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LogoComponent } from './logo/logo.component';
import { AboutComponent } from './about/about.component';
import { FoodMenuComponent } from './food-menu/food-menu.component';
import { FoodWindowComponent } from './food-window/food-window.component';
import { DrinkMenuComponent } from './drink-menu/drink-menu.component';
import { ReservationComponent } from './reservation/reservation.component';
import { CartComponent } from './cart/cart.component';
import { ContentComponent } from './content/content.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CommonModule } from '@angular/common';
import { SlickCarouselModule } from 'ngx-slick-carousel';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    NavbarComponent,
    LogoComponent,
    AboutComponent,
    FoodMenuComponent,
    FoodWindowComponent,
    DrinkMenuComponent,
    ReservationComponent,
    CartComponent,
    ContentComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ToastrModule.forRoot({
      positionClass: "toast-top-center",
    }),
    CommonModule,
    FormsModule,
    SlickCarouselModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
