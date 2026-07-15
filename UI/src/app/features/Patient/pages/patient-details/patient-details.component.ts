// import { Component, inject, OnInit, signal } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { PatientStore } from '../../store/patient.store';
// import { DatePipe } from '@angular/common';
// import { HeaderComponent } from '../../../../shared/components/header/header.component';
// import {
//   LucideArrowRight,
//   LucideUserRound,
//   LucideCalendar,
//   LucideHeartPulse,
//   LucideShieldAlert,
//   LucideInfo,
// } from '@lucide/angular';
//
// @Component({
//   selector: 'app-patient-details',
//   standalone: true,
//   imports: [
//     DatePipe,
//     HeaderComponent,
//     LucideArrowRight,
//     LucideUserRound,
//     LucideCalendar,
//     LucideHeartPulse,
//     LucideShieldAlert,
//     LucideInfo,
//   ],
//   templateUrl: './patient-details.component.html',
//   styleUrl: './patient-details.component.scss',
// })
// export class PatientDetailsComponent implements OnInit {
//   //=========================================
//   //==============inject=====================
//   //=========================================
//   protected patientStore = inject(PatientStore);
//   private route = inject(ActivatedRoute);
//   private router = inject(Router);
//
//   //=========================================
//   //==============signals====================
//   //=========================================
//   protected activeTab = signal<'basic' | 'medical' | 'appointments'>('basic');
//
//   //=========================================
//   //==============life cycle=================
//   //=========================================
//   ngOnInit() {
//     const id = Number(this.route.snapshot.paramMap.get('id'));
//     if (id) {
//       this.patientStore.loadPatientById(id);
//     }
//   }
//
//   //=========================================
//   //==============methods====================
//   //=========================================
//   goBack() {
//     this.patientStore.clearSelectedPatient();
//     this.router.navigate(['/staff/patient/list']);
//   }
//
//   setActiveTab(tab: 'basic' | 'medical' | 'appointments') {
//     this.activeTab.set(tab);
//   }
// }
