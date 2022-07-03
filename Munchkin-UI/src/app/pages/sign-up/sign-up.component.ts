import { Component, OnInit } from '@angular/core';
import { ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
})
export class SignUpComponent implements OnInit {
  nickname!: string;
  email!: string;
  password!: string;
  passwordConfirmation!: string;

  returnUrl!: string;

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'];
  }

  async onSubmit(): Promise<void> {
    if (this.password == this.passwordConfirmation) {
      const result = await this.authService.createUser(
        this.nickname,
        this.email,
        this.password
      );
      if (result.succeeded) {
        await this.router.navigateByUrl(this.returnUrl ?? '/');
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
    if (errors?.['email']) {
      return 'Email is invalid';
    }
    if (errors?.['nicknameIsNotUnique']) {
      return 'Nickname is already taken';
    }
    if (errors?.['emailIsNotUnique']) {
      return 'Email is already taken';
    }
    if (errors?.['passwordsDoNotMatch']) {
      return 'Passwords do not match';
    }
    return '';
  }
}
