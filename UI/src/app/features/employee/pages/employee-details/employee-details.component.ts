import { Component, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { HlmButton } from '@spartan-ng/helm/button';
import { LucideArrowRight, LucideKeyRound, LucideUserRound } from '@lucide/angular';
import { EmployeeStore } from '../../store/employee.store';
import { toast } from '@spartan-ng/brain/sonner';

@Component({
  selector: 'app-employee-details',
  standalone: true,
  imports: [DatePipe, HeaderComponent, HlmButton, LucideArrowRight, LucideKeyRound, LucideUserRound],
  templateUrl: './employee-details.component.html',
  styleUrl: './employee-details.component.scss',
})
export class EmployeeDetailsComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  protected employeeStore = inject(EmployeeStore);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  protected showPasswordForm = signal(false);
  protected newPassword = signal('');

  //=========================================
  //==============life cycle=================
  //=========================================
  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.employeeStore.loadEmployeeById(id);
  }

  //=========================================
  //==============method=====================
  //=========================================
  goBack() {
    this.employeeStore.clearSelectedEmployee();
    this.router.navigate(['/staff/employee']);
  }

  updatePasswordValue(event: Event) {
    this.newPassword.set((event.target as HTMLInputElement).value);
  }

  submitPassword(event: Event) {
    event.preventDefault();
    const employee = this.employeeStore.selectedEmployee();
    if (!employee || this.newPassword().length < 6) {
      toast.error('كلمة المرور يجب أن لا تقل عن 6 أحرف');
      return;
    }

    this.employeeStore.changePassword({ id: employee.id, password: this.newPassword() });
    this.newPassword.set('');
    this.showPasswordForm.set(false);
    toast.success('تم تغيير كلمة المرور بنجاح');
  }
}
