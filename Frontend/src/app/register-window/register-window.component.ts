import { Component } from '@angular/core';
import { MenuComponent } from '../menu/menu.component';
import { AuthService } from '../services/auth.service';
import { LoginWindowComponent } from '../login-window/login-window.component';
import { ToastrService } from 'ngx-toastr';

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


  constructor(public menu: MenuComponent, private authService: AuthService, private toastr: ToastrService) {}

  public register() {
    this.authService.register(this.name, this.email, this.phone, this.password, this.passwordAgain).subscribe((result) => {
      this.menu.showLogin();
      this.toastr.success("Sikeres regisztráció!", undefined, {
        timeOut: 3000,
      });
    });
  }

}
