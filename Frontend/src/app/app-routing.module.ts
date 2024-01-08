import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainWindowComponent } from './main-window/main-window.component';
import { TableReservationWindowComponent } from './table-reservation-window/table-reservation-window.component';
import { SettingsWindowComponent } from './settings-window/settings-window.component';
import { ProductsWindowComponent } from './products-window/products-window.component';

const routes: Routes = [
  {path: "", component: MainWindowComponent},
  {path: "tableReservation", component: TableReservationWindowComponent},
  {path: "settings", component: SettingsWindowComponent},
  {path: "products", component: ProductsWindowComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
