import { Component, OnInit, signal, WritableSignal } from '@angular/core';
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
import { OrderSummaryComponent } from '../order-summary/order-summary.component';
import { ToastrService } from 'ngx-toastr';
import { OrderService } from '../../../services/order.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [
    CommonModule,
    InputComponent,
    ReactiveFormsModule,
    OrderSummaryComponent,
  ],
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  showNewAddress = signal(true);
  showAddressForm = signal(false);
  addressForm: FormGroup = new FormGroup({});

  addressId: WritableSignal<number | null> = signal(null);
  constructor(
    public authenticationService: AuthenticationService,
    public cartService: CartService,
    public orderService: OrderService,
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
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

  onAddressCardClick(id: number) {
    this.addressId.set(id);
  }

  onPlaceOrderClick() {
    if (!this.cartService.cartItems().length) {
      return;
    }

    const id = this.addressId();
    if (id === null) {
      this.toastr.info('Please select an address');
      return;
    }

    this.orderService.createOrder(id)?.subscribe({
      next: (response) => {
        this.cartService.cartItems.set([]);
        this.router.navigateByUrl('/orders');
      },
    });
  }
}
