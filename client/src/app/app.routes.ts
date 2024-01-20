import { Routes } from '@angular/router';
import { HomeComponent } from './store/home/home.component';
import { BookHomeComponent } from './store/book/book-home/book-home.component';
import { BookDetailsComponent } from './store/book/book-details/book-details.component';
import { LoginComponent } from './authentication/login/login.component';
import { RegisterComponent } from './authentication/register/register.component';
import { CheckoutComponent } from './store/order/checkout/checkout.component';
import { OrderListComponent } from './store/order/order-list/order-list.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'books', component: BookHomeComponent },
  { path: 'books/:id', component: BookDetailsComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: 'orders', component: OrderListComponent },
];
