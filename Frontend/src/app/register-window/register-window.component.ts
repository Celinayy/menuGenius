import { Component } from '@angular/core';
import { MenuComponent } from '../menu/menu.component';
import { AuthService } from '../services/auth.service';
import { LoginWindowComponent } from '../login-window/login-window.component';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';


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


  constructor(public menu: MenuComponent, private authService: AuthService, private toastr: ToastrService, private toast: ToastrService) { }

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
        .subscribe((result) => {
          this.menu.showLogin();
          this.toastr.success("Sikeres regisztráció!");
        });
  }

}
