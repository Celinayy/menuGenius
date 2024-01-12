import { Component } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
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
