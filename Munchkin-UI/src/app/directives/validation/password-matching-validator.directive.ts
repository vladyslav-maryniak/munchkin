import { Directive, Input } from '@angular/core';
import {
  FormGroup,
  NG_VALIDATORS,
  ValidationErrors,
  Validator,
} from '@angular/forms';

@Directive({
  selector: '[passwordMatching]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: PasswordMatchingValidatorDirective,
      multi: true,
    },
  ],
})
export class PasswordMatchingValidatorDirective implements Validator {
  @Input('passwordMatching') passwordMatching: string[] = [];

  validate(formGroup: FormGroup): ValidationErrors | null {
    const control = formGroup.controls[this.passwordMatching[0]];
    const matchingControl = formGroup.controls[this.passwordMatching[1]];

    if (control && matchingControl) {
      if (matchingControl.value && control.value !== matchingControl.value) {
        matchingControl.setErrors({ passwordsDoNotMatch: true });
      } else {
        delete matchingControl.errors?.['passwordsDoNotMatch'];
        if (
          matchingControl.errors &&
          Object.keys(matchingControl.errors).length === 0
        ) {
          matchingControl.setErrors(null);
        }
      }
    }
    return null;
  }
}
