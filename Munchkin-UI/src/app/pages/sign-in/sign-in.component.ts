import { Component, OnInit } from '@angular/core';
import { NgForm, NgModel, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
})
export class SignInComponent implements OnInit {
  login!: string;
  password!: string;
  rememberMe = false;

  returnUrl!: string;
  regexp!: RegExp;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.regexp = new RegExp(
      /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'];
  }

  async onSubmit(form: NgForm) {
    const isEmail: boolean = this.regexp.test(this.login);
    let email = '';
    let nickname = '';
    if (isEmail) {
      email = this.login;
    } else {
      nickname = this.login;
    }

    const checkLoginResult = isEmail
      ? await this.authService.checkEmail(email)
      : await this.authService.checkNickname(nickname);

    if (checkLoginResult.isUnique) {
      form.controls['login-input'].setErrors({ noLogin: true });
      return;
    }

    const checkPasswordResult = await this.authService.checkPassword(
      nickname,
      email,
      this.password
    );

    if (checkPasswordResult.canSignIn === false) {
      form.controls['password-input'].setErrors({ incorrectPassword: true });
      return;
    }

    if (checkLoginResult && checkPasswordResult) {
      const signInResult = await this.authService.signInUser(
        nickname,
        email,
        this.password,
        this.rememberMe
      );
      if (signInResult.succeeded) {
        await this.router.navigateByUrl(this.returnUrl ?? '/');
      }
    }
  }

  onLoginInputChange(ngModel: NgModel) {
    if (ngModel.hasError('incorrectPassword')) {
      delete ngModel.errors?.['incorrectPassword'];
      if (ngModel.errors && Object.keys(ngModel.errors).length === 0) {
        ngModel.control.setErrors(null);
      }
    }
  }

  onPasswordInputChange(ngModel: NgModel) {
    if (ngModel.hasError('noLogin')) {
      delete ngModel.errors?.['noLogin'];
      if (ngModel.errors && Object.keys(ngModel.errors).length === 0) {
        ngModel.control.setErrors(null);
      }
    }
  }

  getFirstErrorDescription(
    inputName: string,
    errors: ValidationErrors | null
  ): string {
    if (errors?.['required']) {
      return `${inputName} is required`;
    }
    if (errors?.['minlength']) {
      return `${inputName} is too short`;
    }
    if (errors?.['pattern']) {
      return `Login contains not allowed character`;
    }
    if (errors?.['noLogin']) {
      return 'No player with specified login';
    }
    if (errors?.['incorrectPassword']) {
      return 'Password is incorrect';
    }
    return '';
  }
}
