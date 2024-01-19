import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { MdbCollapseModule } from 'mdb-angular-ui-kit/collapse';
import { MdbDropdownModule } from 'mdb-angular-ui-kit/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarComponent } from './core/nav-bar/nav-bar.component';
import { FooterComponent } from './core/footer/footer.component';
import { CartService } from './services/cart.service';
import { CartComponent } from './store/cart/cart.component';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    MdbCollapseModule,
    MdbDropdownModule,
    NavBarComponent,
    FooterComponent,
    CartComponent,
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(
    public cartService: CartService,
    public authenticationService: AuthenticationService,
  ) {}

  ngOnInit(): void {
    this.authenticationService.setUserIfLoggedIn();
    console.log(this.authenticationService.userDetails());
  }
}
