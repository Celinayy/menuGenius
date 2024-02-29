import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-register-modal',
  templateUrl: './register-modal.component.html',
  styleUrl: './register-modal.component.css'
})
export class RegisterModalComponent {
  public name: string = "";
  public email: string = "";
  public password: string = "";
  public passwordAgain: string = "";
  public phone: string = "";

  constructor(
    private authService: AuthService, 
    private toast: ToastrService,
    private router: Router,
    public activeModal: NgbActiveModal){}


  public register() {
    this.authService.register(this.name, this.email, this.phone, this.password, this.passwordAgain)
      .pipe(catchError((err: HttpErrorResponse) => {
        for (const message of err.error.name ?? []) {
          this.toast.error(message)
        }
        for (const message of err.error.phone ?? []) {
          this.toast.error(message)
        }
        for (const message of err.error.email ?? []) {
          this.toast.error(message)
        }
        for (const message of err.error.password ?? []) {
          this.toast.error(message)
        }
        return throwError(() => err)
      }))
      .subscribe(() => {
        this.router.navigate(["/"]);
        window.location.reload()
      });
  }

  closeModal() {
    this.activeModal.close();
  }
}
