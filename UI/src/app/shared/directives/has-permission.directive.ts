import { Directive, effect, inject, input, TemplateRef, ViewContainerRef } from '@angular/core';
import { AuthStore } from '../../features/auth/state/auth.store';

@Directive({
  selector: '[appHasPermission]',
})
export class HasPermissionDirective {
  private templateRef = inject(TemplateRef<any>);
  private viewContainer = inject(ViewContainerRef);
  private authStore = inject(AuthStore);

  appHasPermission = input.required<string>();

  constructor() {
    effect(() => {
      const requiredPermission = this.appHasPermission();
      const hasAccess = this.authStore.hasPermission()(requiredPermission);

      if (hasAccess) {
        if (this.viewContainer.length === 0) {
          this.viewContainer.createEmbeddedView(this.templateRef);
        }
      } else {
        this.viewContainer.clear();
      }
    });
  }
}
