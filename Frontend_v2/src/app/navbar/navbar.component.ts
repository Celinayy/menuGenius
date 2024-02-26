import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  isModalOpen: boolean = false;
  isNavbarCollapsed = true;

  toggleNavbar() {
    this.isNavbarCollapsed = !this.isNavbarCollapsed;
  }
}
