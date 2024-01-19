import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MdbRippleDirective } from 'mdb-angular-ui-kit/ripple';
import {
  HTTP_INTERCEPTORS,
  HttpClientModule,
  provideHttpClient,
  withInterceptors,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { CategoryService } from './services/category.service';
import { SidebarService } from './services/sidebar.service';
import { BookService } from './services/book.service';
import { PhotoGalleryModule } from '@twogate/ngx-photo-gallery';
import { CartService } from './services/cart.service';
import { provideToastr } from 'ngx-toastr';
import { AuthenticationService } from './services/authentication.service';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    BsModalService,
    MdbRippleDirective,
    CategoryService,
    SidebarService,
    BookService,
    CartService,
    AuthenticationService,
    provideAnimations(),
    provideToastr(),
    provideHttpClient(withInterceptorsFromDi()),
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
};
