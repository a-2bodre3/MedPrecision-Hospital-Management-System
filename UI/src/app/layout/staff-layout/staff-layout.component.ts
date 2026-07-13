import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { SidebarStaffComponent } from './sidebar-staff/sidebar-staff.component';
import { HlmSidebarImports } from '@spartan-ng/helm/sidebar';
import { LogoComponent } from '../../shared/components/logo/logo.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-staff-layout',
  imports: [SidebarStaffComponent, HlmSidebarImports, LogoComponent, RouterOutlet],
  templateUrl: './staff-layout.component.html',
  styleUrl: './staff-layout.component.scss',
})
export class StaffLayoutComponent implements OnInit, OnDestroy {
  currentDate = signal<string>('');
  currentTime = signal<string>('');
  private timerId: any;
  ngOnInit() {
    this.updateDateTime();

    this.timerId = setInterval(() => {
      this.updateDateTime();
    }, 1000);
  }
  ngOnDestroy() {
    if (this.timerId) {
      clearInterval(this.timerId);
    }
  }

  private updateDateTime() {
    const now = new Date();

    const datePart = new Intl.DateTimeFormat('ar-EG', {
      weekday: 'long',
      day: 'numeric',
      month: 'long',
      year: 'numeric',
    }).format(now);

    const timePart = new Intl.DateTimeFormat('ar-EG', {
      hour: 'numeric',
      minute: 'numeric',
      hour12: true,
    }).format(now);

    this.currentDate.set(datePart);
    this.currentTime.set(timePart);
  }
}
