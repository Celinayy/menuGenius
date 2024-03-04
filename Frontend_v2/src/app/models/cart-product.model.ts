import { Category } from "./category.model";
import { Image } from "./image.model";
import { Ingredient } from "./ingredient.model";

export interface CartProduct {
    id: number;
    name: string;
    packing: string;
    price: number;
}

// export interface CartProduct {
//     id: number;
//     name: string;
//     description: string;
//     packing: string;
//     price: number;
//     is_food: boolean;
//     category: Category;
//     ingredients: Ingredient[];
//     user_id: number;
// }
    
