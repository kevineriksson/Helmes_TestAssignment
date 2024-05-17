import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Shipment } from '../models/shipment.model';

@Injectable({
  providedIn: 'root'
})
export class ShipmentService {

  private apiUrl = 'https://localhost:7076/api/Shipment';

  constructor(private http: HttpClient) { }

  getAllShipments(): Observable<Shipment[]> {
    return this.http.get<Shipment[]>(this.apiUrl);
  }
  getShipmentById(id: string): Observable<Shipment> {
    return this.http.get<Shipment>(`${this.apiUrl}/${id}`);
  }
  createShipment(shipment: Shipment): Observable<Shipment> {
    return this.http.post<Shipment>(this.apiUrl, shipment);
  }

  updateShipment(shipment: Shipment): Observable<Shipment> {
    return this.http.put<Shipment>(`${this.apiUrl}/${shipment.id}`, shipment);
  }

  deleteShipment(id: string): Observable<Shipment> {
    return this.http.delete<Shipment>(`${this.apiUrl}/${id}`);
  }
  finalizeShipment(id: string): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/finalize`, null);
  }
}
