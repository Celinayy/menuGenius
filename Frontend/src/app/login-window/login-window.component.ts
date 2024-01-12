import { Component } from '@angular/core';
import { MenuComponent } from '../menu/menu.component';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-window',
  templateUrl: './login-window.component.html',
  styleUrls: ['./login-window.component.css']
})
export class LoginWindowComponent {
  public email: string = "";
  public password: string = "";

  public showSuccessfulLoginToast: boolean = false;
  public showSuccessfulRegisterToast: boolean = false;

  constructor(public menu: MenuComponent, private authService: AuthService) {}

  public login() {
    this.authService.login(this.email, this.password).subscribe((result) => {
      localStorage.setItem("token", result.token);
      this.showSuccessfulLoginToast = true;
      setTimeout(() => {
        this.showSuccessfulLoginToast = false;
      }, 2000);
    });
  }

}
