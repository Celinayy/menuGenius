import { Component } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {

  public showRegister: boolean = false
  public showLogin: boolean = false;

  Login() {
    this.showLogin = true;
  }

  Register() {
    this.showRegister = true;
  }

  BackToSomewhere() {
    this.showLogin = false
    this.showRegister = false
  }
}
