import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IdentityError } from 'src/app/models/identity/identity-error';
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
  pwdConfirmation!: string;

  returnUrl!: string;

  errors: IdentityError[] = [];

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'];
  }

  async onSubmit(): Promise<void> {
    if (this.password == this.pwdConfirmation) {
      const result = await this.authService.createUser(
        this.nickname,
        this.email,
        this.password
      );
      if (result.succeeded) {
        await this.router.navigateByUrl(this.returnUrl ?? '/');
      } else {
        this.errors = result.errors;
      }
    }
  }
}
