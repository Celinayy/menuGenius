import { Pipe, PipeTransform } from '@angular/core';
import { Product } from '../models/product.model';

@Pipe({
  name: 'charFilter',
})
export class CharFilterPipe implements PipeTransform {

  transform(products: Product[], filterString: string, name: string): Product[] {
    const result: Product[] = [];

    if(!products || filterString === '' || name === '') {
      return products;
    }
    products.forEach((p) => {
      if(p.name.trim().toLowerCase().includes(filterString.toLowerCase())){
        result.push(p)
      }
    });
    return result;
  }
}
