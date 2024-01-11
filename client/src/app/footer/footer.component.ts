import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MdbFormsModule} from "mdb-angular-ui-kit/forms";
import {MdbRippleModule} from "mdb-angular-ui-kit/ripple";

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule, MdbFormsModule, MdbRippleModule],
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {

}
