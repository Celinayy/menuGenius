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

const routes: Routes = [
  {path: "", component: MainWindowComponent},
  {path: "tableReservation", component: TableReservationWindowComponent},
  {path: "profile", component: UserProfileComponent},
  {path: "profile/settings", component: SettingsWindowComponent},
  {path: "profile/history", component: HistoryWindowComponent},
  {path: "profile/favFood", component: FavouriteFoodWindowComponent},
  {path: "products", component: ProductsWindowComponent},
  {path: "products/:id", component: ProductsDetailsWindowComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
