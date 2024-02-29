import { Component, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginModalComponent } from '../modals/login-modal/login-modal.component';
import { RegisterModalComponent } from '../modals/register-modal/register-modal.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  isNavbarCollapsed = true;
  
  @Output() openLoginModal: EventEmitter<void> = new EventEmitter<void>();
  @Output() openRegisterModal: EventEmitter<void> = new EventEmitter<void>();

  constructor(
    public authService: AuthService,
    private modalService: NgbModal,
    ){}

  logout() {
    this.authService.logout().subscribe(() => {
    });
  }

  toggleNavbar() {
    this.isNavbarCollapsed = !this.isNavbarCollapsed;
  }

  onRegisterClick() {
    const modalRef = this.modalService.open(RegisterModalComponent, {size: 'sm', centered: true, animation: true, keyboard: true});
  }

  onLoginClick() {
    const modalRef = this.modalService.open(LoginModalComponent, {size: 'sm', centered: true, animation: true, keyboard: true});
  }

}
