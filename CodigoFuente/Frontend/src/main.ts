import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { provideHotToastConfig } from '@ngneat/hot-toast';

bootstrapApplication(AppComponent, appConfig).catch((err) =>
  console.error(err)
);
