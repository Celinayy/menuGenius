import { Component, Output, EventEmitter, HostListener } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginModalComponent } from '../modals/login-modal/login-modal.component';
import { RegisterModalComponent } from '../modals/register-modal/register-modal.component';
import { CartService } from '../services/cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  isNavbarCollapsed = true;
  cartItemCount: number = 0;

  @Output() openLoginModal: EventEmitter<void> = new EventEmitter<void>();
  @Output() openRegisterModal: EventEmitter<void> = new EventEmitter<void>();

  constructor(
    public authService: AuthService,
    private modalService: NgbModal,
    public cartService: CartService,
    private router: Router
    ){}

  logout() {
    this.authService.logout().subscribe(() => {
    });
  }

  toggleNavbar() {
    this.isNavbarCollapsed = !this.isNavbarCollapsed;
  }

  onRegisterClick() {
    this.modalService.open(RegisterModalComponent, {size: 'sm', centered: true, animation: true, keyboard: true});
  }

  onLoginClick() {
    this.modalService.open(LoginModalComponent, {size: 'sm', centered: true, animation: true, keyboard: true});
  }
  @HostListener('window:keyup', ['$event'])

  handleKeyDown(event: KeyboardEvent) {
    if (event.key === 'F1') {
      this.router.navigate(['/about']);
    }
    if (event.key === 'F2') {
      this.router.navigate(['/food-menu']);
    }
    if (event.key === 'F3') {
      this.router.navigate(['/drink-menu']);
    }
    if (event.key === 'F4') {
      this.router.navigate(['/reservation']);
    }
    if (event.key === 'F5') {
      this.router.navigate(['/cart']);
    }
    if (event.key === 'F6') {
      this.onLoginClick();
    }
    if (event.key === 'F7') {
      this.onRegisterClick();
    }
    if (event.key === 'F8') {
      this.router.navigate(['/profile']);
    }
    if (event.key === 'F9') {
      this.logout();
    }

  }

}
