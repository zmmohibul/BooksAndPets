import {Component, TemplateRef} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MdbDropdownModule} from "mdb-angular-ui-kit/dropdown";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {MdbRippleDirective, MdbRippleModule} from "mdb-angular-ui-kit/ripple";

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [CommonModule, MdbDropdownModule, MdbRippleModule],
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  modalRef?: BsModalRef;
  constructor(private modalService: BsModalService) {}

  openModal(template: TemplateRef<void>) {
    this.modalRef = this.modalService.show(template);
  }
}
