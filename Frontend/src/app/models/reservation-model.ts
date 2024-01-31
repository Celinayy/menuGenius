export interface ReservationModel {
  id: number;
  number_of_guests: number;
  checkin_date: Date;
  checkout_date: Date;
  name: string;
  phone: string;
  desk_id: number;
  comment: string;
}
