import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import * as moment from 'moment';
import { ReservationModel } from '../models/reservation-model';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  private url:string = `${environment.apiUrl}/api/reservation`



  constructor(private connection: HttpClient) { }


  public createReservation(data: {
    phone: string;
    name: string;
    number_of_guests: number;
    date: Date;
    check_in_datetime: string;
    check_out_datetime: string;
    comment: string;
  }) {
    const token = localStorage.getItem("token");

    return this.connection.post(`${this.url}`, {
      name: data.name,
      phone: data.phone,
      number_of_guests: data.number_of_guests,
      comment: data.comment,
      checkin_date: this.formatDateTime(data.date, data.check_in_datetime),
      checkout_date: this.formatDateTime(data.date, data.check_out_datetime)
    }, {
      headers: token ? {
        "Authorization": `Bearer ${token}`,
      } : {

      },
    })
  }

  private formatDateTime(date: Date, time: string) {
    return `${moment(date).format("YYYY-MM-DD")} ${time}:00`
  }


  public getUserReservation() {
    return this.connection.get<ReservationModel[]>(this.url, {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
    })
  }
}
