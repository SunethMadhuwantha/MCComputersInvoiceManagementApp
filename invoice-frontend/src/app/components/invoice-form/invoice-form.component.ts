import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormsModule, FormArray } from '@angular/forms';
import { InvoiceService } from '../../services/invoice.service';
import { ProductService } from '../../services/product.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-invoice-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule, FormsModule],
  templateUrl: './invoice-form.component.html',
  styleUrls: ['./invoice-form.component.scss']
})
export class InvoiceFormComponent implements OnInit {
  invoiceForm!: FormGroup;
  products: any[] = [];

  constructor(
    private fb: FormBuilder,
    private invoiceService: InvoiceService,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.invoiceForm = this.fb.group({
      transactionDate: ['', Validators.required],
      items: this.fb.array([this.createItem()]),
      discount: [0],
      totalAmount: [{ value: 0, disabled: true }],
      balanceAmount: [{ value: 0, disabled: true }]
    });

    this.productService.getProducts().subscribe(data => {
      this.products = data;
    });

    this.invoiceForm.valueChanges.subscribe(() => this.updateTotal());
  }

  get items(): FormArray {
    return this.invoiceForm.get('items') as FormArray;
  }

  createItem(): FormGroup {
    return this.fb.group({
      productId: [null, Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]]
    });
  }

  addItem(): void {
    this.items.push(this.createItem());
  }

  removeItem(index: number): void {
    this.items.removeAt(index);
  }

  updateTotal() {
    const items = this.items.value;
    const discount = this.invoiceForm.get('discount')?.value || 0;

    let total = 0;

    for (let item of items) {
      const product = this.products.find(p => p.productId == item.productId);
      if (product && item.quantity > 0) {
        total += product.price * item.quantity;
      }
    }

    const discountAmount = (total * discount) / 100;
    const finalTotal = total - discountAmount;

    this.invoiceForm.patchValue({
      totalAmount: total,
      balanceAmount: finalTotal
    }, { emitEvent: false });
  }

  submitInvoice() {
    this.invoiceForm.markAllAsTouched();
    if (this.invoiceForm.valid) {
      const invoiceData = this.invoiceForm.getRawValue();
      this.invoiceService.createInvoice(invoiceData).subscribe({
        next: () => alert('Invoice submitted!'),
        error: () => alert('Error submitting invoice!')
      });
    }
  }
}