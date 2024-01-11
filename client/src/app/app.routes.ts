import { Routes } from '@angular/router';
import {HomeComponent} from "./store/home/home.component";

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: "full" }
];
