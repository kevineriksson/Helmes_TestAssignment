import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { AbstractControl, Form, FormArray, FormBuilder, FormGroup, FormRecord, ValidationErrors, Validators } from '@angular/forms';
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
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['bagId']) {
      }
  }

  private initForm(): void {
    this.parcelForm = this.fb.group({
      parcels: this.fb.array([])
    });
  }

  get parcels(): FormArray {
    return this.parcelForm.get('parcels') as FormArray;
  }

  addParcel(): void {
    const parcelGroup = this.fb.group({
      id: ['', [Validators.required, this.parcelIdValidator]],
      bagId: [this.bagId, Validators.required],
      recipientName: ['', [Validators.required, Validators.maxLength(100)]],
      destinationCountry: ['', [Validators.required, Validators.pattern(/^[A-Z]{2}$/)]],
      weight: [0, [Validators.required, Validators.min(0.1), Validators.pattern(/^\d+(\.\d{1,3})?$/)]],
      price: [0, [Validators.required, Validators.min(0.01), Validators.pattern(/^\d+(\.\d{1,2})?$/)]]
    });
    this.parcels.push(parcelGroup);
  }

  private parcelIdValidator(control: AbstractControl): ValidationErrors | null {
    const valid = /^[A-Za-z]{2}\d{6}[A-Za-z]{2}$/.test(control.value);
    return valid ? null : { invalidParcelId: { value: control.value } };
  }
  
  saveParcels(): void {
    if (this.parcelForm.valid) {
      let parcelsToSave = this.parcels.length;
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
              alert('Parcels saved successfully!');
            }
          },
          error: (error) => {
            console.error(`Error saving parcel ${index + 1}:`, error);
            savedCount++;
            alert('Form is invalid!');
            if (savedCount === parcelsToSave) {
              this.resetForm();
            }
          },
        });
      });
    } else {
      console.log('Form is not valid:', this.parcelForm.errors);
      console.log(this.bagId);
    }
  }
  
  private resetForm(): void {
    this.parcelForm.reset();
    this.resetBagForm.emit();
  }
}
