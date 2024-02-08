import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Observable, catchError, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-settings-window',
  templateUrl: './settings-window.component.html',
  styleUrls: ['./settings-window.component.css']
})
export class SettingsWindowComponent {

  public showSavedToast: boolean = false;

  public email: string = "";
  public emailAgain: string = "";
  public phone: string = "";
  public phoneAgain: string = "";
  public currentPassword: string = "";
  public password: string = "";
  public passwordAgain: string = "";
  public passwordCheck: string = "";
  public passwordAgainCheck: string = "";
  public userId: number = 0;


  constructor(public authService: AuthService, private toast: ToastrService,
    public router: Router) { }

  // public saveSettings() {
  //   this.showSavedToast = true;
  // setTimeout(() =>{
  //   this.showSavedToast = false;
  // }, 2500)

  // }

  public saveEmail() {
    this.authService.update({ email: this.email })
      .pipe(catchError((err) => {
        this.toast.error(err.error.message)
        return throwError(() => err)
      }))
      .subscribe(() => {
        setTimeout(() => {
          this.toast.success("Sikeres mentés!")
        }, 2500)
      })
  }

  public savePhone() {
    this.authService.update({ phone: this.phone })
      .pipe(catchError((err) => {
        this.toast.error(err.error.message)
        return throwError(() => err)
      }))
      .subscribe(() => {
        setTimeout(() => {
          this.toast.success("Sikeres mentés!")
        }, 2500)
      })

  }

  public savePassword() {
    this.authService.update({
      password: this.password,
      password_confirmation: this.passwordAgain,
      current_password: this.currentPassword
    })
      .pipe(catchError((err) => {
        this.toast.error(err.error.message)
        return throwError(() => err)
      }))
      .subscribe(() => {
        setTimeout(() => {
          this.toast.success("Sikeres mentés!")
        }, 2500)
      })
  }

 // Nem működik még

  public checkPassword(passwordCheck: string) {
    if(passwordCheck === this.currentPassword) {
      this.toast.success("Egyezik a két jelszó!")
    } else {
      this.toast.error("Nem egyezik a két jelszó!")
    }
  }

  public deleteUser(current_password: string) {
    this.authService.deleteUser(current_password).pipe(catchError((err) => {
      this.toast.error(err.error.message)
      return throwError(() => err)
    }))
      .subscribe(() => {
        this.authService.logout().subscribe()
        this.router.navigate(["/"]);
        this.toast.success("Sikeresen törölte a felhasználóját!")
      })
  }
}
