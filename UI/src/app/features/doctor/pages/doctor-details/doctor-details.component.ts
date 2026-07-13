import { Component, inject, OnInit, signal } from '@angular/core';
import { EmployeeStore } from '../../../employee/store/employee.store';
import { ActivatedRoute, Router } from '@angular/router';
import { toast } from '@spartan-ng/brain/sonner';
import { DatePipe } from '@angular/common';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { HlmButton } from '@spartan-ng/helm/button';
import { LucideArrowRight, LucideKeyRound, LucideUserRound } from '@lucide/angular';

@Component({
  selector: 'app-doctor-details',
  imports: [
    DatePipe,
    HeaderComponent,
    HlmButton,
    LucideArrowRight,
    LucideKeyRound,
    LucideUserRound,
  ],
  templateUrl: './doctor-details.component.html',
  styleUrl: './doctor-details.component.scss',
})
export class DoctorDetailsComponent implements OnInit {
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
    this.employeeStore.loadDoctorById(id);
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
    const doctor = this.employeeStore.selectedDoctor();
    if (!doctor || this.newPassword().length < 6) {
      toast.error('كلمة المرور يجب أن لا تقل عن 6 أحرف');
      return;
    }

    this.employeeStore.changeDoctorPassword({ id: doctor.id, password: this.newPassword() });
    this.newPassword.set('');
    this.showPasswordForm.set(false);
    toast.success('تم تغيير كلمة المرور بنجاح');
  }
}
