import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {

  constructor(public authService: AuthService){}

  public show: "login" | "register" | null = null;

  showLogin() {
    this.show = "login";
  }

  showRegister() {
    this.show = "register";
  }

  back() {
    this.show = null;
  }
}
