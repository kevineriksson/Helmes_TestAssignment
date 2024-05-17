import { Component, EventEmitter, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ShipmentService } from '../../services/shipment.service';
import { BagManagerComponent } from '../bag/bag.component';
import { BagService } from '../../services/bag.service';
import { ParcelService } from '../../services/parcel.service';
import { Bag } from '../../models/bag.model';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-shipment-manager',
  templateUrl: './shipment.component.html',
  styleUrls: ['./shipment.component.css']
})
export class ShipmentComponent implements OnInit {
  shipmentForm!: FormGroup;
  shipments: any[] = [];
  bags: Bag[] = [];
  shipmentId: string = '';
  shipmentSaved: boolean = false;
  isEditing: boolean = false;
  @ViewChild(BagManagerComponent) bagManager!: BagManagerComponent;

  constructor(private shipmentService: ShipmentService, private bagService: BagService, private parcelService: ParcelService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initForm();
    this.showShipments();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['id']) {
      console.log("ShipmentId changed to:", this.shipmentForm.get('id')?.value);
    }
  }

  initForm(): void {
    this.shipmentForm = this.fb.group({
      id: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9]{3}-[a-zA-Z0-9]{6}$/)]],
      airportCode: ['', [Validators.required, Validators.pattern(/^(TLL|RIX|HEL)$/)]],
      flightNumber: ['', [Validators.required, Validators.pattern(/^[a-zA-Z]{2}[0-9]{4}$/)]],
      flightDate: ['', [Validators.required, this.futureDateValidator]],
      finalized: [false]
    });
  }

  futureDateValidator(control: AbstractControl): ValidationErrors | null {
    const date = new Date(control.value);
    const now = new Date();
    now.setHours(0, 0, 0, 0);
    return date >= now ? null : { pastDate: true };
  }

  showShipments(): void {
    this.shipmentService.getAllShipments().subscribe(shipments => {
      this.shipments = shipments;
      console.log('Shipments:', shipments);
    });
  }

  toggleBags(shipment: any): void {
    if (shipment.finalized) {
      console.warn('Cannot modify finalized shipment');
      return;
    }
    if (!shipment.bags) {
        shipment.bags = [];
        this.bagService.getBagsByShipmentId(shipment.id).subscribe({
            next: (bags) => {
                shipment.bags = bags;
                this.initializeParcelCounts(shipment.bags);
                shipment.showBags = true;
            },
            error: (error) => {
                console.error('Error:', error);
            }
        });
    } else {
        shipment.showBags = !shipment.showBags;
    }
  }

  editBag(bagId: string): void {
    this.bagService.getBagById(bagId).subscribe(bag => {
      this.bagManager.populateBagForm(bag);      
    });
  }

  initializeParcelCounts(bags: any[]): void {
      for (let bag of bags) {
          if (bag.bagType === 'parcel' && bag.parcelCount == null) {
              this.loadParcelCount(bag);
          }
      }
  }

  loadParcelCount(bag: any): void {
      this.parcelService.getParcelsByBagId(bag.id).subscribe({
          next: (parcels) => {
              bag.parcels = parcels;
              bag.parcelCount = parcels.length;
          },
          error: (error) => {
              console.error('Failed to load parcels:', error);
          }
      });
  }

  saveShipment(): void {
    if (this.shipmentForm.invalid) {
      return;
    }

    const shipment = this.shipmentForm.value;
    if (this.isEditing) {
      this.shipmentService.updateShipment(shipment).subscribe({
        next: (response) => {
          const index = this.shipments.findIndex(s => s.id === response.id);
          if (index !== -1) {
            this.shipments[index] = response;
          }
          this.shipmentSaved = true;
          this.shipmentId = response.id = '';
          this.isEditing = false; // Reset editing flag
          this.shipmentForm.get('id')?.enable(); // Re-enable the id field
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
    } else {
      this.shipmentService.createShipment(shipment).subscribe({
        next: (response) => {
          this.shipments.push(response);
          this.shipmentSaved = true;
          this.shipmentId = response.id = '';
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
    }
  }

  deleteShipment(shipment: any): void {
    if (confirm(`Are you sure you want to delete the shipment with ID ${shipment.id}?`)) {
      this.shipmentService.deleteShipment(shipment.id).subscribe({
        next: () => {
          this.shipments = this.shipments.filter(s => s.id !== shipment.id);
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
    }
  }

  editShipment(shipmentId: string): void {
    this.shipmentService.getShipmentById(shipmentId).subscribe(shipment => {
      this.shipmentForm.patchValue({
        id: shipment.id,
        airportCode: shipment.airportCode,
        flightNumber: shipment.flightNumber,
        flightDate: shipment.flightDate,
        finalized: shipment.finalized
      });

      this.shipmentForm.get('id')?.disable(); // Disable the id field
      this.shipmentSaved = true;
      this.shipmentId = shipment.id || '';
      this.isEditing = true; // Set editing flag
    });
  }
  
  finalizeShipment(shipmentId: string): void {
    if (confirm('Are you sure you want to finalize this shipment? This action cannot be undone.')) {
      this.shipmentService.finalizeShipment(shipmentId).subscribe({
        next: () => {
          const shipment = this.shipments.find(s => s.id === shipmentId);
          if (shipment) {
            shipment.finalized = true;
          }
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
    }
  }
  resetShipmentForm(): void {
    this.shipmentForm.reset();
    this.shipmentSaved = false;
    this.isEditing = false; // Reset editing flag
    this.shipmentForm.get('id')?.enable(); // Re-enable the id field
  }
}
