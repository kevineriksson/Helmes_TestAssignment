import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Parcel } from '../models/parcel.model';

@Injectable({
  providedIn: 'root'
})
export class ParcelService {
  private apiUrl = 'https://localhost:7076/api/Parcel';

  constructor(private http: HttpClient) { }

  getParcels(): Observable<Parcel[]> {
    return this.http.get<Parcel[]>(this.apiUrl);
  }

  getParcelByBagId(id: string): Observable<Parcel> {
    return this.http.get<Parcel>(`${this.apiUrl}/${id}`);
  }
  getParcelsByBagId(id: string): Observable<Parcel[]> {
    return this.http.get<Parcel[]>(`${this.apiUrl}/${id}`);
  }
  createParcel(parcel: Parcel): Observable<Parcel> {
    return this.http.post<Parcel>(this.apiUrl, parcel);
  }
  updateParcel(parcel: Parcel): Observable<Parcel> {
    return this.http.put<Parcel>(`${this.apiUrl}/${parcel.id}`, parcel);
  }
  deleteParcel(id: string): Observable<Parcel> {
    return this.http.delete<Parcel>(`${this.apiUrl}/${id}`);
  }
}
