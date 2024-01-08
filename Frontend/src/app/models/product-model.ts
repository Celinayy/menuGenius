export interface ProductModel {
  id: number,
  name: string,
  description: string,
  category_id: number,
  packing: string,
  price: number,
  is_food: boolean,
  image_id: number,
  image: {
    data: string,
  }
}
