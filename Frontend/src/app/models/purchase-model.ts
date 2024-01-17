export interface PurchaseModel {
  id: number;
  date_time: Date;
  total_pay: number;
  status: "cooked" | "reserved" | "ordered";
  paid: boolean;
  user_id: number;
  desk_id: number;
}
