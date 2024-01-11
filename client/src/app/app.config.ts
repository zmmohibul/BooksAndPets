import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {provideAnimations} from "@angular/platform-browser/animations";
import {BsModalService} from "ngx-bootstrap/modal";
import {MdbRippleDirective} from "mdb-angular-ui-kit/ripple";

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideAnimations(), BsModalService, MdbRippleDirective]
};
