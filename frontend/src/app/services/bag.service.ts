import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BagWithLetters } from '../models/bag-with-letters.model';
import { BagWithParcels } from '../models/bag-with-parcels.model';
import { Bag } from '../models/bag.model';

@Injectable({
  providedIn: 'root'
})
export class BagService {
  private apiUrl = 'https://localhost:7076/api/Bag';

  constructor(private http: HttpClient) { }

getBags(): Observable<Bag[]> {
    return this.http.get<Bag[]>(this.apiUrl);
}

getBagById(id: string): Observable<Bag> {
    return this.http.get<Bag>(`${this.apiUrl}/${id}`);
}
getBagsByShipmentId(shipmentId: string): Observable<Bag[]> {
    return this.http.get<Bag[]>(`${this.apiUrl}/getBagsByShipmentId/${shipmentId}`);
}
createParcelBag(bag: BagWithParcels): Observable<BagWithParcels> {
    return this.http.post<BagWithParcels>(`${this.apiUrl}/createParcelBag`, bag);
}

createLetterBag(bag: BagWithLetters): Observable<BagWithLetters> {
    return this.http.post<BagWithLetters>(`${this.apiUrl}/createLetterBag`, bag);
}

updateBag(bag: Bag): Observable<Bag> {
    return this.http.put<Bag>(`${this.apiUrl}/${bag.id}`, bag);
}

deleteBag(id: string): Observable<Bag> {
    return this.http.delete<Bag>(`${this.apiUrl}/${id}`);
}
}
