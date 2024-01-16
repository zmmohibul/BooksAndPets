import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { BsModalService } from 'ngx-bootstrap/modal';
import { MdbRippleDirective } from 'mdb-angular-ui-kit/ripple';
import { HttpClientModule, provideHttpClient } from '@angular/common/http';
import { CategoryService } from './services/category.service';
import { SidebarService } from './services/sidebar.service';
import { BookService } from './services/book.service';
import { PhotoGalleryModule } from '@twogate/ngx-photo-gallery';
import { CartService } from './services/cart.service';

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
    provideHttpClient(),
  ],
};
