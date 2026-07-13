import { Component, effect, inject, input, output, signal } from '@angular/core';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { LucideCircleX, LucideClipboardPenLine, LucideGitPullRequestCreate } from '@lucide/angular';
import { form, FormField, required } from '@angular/forms/signals';
import { BranchStore } from '../../state/branch.store';
import { CreateBranchRequest, UpdateBranchRequest } from '../../model/branch-request.model';

type BranchFormModel = UpdateBranchRequest & { code: string };

@Component({
  selector: 'app-branch-form',
  imports: [LucideCircleX, LucideClipboardPenLine, LucideGitPullRequestCreate, FormField],
  templateUrl: './branch-form.component.html',
})
export class BranchFormComponent {
  //====================================
  //===========inject===================
  //====================================
  protected ref = inject(DynamicDialogRef);
  private branchStore = inject(BranchStore);

  //====================================
  //===========input===================
  //====================================
  title = input.required<string>();
  description = input.required<string>();
  type = input.required<'update' | 'create'>();

  onFormSubmit = output<CreateBranchRequest | UpdateBranchRequest>();

  //====================================
  //===========form=====================
  //====================================
  branchModel = signal<BranchFormModel>(this.getEmptyForm());

  constructor() {
    effect(() => {
      const details = this.branchStore.branchDetails();

      if (this.type() === 'create' || !details) {
        this.branchModel.set(this.getEmptyForm());
        return;
      }

      this.branchModel.set({
        name: details.name,
        code: details.code,
        phoneNumber: details.phoneNumber,
        isActive: details.isActive,
        address: {
          city: details.address?.city || '',
          country: details.address?.country || '',
          street: details.address?.street || '',
          addressType: details.address?.addressType,
        },
      });
    });
  }

  branchForm = form(this.branchModel, (schemaPath) => {
    required(schemaPath.name);
    required(schemaPath.phoneNumber);
    required(schemaPath.code);
    required(schemaPath.address.street);
    required(schemaPath.address.city);
    required(schemaPath.address.country);
  });

  //====================================
  //===========method===================
  //====================================
  private getEmptyForm(): BranchFormModel {
    return {
      name: '',
      code: '',
      phoneNumber: '',
      isActive: true,
      address: {
        city: '',
        country: '',
        street: '',
      },
    };
  }

  closeDialog() {
    this.ref.close();
  }

  onSubmit(event: Event) {
    event.preventDefault();

    if (this.branchForm().invalid()) {
      return;
    }

    const finalData = this.branchModel();

    if (this.type() === 'create') {
      this.onFormSubmit.emit({
        name: finalData.name,
        code: finalData.code,
        phoneNumber: finalData.phoneNumber,
        address: finalData.address,
      });
      return;
    }

    this.onFormSubmit.emit({
      name: finalData.name,
      phoneNumber: finalData.phoneNumber,
      isActive: finalData.isActive,
      address: finalData.address,
    });
  }
}
