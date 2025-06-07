import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http'; // ✅ NEW WAY for standalone

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient() // ✅ This replaces HttpClientModule
  ]
}).catch(err => console.error(err));
