import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BagService } from '../../services/bag.service';
import { BagWithLetters } from '../../models/bag-with-letters.model';
import { BagWithParcels } from '../../models/bag-with-parcels.model';
import { ParcelService } from '../../services/parcel.service';

@Component({
  selector: 'app-bag-manager',
  templateUrl: './bag.component.html',
  styleUrls: ['./bag.component.css']
})
export class BagManagerComponent implements OnInit {
  bagForm!: FormGroup;
  bags: any[] = [];
  currentParcels: any[] = [];
  bagId: string = '';
  bagSaved: boolean = false;
  @Input() shipmentId: string = '';
  @Output() resetShipmentForm = new EventEmitter<void>();

  constructor(private bagService: BagService, private parcelService: ParcelService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initForm();
    this.bagForm.get('bagType')?.valueChanges.subscribe(value => {
      this.updateFormControls(value);
    });
    this.showBags();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['bagType']) {
      console.log("bagtype changed to:", this.bagForm.get('bagType')?.value);
    }
    if (changes['shipmentId'] && changes['shipmentId'].currentValue) {
      this.loadBags(changes['shipmentId'].currentValue);
    }
  }

  loadBags(shipmentId: string): void {
    this.bagService.getBagsByShipmentId(shipmentId).subscribe(bags => {
      const bagFGs = bags.map(bag => this.fb.group(bag));
      const bagFormArray = this.fb.array(bagFGs);
      this.bagForm.setControl('bags', bagFormArray);
    });
  }

  initForm(): void {
    this.bagForm = this.fb.group({
      bagId: ['', [Validators.required, Validators.maxLength(15), Validators.pattern(/^[a-zA-Z0-9]*$/)]],
      bagType: ['', [Validators.required, Validators.pattern(/^(parcel|letter)$/)]],
      letterCount: ['', [Validators.required, Validators.min(1)]],
      weight: ['', [Validators.required, Validators.min(0.01), Validators.pattern(/^\d+(\.\d{1,3})?$/)]],
      price: ['', [Validators.required, Validators.min(0.01), Validators.pattern(/^\d+(\.\d{1,2})?$/)]]
    });
  }

  showBags(): void {
    this.bagService.getBags().subscribe(bags => {
      this.bags = bags;
      console.log('Bags:', bags);
    });
  }

  populateBagForm(bag: any): void {
    this.bagForm.patchValue({
      bagId: bag.id,
      bagType: bag.bagType,
    });
  }

  
  toggleParcels(bag: any): void {
    if (!bag.parcels) {
      bag.parcels = [];
      this.parcelService.getParcelsByBagId(bag.id).subscribe({
        next: (parcels) => {
          bag.parcels = parcels;
          bag.showParcels = true;
        },
        error: (error) => {
          console.error('Failed to load parcels:', error);
          bag.showParcels = true;
        }
      });
    } else if (bag.bagType === 'letter') {
      bag.showParcels = true;
    } else {
      bag.showParcels = !bag.showParcels;
    }
  }

  updateFormControls(type: string): void {
    if (type === 'parcel') {
      this.bagForm.removeControl('letterCount');
      this.bagForm.removeControl('weight');
      this.bagForm.removeControl('price');
      this.bagForm.addControl('parcels', this.fb.array([]));
    } else if (type === 'letter') {
      this.bagForm.removeControl('parcels');
      this.bagForm.addControl('letterCount', this.fb.control('', [Validators.required, Validators.min(1)]));
      this.bagForm.addControl('weight', this.fb.control('', [Validators.required, Validators.min(0.01), Validators.pattern(/^\d+(\.\d{1,3})?$/)]));
      this.bagForm.addControl('price', this.fb.control('', [Validators.required, Validators.min(0.01), Validators.pattern(/^\d+(\.\d{1,2})?$/)]));
    }
  }

  saveBag(): void {
    if (this.bagForm.invalid) {
      console.error('Form is not valid:', this.bagForm.errors);
      return;
    }

    if (this.bagForm.get('bagType')?.value === 'parcel') {
      this.saveParcelBag();
    } else {
      this.saveLetterBag();
    }
    this.bagSaved = true;
    this.showBags();
  }

  resetBagForm(): void {
    this.bagForm.reset();
    this.bagSaved = false;
  }

  saveParcelBag(): void {
    const parcelBagData: BagWithParcels = {
      id: this.bagForm.get('bagId')?.value,
      bagType: 'parcel',
      shipmentId: this.shipmentId,
    };
    this.bagService.createParcelBag(parcelBagData).subscribe({
      next: (response) => {
        console.log('Parcel bag saved successfully:', response);
        this.bagId = response.id as string;
      },
      error: (error) => console.error('Error saving parcel bag:', error)
    });
  }

  saveLetterBag(): void {
    const letterBagData: BagWithLetters = {
      id: this.bagForm.get('bagId')?.value,
      shipmentId: this.shipmentId,
      bagType: 'letter',
      countOfLetters: this.bagForm.get('letterCount')?.value,
      weight: this.bagForm.get('weight')?.value,
      price: this.bagForm.get('price')?.value
    };
    this.bagService.createLetterBag(letterBagData).subscribe({
      next: (response) => console.log('Letter bag saved successfully:', response),
      error: (error) => console.error('Error saving letter bag:', error)
    });
  }
}
