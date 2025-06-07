export interface InvoiceItem {
    productId: number;
    quantity: number;
    discount?: number;
  }
  
  export interface Invoice {
    transactionDate: Date;
    items: InvoiceItem[];
  }
  