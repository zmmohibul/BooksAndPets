import { Injectable, signal } from '@angular/core';
import { BehaviorSubject, take } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SidebarService {
  hideBar = signal(false);
  $hideBar = new BehaviorSubject<boolean>(false);
  constructor() {}

  toggleBar() {
    this.hideBar.set(!this.hideBar());
    let b = this.hideBar();

    this.$hideBar.pipe(take(1)).subscribe({
      next: (data) => {
        this.$hideBar.next(!data);
      },
    });
  }
}
