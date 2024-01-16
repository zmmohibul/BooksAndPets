import { Routes } from '@angular/router';
import { HomeComponent } from './store/home/home.component';
import { BookHomeComponent } from './store/book/book-home/book-home.component';
import { BookDetailsComponent } from './store/book/book-details/book-details.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'books', component: BookHomeComponent },
  { path: 'books/:id', component: BookDetailsComponent },
];
