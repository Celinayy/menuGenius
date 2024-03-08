import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Allergen } from '../models/allergen.model';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AllergenService {

  private url = "http://localhost:8000/api/allergen"

  constructor(private connection: HttpClient) { }

  public getAllAllergen(){
    return this.connection.get<Allergen[]>(this.url)
  }

  // public getAllAllergen(): Observable<Allergen[]>{
  //   this.connection.get<Allergen[]>(this.url).subscribe((allergens: Allergen[]) => {
  //     allergens.forEach(allergen => {
  //       if (allergen.code.toString().endsWith('00')) allergen.parent = true;
  //       else allergen.parent = false;
  //     });
  //     return allergens;
  //   });
  // }

  // getAllAllergen() {
  //   return this.connection.get<Allergen[]>('this.url').pipe(
  //     tap(allergens => {
  //       allergens.forEach(allergen => {
  //         if (allergen.code >= 1 && allergen.code < 2) {
  //           allergen.parentCode = 1;
  //         } else if (allergen.code >= 8 && allergen.code < 9) {
  //           allergen.parentCode = 8;
  //         } else {
  //           allergen.parentCode = 0;
  //         }
  //       });
  //     })
  //   );
  // }
}
