import { Component, Input, Self } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MdbValidationModule } from 'mdb-angular-ui-kit/validation';
import { MdbFormsModule } from 'mdb-angular-ui-kit/forms';
import {
  ControlValueAccessor,
  FormControl,
  NgControl,
  ReactiveFormsModule,
} from '@angular/forms';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [
    CommonModule,
    MdbValidationModule,
    MdbFormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
})
export class InputComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() inputType = 'text';
  @Input() name = '';

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {}

  registerOnChange(fn: any): void {}

  registerOnTouched(fn: any): void {}

  get control(): FormControl {
    return this.ngControl.control as FormControl;
  }

  get requiredError(): boolean {
    return (
      this.control.errors?.['required'] &&
      (this.control?.dirty || this.control?.touched) &&
      !this.control.errors?.['minlength'] &&
      !this.control.errors?.['maxlength'] &&
      !this.control.errors?.['min'] &&
      !this.control.errors?.['passwordMismatch'] &&
      !this.control.errors?.['categoryNameTaken']
    );
  }

  get minLengthError(): boolean {
    return (
      this.control.errors?.['minlength'] &&
      (this.control?.dirty || this.control?.touched) &&
      !this.control.errors?.['required'] &&
      !this.control.errors?.['maxlength'] &&
      !this.control.errors?.['min'] &&
      !this.control.errors?.['passwordMismatch'] &&
      !this.control.errors?.['invalidPhoneNumber'] &&
      !this.control.errors?.['weakPassword'] &&
      !this.control.errors?.['categoryNameTaken']
    );
  }

  get maxLengthError(): boolean {
    return (
      this.control.errors?.['maxlength'] &&
      (this.control?.dirty || this.control?.touched) &&
      !this.control.errors?.['minlength'] &&
      !this.control.errors?.['required'] &&
      !this.control.errors?.['min'] &&
      !this.control.errors?.['passwordMismatch'] &&
      !this.control.errors?.['invalidPhoneNumber'] &&
      !this.control.errors?.['weakPassword'] &&
      !this.control.errors?.['categoryNameTaken']
    );
  }

  get minError(): boolean {
    return (
      this.control.errors?.['min'] &&
      (this.control?.dirty || this.control?.touched) &&
      !this.control.errors?.['minlength'] &&
      !this.control.errors?.['maxlength'] &&
      !this.control.errors?.['required'] &&
      !this.control.errors?.['passwordMismatch'] &&
      !this.control.errors?.['invalidPhoneNumber'] &&
      !this.control.errors?.['weakPassword'] &&
      !this.control.errors?.['categoryNameTaken']
    );
  }

  get passwordMismatchError(): boolean {
    return (
      this.control.errors?.['passwordMismatch'] &&
      (this.control?.dirty || this.control?.touched) &&
      !this.control.errors?.['minlength'] &&
      !this.control.errors?.['maxlength'] &&
      !this.control.errors?.['min'] &&
      !this.control.errors?.['required'] &&
      !this.control.errors?.['invalidPhoneNumber'] &&
      !this.control.errors?.['weakPassword'] &&
      !this.control.errors?.['categoryNameTaken']
    );
  }

  get invalidPhoneNumberError(): boolean {
    return (
      this.control.errors?.['invalidPhoneNumber'] &&
      (this.control?.dirty || this.control?.touched) &&
      !this.control.errors?.['minlength'] &&
      !this.control.errors?.['maxlength'] &&
      !this.control.errors?.['min'] &&
      !this.control.errors?.['passwordMismatch'] &&
      !this.control.errors?.['required'] &&
      !this.control.errors?.['weakPassword'] &&
      !this.control.errors?.['categoryNameTaken']
    );
  }

  get categoryNameTakenError(): boolean {
    return (
      this.control.errors?.['categoryNameTaken'] &&
      (this.control?.dirty || this.control?.touched) &&
      !this.control.errors?.['minlength'] &&
      !this.control.errors?.['maxlength'] &&
      !this.control.errors?.['min'] &&
      !this.control.errors?.['passwordMismatch'] &&
      !this.control.errors?.['invalidPhoneNumber'] &&
      !this.control.errors?.['weakPassword'] &&
      !this.control.errors?.['required']
    );
  }

  get weakPasswordError(): boolean {
    return (
      this.control.errors?.['weakPassword'] &&
      (this.control?.dirty || this.control?.touched) &&
      !this.control.errors?.['minlength'] &&
      !this.control.errors?.['maxlength'] &&
      !this.control.errors?.['min'] &&
      !this.control.errors?.['passwordMismatch'] &&
      !this.control.errors?.['invalidPhoneNumber'] &&
      !this.control.errors?.['required'] &&
      !this.control.errors?.['categoryNameTaken']
    );
  }
}
