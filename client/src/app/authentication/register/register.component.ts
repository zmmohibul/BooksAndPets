import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  ValidatorFn,
  Validators,
} from '@angular/forms';

import { LoginModel } from '../../models/authentication/loginModel';
import { AuthenticationService } from '../../services/authentication.service';
import { ToastrService } from 'ngx-toastr';
import { Router, RouterLink } from '@angular/router';
import { InputComponent } from '../../core/input/input.component';
import { MdbRippleModule } from 'mdb-angular-ui-kit/ripple';
import { RegisterModel } from '../../models/authentication/registerModel';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    InputComponent,
    MdbRippleModule,
    ReactiveFormsModule,
    RouterLink,
  ],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({});
  model: RegisterModel = {
    userName: '',
    password: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
  };
  loading = false;

  constructor(
    public authenticationService: AuthenticationService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      userName: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(32),
        ],
      ],
      firstName: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(32),
        ],
      ],
      lastName: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(32),
        ],
      ],
      phoneNumber: [
        '',
        [
          Validators.required,
          Validators.pattern('(^([+]{1}[8]{2}|0088)?(01){1}[3-9]{1}\\d{8})$'),
          this.invalidPhoneNumber(),
        ],
      ],
      password: [
        '',
        [
          Validators.required,
          Validators.pattern(
            '^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$',
          ),
          this.weakPassword(),
        ],
      ],
      confirmPassword: [
        '',
        [Validators.required, this.matchValues('password')],
      ],
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.get(matchTo)?.value
        ? null
        : { passwordMismatch: true };
    };
  }

  weakPassword(): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.errors?.['pattern']?.requiredPattern &&
        !control?.errors?.['required']
        ? { weakPassword: true }
        : null;
    };
  }

  invalidPhoneNumber(): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.errors?.['pattern']?.requiredPattern
        ? { invalidPhoneNumber: true }
        : null;
    };
  }

  onFormSubmit(event: SubmitEvent) {
    console.log(this.registerForm.controls['password']);
    event.preventDefault();

    if (this.registerForm.invalid) {
      return;
    }

    const formValues = { ...this.registerForm.value };
    if (formValues.password !== formValues.confirmPassword) {
      this.registerForm.controls['password'].setErrors({
        passwordMismatch: true,
      });
      this.registerForm.controls['confirmPassword'].setErrors({
        passwordMismatch: true,
      });
      return;
    }

    this.model = { ...formValues };
    this.authenticationService.register(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl(this.authenticationService.lastPage);
      },
    });
  }
}
