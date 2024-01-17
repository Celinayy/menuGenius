import { PurchaseModel } from "./purchase-model";
import { ReservationModel } from "./reservation-model";

export interface UserModel {
  id: number;
  name: string;
  email: string;
  email_verified_at: string;
  phone: string;
  admin: boolean;
  purchases: PurchaseModel[];
  reservations: ReservationModel[];
}
