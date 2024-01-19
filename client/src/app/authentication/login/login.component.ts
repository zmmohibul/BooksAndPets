import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { LoginModel } from '../../models/authentication/loginModel';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { InputComponent } from '../../core/input/input.component';
import { MdbRippleModule } from 'mdb-angular-ui-kit/ripple';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, InputComponent, MdbRippleModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({});
  model: LoginModel = { userName: '', password: '' };
  loading = false;

  constructor(
    public authenticationService: AuthenticationService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  onFormSubmit(event: SubmitEvent) {
    event.preventDefault();

    if (this.loginForm.invalid) {
      return;
    }

    this.model = { ...this.loginForm.value };
    this.authenticationService.login(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl(this.authenticationService.lastPage);
      },
    });
  }
}
