import { Pipe, PipeTransform } from '@angular/core';
import { ProductModel } from './models/product-model';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(product: ProductModel[], filterString: string, propName: string): ProductModel[] {
    const result: ProductModel[] = [];

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
