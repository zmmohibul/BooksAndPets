import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthenticationService } from '../../../services/authentication.service';
import { InputComponent } from '../../../core/input/input.component';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { CartService } from '../../../services/cart.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, InputComponent, ReactiveFormsModule],
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  showNewAddress = signal(true);
  showAddressForm = signal(false);
  addressForm: FormGroup = new FormGroup({});
  constructor(
    public authenticationService: AuthenticationService,
    public cartService: CartService,
    private formBuilder: FormBuilder,
    private router: Router,
  ) {}

  ngOnInit(): void {
    if (!this.authenticationService.user()) {
      this.router.navigateByUrl('/login');
    }

    if (!this.cartService.cartItems().length) {
      this.router.navigateByUrl('');
    }

    this.addressForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      street: ['', [Validators.required]],
      area: ['', [Validators.required]],
      city: ['', [Validators.required]],
      phoneNumber: [
        '',
        [
          Validators.required,
          Validators.pattern('(^([+]{1}[8]{2}|0088)?(01){1}[3-9]{1}\\d{8})$'),
          this.invalidPhoneNumber(),
        ],
      ],
    });
  }

  invalidPhoneNumber(): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.errors?.['pattern']?.requiredPattern
        ? { invalidPhoneNumber: true }
        : null;
    };
  }

  onFormSubmit(event: SubmitEvent) {
    event.preventDefault();
    const address = { ...this.addressForm.value };
    this.authenticationService.createAddress(address).subscribe({
      next: (response) => {
        this.showNewAddress.set(true);
        this.showAddressForm.set(false);
      },
    });
  }

  onNewAddressClick() {
    this.showNewAddress.set(false);
    this.showAddressForm.set(true);
  }
}
