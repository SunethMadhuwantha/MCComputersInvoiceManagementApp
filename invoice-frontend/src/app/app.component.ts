import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { InvoiceFormComponent } from './components/invoice-form/invoice-form.component'; // if you're using this

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, InvoiceFormComponent], // ✅ no HttpClientModule here
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'invoice-frontend';
}
