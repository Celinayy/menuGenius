import { Allergen } from "./allergen.model";

export interface Ingredient {
    id: number;
    name: string;
    allergens: Allergen[];
}
