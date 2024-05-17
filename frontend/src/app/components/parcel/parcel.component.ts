import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { Form, FormArray, FormBuilder, FormGroup, FormRecord, Validators } from '@angular/forms';
import { Parcel } from '../../models/parcel.model';
import { ParcelService } from '../../services/parcel.service';
import { BagManagerComponent } from '../bag/bag.component';

@Component({
  selector: 'app-parcel-manager',
  templateUrl: './parcel.component.html',})
export class ParcelManagerComponent implements OnInit {
  @Input() bagId: string = '';
  @Output() resetBagForm = new EventEmitter<void>();
  parcelForm: FormGroup = new FormGroup({});
  
  constructor(private fb: FormBuilder, private parcelService: ParcelService) {}


  ngOnInit(): void {
    this.initForm();
    console.log("Received bagId:", this.bagId);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['bagId']) {
      console.log("bagId changed to:", this.bagId);
      }
  }

  initForm(): void {
    this.parcelForm = this.fb.group({
      parcels: this.fb.array([])
    });
  }

  get parcels(): FormArray {
    return this.parcelForm.get('parcels') as FormArray;
  }

  addParcel(): void {
    const parcelGroup = this.fb.group({
      id: ['', Validators.required],
      bagId: [this.bagId, Validators.required],
      recipientName: ['', Validators.required],
      destinationCountry: ['', [Validators.required, Validators.maxLength(2)]],
      weight: [0, [Validators.required, Validators.min(0.1)]],
      price: [0, [Validators.required, Validators.min(0.01)]]
    });
    this.parcels.push(parcelGroup);
  }

  saveParcels(): void {
    if (this.parcelForm.valid) {
      let parcelsToSave = this.parcels.controls.length;
      let savedCount = 0;
  
      this.parcels.controls.forEach((parcelGroup, index) => {
        console.log('Sending parcel data:', parcelGroup.value);
        this.parcelService.createParcel(parcelGroup.value).subscribe({
          next: (response) => {
            console.log(`Parcel ${index + 1} saved successfully:`, response);
            savedCount++;
  
            if (savedCount === parcelsToSave) {
              this.parcelForm.reset();
              this.resetBagForm.emit();            
            }
          },
          error: (error) => {
            console.error(`Error saving parcel ${index + 1}:`, error);
            savedCount++;
  
            if (savedCount === parcelsToSave) {
              this.parcelForm.reset();
            }
          },
        });
      });
    } else {
      console.log('Form is not valid:', this.parcelForm.errors);
      console.log(this.bagId);
    }
  }
}
