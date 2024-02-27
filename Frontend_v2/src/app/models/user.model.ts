import { Purchase } from "./purchase.model";
import { Reservation } from "./reservation.model";
export interface User {
    id: number;
    name: string;
    email: string;
    email_verified_at: string;
    phone: string;
    admin: boolean;
    purchases: Purchase[];
    reservations: Reservation[];
  }