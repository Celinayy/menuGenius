import { Pipe, PipeTransform } from '@angular/core';
import { Allergen } from '../models/allergen.model';

@Pipe({
  name: 'allergenFilter'
})
export class AllergenFilterPipe implements PipeTransform {


  transform(allergens: Allergen[], selectedAllergens: { [key: number]: boolean }): Allergen[] {
    if (!allergens || !selectedAllergens) {
      return [];
    }

    return allergens.map(allergen => {
      return {
        ...allergen,
        active: selectedAllergens[allergen.id]
      };
    });
  }

  // transform(products: Product[], selectedAllergens: {[key:number]:boolean}, ...args: unknown[]): Product[] {
  //   let result: Product[] = [];

  //   if (!products || !selectedAllergens) {
  //     return products;
  //   }
  //   result = products.filter(product =>
  //     product.ingredients.every(ingredient =>
  //       ingredient.allergens.every(allergen => selectedAllergens[allergen.id])
  //       )
  //     );
  //     return result;
  // }
}




// transform(product: Product[], filterString: string, name: string): Product[] {
//   const result: Product[] = [];

//   if(!product || filterString === '' || name === '') {
//     return product;
//   }
//   product.forEach((p) => {
//     if(p.name.trim().toLowerCase().includes(filterString.toLowerCase())){
//       result.push(p)
//     }
//   });
//   return result;
// }
// }

