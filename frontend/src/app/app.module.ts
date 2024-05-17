import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ParcelManagerComponent } from './components/parcel/parcel.component';
import { BagManagerComponent } from './components/bag/bag.component';
import { ShipmentComponent } from './components/shipment/shipment.component';

@NgModule({
  declarations: [
    AppComponent,
    ParcelManagerComponent,
    ShipmentComponent,
    BagManagerComponent,
    ShipmentComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
