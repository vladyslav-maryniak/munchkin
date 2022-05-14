import { Component, OnInit } from '@angular/core';
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

  async onSubmit() {
    const isEmail: boolean = this.regexp.test(this.login);
    let email = '';
    let nickname = '';
    if (isEmail) {
      email = this.login;
    } else {
      nickname = this.login;
    }

    const isSucceeded = await this.authService.signInUser(
      nickname,
      email,
      this.password,
      this.rememberMe
    );

    if (isSucceeded) {
      await this.router.navigateByUrl(this.returnUrl ?? '/');
    }
  }
}
