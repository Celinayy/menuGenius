import { Component } from '@angular/core';
import { MenuComponent } from '../menu/menu.component';
import { AuthService } from '../services/auth.service';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login-window',
  templateUrl: './login-window.component.html',
  styleUrls: ['./login-window.component.css']
})
export class LoginWindowComponent {
  public email: string = "";
  public password: string = "";

  constructor(public menu: MenuComponent, private authService: AuthService, private toast: ToastrService) { }

  public login() {
    this.authService.login(this.email, this.password)
      .pipe(catchError((err: HttpErrorResponse) => {
        for (const message of err.error.email ?? []) {
          this.toast.error(message)
        }
        for (const message of err.error.password ?? []) {
          this.toast.error(message)
        }
        return throwError(() => err)
      }))
      .subscribe((result) => {
        localStorage.setItem("token", result.token);
        this.toast.success("Sikeres bejelentkezés!")
      });

  }

}
