import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationGuard implements CanActivate {
  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> {
    const result = await this.authService.checkSignIn();
    if (result.authenticated && this.isSignPage(route)) {
      await this.router.navigate(['/']);
      return false;
    }
    if (!result.authenticated && !this.isSignPage(route)) {
      await this.router.navigate(['/sign-in'], {
        queryParams: { returnUrl: state.url },
      });
      return false;
    }
    return true;
  }

  isSignPage(route: ActivatedRouteSnapshot) {
    const activatingRoute = route.url.join('/');
    return activatingRoute == 'sign-in' || activatingRoute == 'sign-up';
  }
}
