export class Shipment {
    id?: string;
    airportCode: string = '';
    flightNumber: string = '';
    flightDate: Date = new Date();
    finalized: boolean = false;
}