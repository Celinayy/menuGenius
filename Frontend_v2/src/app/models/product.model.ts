import { Category } from "./category.model";
import { Image } from "./image.model";
import { Ingredient } from "./ingredient.model";

export interface Product {
    id: number;
    name: string;
    description: string;
    packing: string;
    price: number;
    is_food: boolean;
    image: Image;
    category: Category;
    ingredients: Ingredient[];
}
