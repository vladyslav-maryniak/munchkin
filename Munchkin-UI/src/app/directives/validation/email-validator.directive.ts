import { Directive } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import {
  AbstractControl,
  AsyncValidator,
  NG_ASYNC_VALIDATORS,
  ValidationErrors,
} from '@angular/forms';
import {
  debounceTime,
  distinctUntilChanged,
  first,
  map,
  Observable,
  switchMap,
} from 'rxjs';
import { CheckEmail } from 'src/app/models/identity/check-email';

@Directive({
  selector: '[uniqueEmail][ngModel]',
  providers: [
    {
      provide: NG_ASYNC_VALIDATORS,
      useExisting: EmailValidatorDirective,
      multi: true,
    },
  ],
})
export class EmailValidatorDirective implements AsyncValidator {
  constructor(private authService: AuthenticationService) {}
  validate(
    control: AbstractControl
  ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {
    return control.valueChanges.pipe(
      debounceTime(1000),
      distinctUntilChanged(),
      switchMap((value) => this.authService.checkEmail(value)),
      map((result: CheckEmail) =>
        result.isUnique ? null : { emailIsNotUnique: true }
      ),
      first()
    );
  }
}
