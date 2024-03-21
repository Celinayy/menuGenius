import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register-window',
  templateUrl: './register-window.component.html',
  styleUrls: ['./register-window.component.css']
})
export class RegisterWindowComponent {
  public name: string = "";
  public email: string = "";
  public password: string = "";
  public passwordAgain: string = "";
  public phone: string = "";


  constructor(
    private authService: AuthService,
    private toast: ToastrService,
    private route: Router,
  ) { }

  public register() {
    this.authService.register(this.name, this.email, this.phone, this.password, this.passwordAgain)
      .pipe(catchError((err: HttpErrorResponse) => {
        for (const message of err.error.name ?? []) {
          this.toast.error(message)
        }
        for (const message of err.error.phone ?? []) {
          this.toast.error(message)
        }
        for (const message of err.error.email ?? []) {
          this.toast.error(message)
        }
        for (const message of err.error.password ?? []) {
          this.toast.error(message)
        }
        return throwError(() => err)
      }))
      .subscribe(() => {
        this.route.navigate(["/"]);
        this.toast.success("Sikeres regisztráció!");
      });
  }

}
