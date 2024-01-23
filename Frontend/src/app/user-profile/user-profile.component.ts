import { Component } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Observable, catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent {

  public initialCarouselItem = 0;
  public products: ProductModel[] = []

  constructor(public authService: AuthService, private toast: ToastrService, private router: Router) {
  }


  public logout() {
    this.authService.logout()
      .pipe(catchError((err: HttpErrorResponse) => {
        this.toast.error(err.statusText)
        return throwError(() => err)
      }))
      .subscribe(() => {
        this.router.navigate(["/"]);
        this.toast.success("Sikeres kijelentkezés!")
      })
  }
}
