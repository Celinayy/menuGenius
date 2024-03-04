import { Pipe, PipeTransform } from '@angular/core';
import { Product } from './models/product.model';

@Pipe({
  name: 'filter',
})
export class FilterPipe implements PipeTransform {

  transform(product: Product[], filterString: string, name: string): Product[] {
    const result: Product[] = [];

    if(!product || filterString === '' || name === '') {
      return product;
    }
    product.forEach((p) => {
      if(p.name.trim().toLowerCase().includes(filterString.toLowerCase())){
        result.push(p)
      }
    });
    return result;
  }
}
