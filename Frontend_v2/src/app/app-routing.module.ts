import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { FoodMenuComponent } from './food-menu/food-menu.component';
import { DrinkMenuComponent } from './drink-menu/drink-menu.component';
import { ReservationComponent } from './reservation/reservation.component';
import { CartComponent } from './cart/cart.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { FoodWindowComponent } from './food-window/food-window.component';

const routes: Routes = [
  {
    title: 'Rólunk',
    path: 'about',
    component: AboutComponent,
  },
  {
    title: 'Étlap',
    path: 'food-menu',
    component: FoodWindowComponent
  },
  {
    title: 'Itallap',
    path: 'drink-menu',
    component: DrinkMenuComponent
  },
  {
    title: 'Foglalás',
    path: 'reservation',
    component: ReservationComponent
  },
  {
    title: 'Kosár',
    path: 'cart',
    component: CartComponent
  },
  {
    title: 'Belépés',
    path: 'login',
    component: LoginComponent
  },
  {
    title: 'Regisztráció',
    path: 'register',
    component: RegisterComponent
  },
  {
    title: 'Profil',
    path: 'profile',
    component: ProfileComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
