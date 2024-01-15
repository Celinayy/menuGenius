import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

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
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: "toast-top-center",
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
