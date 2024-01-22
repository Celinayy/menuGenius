import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-table-reservation-window',
  templateUrl: './table-reservation-window.component.html',
  styleUrls: ['./table-reservation-window.component.css']
})
export class TableReservationWindowComponent {

  public phone: string = "";
  public email: string = "";


  constructor(public authService: AuthService) {
    this.authService.getUser().subscribe((user) => {
      this.phone = user.phone;
      this.email = user.email;
    })
  }
}
