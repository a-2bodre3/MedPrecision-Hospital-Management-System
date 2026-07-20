import { Component, inject, OnInit, signal } from '@angular/core';
import { DoctorStore } from '../../store/doctor.store';
import { ActivatedRoute, Router } from '@angular/router';
import { toast } from '@spartan-ng/brain/sonner';
import { DatePipe } from '@angular/common';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { HlmButton } from '@spartan-ng/helm/button';
import { LucideArrowRight, LucideKeyRound, LucideUserRound } from '@lucide/angular';

@Component({
  selector: 'app-doctor-details',
  standalone: true,
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
  protected doctorStore = inject(DoctorStore);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  protected showPasswordForm = signal(false);
  protected newPassword = signal('');

  //=========================================
  //==============life cycle=================
  //=========================================
  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.doctorStore.loadDoctorById(id);
  }

  //=========================================
  //==============method=====================
  //=========================================
  goBack() {
    this.doctorStore.clearSelectedDoctor();
    this.router.navigate(['/staff/doctor']);
  }

  updatePasswordValue(event: Event) {
    this.newPassword.set((event.target as HTMLInputElement).value);
  }

  submitPassword(event: Event) {
    event.preventDefault();
    const doctor = this.doctorStore.doctorDetails();
    if (!doctor || this.newPassword().length < 6) {
      toast.error('كلمة المرور يجب أن لا تقل عن 6 أحرف');
      return;
    }

    this.doctorStore.changePassword({ id: doctor.id, password: this.newPassword() });
    this.newPassword.set('');
    this.showPasswordForm.set(false);
    toast.success('تم تغيير كلمة المرور بنجاح');
  }
}
