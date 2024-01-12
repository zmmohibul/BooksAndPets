import { Routes } from '@angular/router';
import {HomeComponent} from "./store/home/home.component";
import {BookHomeComponent} from "./store/book/book-home/book-home.component";

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: "full" },
  { path: 'books', component: BookHomeComponent },
];
