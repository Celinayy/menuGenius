import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule, DatePipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorIntl } from '@angular/material/paginator';

import { ToastrModule } from 'ngx-toastr';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';

import { AboutComponent } from './about/about.component';
import { CartComponent } from './cart/cart.component';
import { ContentComponent } from './content/content.component';
import { DrinkMenuComponent } from './drink-menu/drink-menu.component';
import { FoodMenuComponent } from './food-menu/food-menu.component';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { LoginModalComponent } from './modals/login-modal/login-modal.component';
import { LogoComponent } from './logo/logo.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ProductModalComponent } from './modals/product-modal/product-modal.component';
import { ProfileComponent } from './profile/profile.component';
import { RegisterModalComponent } from './modals/register-modal/register-modal.component';
import { ReservationComponent } from './reservation/reservation.component';

import { FilterPipe } from './filter.pipe';
import { PaginatorService } from './services/paginator.service';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    NavbarComponent,
    LogoComponent,
    AboutComponent,
    FoodMenuComponent,
    DrinkMenuComponent,
    ReservationComponent,
    CartComponent,
    ContentComponent,
    ProfileComponent,
    FilterPipe,
    ProductModalComponent,
    LoginModalComponent,
    RegisterModalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ToastrModule.forRoot({
      positionClass: "toast-center-center",
    }),
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    NgbModalModule,
    MatPaginatorModule,
    MatTableModule,
    MatInputModule
    ],
  providers: [
    DatePipe,
    provideAnimationsAsync(),
    { provide: MatPaginatorIntl, useClass: PaginatorService }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
