
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private apiUrl = 'https://localhost:7073/api/Invoice'; // backend endpoint

  constructor(private http: HttpClient) {} 

  createInvoice(invoiceData: any): Observable<any> {
    return this.http.post(this.apiUrl, invoiceData);
  }
}
