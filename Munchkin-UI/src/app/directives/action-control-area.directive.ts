import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[actionControlArea]',
})
export class ActionControlAreaDirective {
  constructor(public viewContainerRef: ViewContainerRef) {}
}
