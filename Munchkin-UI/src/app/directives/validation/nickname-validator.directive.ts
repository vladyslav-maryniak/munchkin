import { Directive } from '@angular/core';
import { CheckNickname } from 'src/app/models/identity/check-nickname';
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

@Directive({
  selector: '[uniqueNickname][ngModel]',
  providers: [
    {
      provide: NG_ASYNC_VALIDATORS,
      useExisting: NicknameValidatorDirective,
      multi: true,
    },
  ],
})
export class NicknameValidatorDirective implements AsyncValidator {
  constructor(private authService: AuthenticationService) {}
  validate(
    control: AbstractControl
  ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {
    return control.valueChanges.pipe(
      debounceTime(1000),
      distinctUntilChanged(),
      switchMap((value) => this.authService.checkNickname(value)),
      map((result: CheckNickname) =>
        result.isUnique ? null : { nicknameIsNotUnique: true }
      ),
      first()
    );
  }
}
