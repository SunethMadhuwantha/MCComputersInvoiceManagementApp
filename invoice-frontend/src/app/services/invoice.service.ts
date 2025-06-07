// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { Invoice } from '../models/invoice.model';
// import { Observable } from 'rxjs';

// @Injectable({
//   providedIn: 'root'
// })
// export class InvoiceService {
//   private apiUrl = 'https://localhost:7073/api/invoices'; // Adjust to your backend

//   constructor(private http: HttpClient) {}

//   submitInvoice(invoice: Invoice): Observable<any> {
//     return this.http.post(this.apiUrl, invoice);
//   }
// }
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private apiUrl = 'https://localhost:7073/api/Invoice'; // Change this to your actual API endpoint

  constructor(private http: HttpClient) {}

  createInvoice(invoiceData: any): Observable<any> {
    return this.http.post(this.apiUrl, invoiceData);
  }
}
