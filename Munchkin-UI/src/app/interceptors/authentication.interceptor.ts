import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse,
  HttpStatusCode,
} from '@angular/common/http';
import { catchError, filter, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private state: ActivatedRoute,
    private authService: AuthenticationService
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      filter((event) => event instanceof HttpResponse),
      catchError(async (error: HttpErrorResponse) => {
        if (error.status == HttpStatusCode.Unauthorized) {
          this.authService.clearUserData();
          await this.router.navigate(['/sign-in']);
        }
        throw error;
      })
    );
  }
}
