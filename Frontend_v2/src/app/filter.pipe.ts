import { Pipe, PipeTransform } from '@angular/core';
import { Product } from './models/product.model';

@Pipe({
  name: 'filter',
})
export class FilterPipe implements PipeTransform {

  transform(product: Product[], filterString: string, propName: string): Product[] {
    const result: Product[] = [];

    if(!product || filterString === '' || propName === '') {
      return product;
    }
    product.forEach((p: any) => {
      if(p[propName].trim().toLowerCase().includes(filterString.toLowerCase())){
        result.push(p)
      }
    });
    return result;
  }

}
