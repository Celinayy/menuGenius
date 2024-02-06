import { Component } from '@angular/core';
import { ReservationService } from '../services/reservation.service';
import { ReservationModel } from '../models/reservation-model';
import * as moment from 'moment';
@Component({
  selector: 'app-user-reservation-window',
  templateUrl: './user-reservation-window.component.html',
  styleUrls: ['./user-reservation-window.component.css']
})
export class HistoryWindowComponent {

  public userReservationHistory: ReservationModel[] = [];

  constructor(public reservationService: ReservationService) {
    this.loadHistory()
  }

  public loadHistory() {
    this.reservationService.getUserReservation().subscribe((userHistory) => {
      this.userReservationHistory = userHistory;
    })
  }

  public daySince(reservation: ReservationModel) {
    const difference: number = moment(reservation.checkin_date).diff(moment(), "days")
    if (difference > 0) {
      return difference + " nap múlva érkezhet is éttermünkbe!"
    } else if (difference < 0) {
      return Math.abs(difference) + " nappal ezelőtti foglalás"
    } else {
      return "Mai napi foglalás"
    }
  }

  public isReservationOver(reservation: ReservationModel) {
    return moment(reservation.checkout_date).isBefore(moment(), "day");
  }

  public isReservationFuture(reservation: ReservationModel) {
    return moment(reservation.checkin_date).isAfter(moment(), "day");
  }

  public isReservationToday(reservation: ReservationModel) {
    return moment(reservation.checkin_date).isSame(moment(), "day");
  }
}
