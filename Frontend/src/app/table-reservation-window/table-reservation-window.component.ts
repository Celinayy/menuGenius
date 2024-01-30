import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { DatePipe } from '@angular/common';
import { ReservationService } from '../services/reservation.service';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-table-reservation-window',
  templateUrl: './table-reservation-window.component.html',
  styleUrls: ['./table-reservation-window.component.css']
})
export class TableReservationWindowComponent {

  public phone: string = "";
  public name: string = "";
  public date: Date = new Date();
  public number_of_guests: number = 1;
  public check_in_datetime: string = "10:00";
  public check_out_datetime: string = "11:00";
  public comment: string = "";


  constructor(
    public authService: AuthService,
    private datePipe: DatePipe,
    public reservationService: ReservationService,
    private toast: ToastrService,
  ) {
    this.authService.getUser().subscribe((user) => {
      this.phone = user.phone;
      this.name = user.name;
    })
  }

  public handleSubmit() {
    this.reservationService.createReservation(
      {
        phone: this.phone,
        name: this.name,
        date: this.date,
        number_of_guests: this.number_of_guests,
        check_in_datetime: this.check_in_datetime,
        check_out_datetime: this.check_out_datetime,
        comment: this.comment,
      }
    )
    .pipe(catchError((err) => {
      this.toast.error(err.error.message);
      return throwError(() => err);
    }))
    .subscribe(() => {
      this.toast.success("Sikeres foglalás!");
    })
  }

  get formattedDate() {
    return this.datePipe.transform(this.date, "yyyy-MM-dd")
  }
}
