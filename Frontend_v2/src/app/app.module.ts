import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { SlickCarouselModule } from 'ngx-slick-carousel';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AboutComponent } from './about/about.component';
import { FoodMenuComponent } from './food-menu/food-menu.component';
import { DrinkMenuComponent } from './drink-menu/drink-menu.component';
import { ReservationComponent } from './reservation/reservation.component';
import { CartComponent } from './cart/cart.component';
import { ContentComponent } from './content/content.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    NavbarComponent,
    AboutComponent,
    FoodMenuComponent,
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
    SlickCarouselModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
