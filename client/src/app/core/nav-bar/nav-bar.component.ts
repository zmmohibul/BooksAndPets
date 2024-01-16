import { Component, TemplateRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MdbDropdownModule } from 'mdb-angular-ui-kit/dropdown';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MdbRippleDirective, MdbRippleModule } from 'mdb-angular-ui-kit/ripple';
import { SidebarService } from '../../services/sidebar.service';
import { Router, RouterLink } from '@angular/router';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [CommonModule, MdbDropdownModule, MdbRippleModule, RouterLink],
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent {
  sidebarInRoutes = ['/books', '/pets'];
  modalRef?: BsModalRef;
  constructor(
    private modalService: BsModalService,
    private sidebarService: SidebarService,
    public cartService: CartService,
    public router: Router,
  ) {}

  openModal(template: TemplateRef<void>) {
    this.modalRef = this.modalService.show(template);
  }

  toggleBar() {
    this.sidebarService.toggleBar();
  }
}
