import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Observable, catchError, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

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


  constructor(public authService: AuthService, private toast: ToastrService) { }

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
}
