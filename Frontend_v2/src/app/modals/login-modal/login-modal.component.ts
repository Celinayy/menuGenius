import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-login-modal',
  templateUrl: './login-modal.component.html',
  styleUrl: './login-modal.component.css'
})
export class LoginModalComponent {
    public email: string = "";
    public password: string = "";
  
    constructor( 
      private authService: AuthService, 
      private toast: ToastrService,  
      private router: Router,
      public activeModal: NgbActiveModal
      ) {}
  
  public login() {
    this.authService.login(this.email, this.password)
      .pipe(catchError((err: HttpErrorResponse) => {
        if(err.error.error) {
          this.toast.error(err.error.error)
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