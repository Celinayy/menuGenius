import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Allergen } from '../models/allergen.model';

@Injectable({
  providedIn: 'root'
})
export class AllergenService {

  private url = "http://localhost:8000/api/allergen"

  constructor(private connection: HttpClient) { }

  public getAllAllergen(){
    return this.connection.get<Allergen[]>(this.url)
  }

}
