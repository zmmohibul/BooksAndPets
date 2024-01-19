import { Component, OnInit, TemplateRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MdbDropdownModule } from 'mdb-angular-ui-kit/dropdown';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { MdbRippleDirective, MdbRippleModule } from 'mdb-angular-ui-kit/ripple';
import { SidebarService } from '../../services/sidebar.service';
import { Router, RouterLink } from '@angular/router';
import { CartService } from '../../services/cart.service';
import { AuthenticationService } from '../../services/authentication.service';
import { UserDetails } from '../../models/authentication/userDetails';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [CommonModule, MdbDropdownModule, MdbRippleModule, RouterLink],
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  userDetails: UserDetails | null = this.authenticationService.userDetails();
  sidebarInRoutes = ['/books', '/pets'];
  modalRef?: BsModalRef;
  constructor(
    public authenticationService: AuthenticationService,
    private modalService: BsModalService,
    private sidebarService: SidebarService,
    public cartService: CartService,
    public router: Router,
  ) {}

  onLogout() {
    this.authenticationService.logout();
  }

  openModal(template: TemplateRef<void>) {
    this.modalRef = this.modalService.show(template);
  }

  toggleBar() {
    this.sidebarService.toggleBar();
  }

  ngOnInit(): void {}
}
