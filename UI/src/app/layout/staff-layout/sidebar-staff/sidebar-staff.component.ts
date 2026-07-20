import { Component, computed, inject, signal } from '@angular/core';
import {
  HlmCollapsible,
  HlmCollapsibleContent,
  HlmCollapsibleTrigger,
} from '@spartan-ng/helm/collapsible';
import { HlmSidebar, HlmSidebarImports } from '@spartan-ng/helm/sidebar';
import {
  LucideChevronRight,
  LucideDynamicIcon,
  LucideFileClock,
  LucideGitBranchMinus,
  LucideHandHeart,
  LucideHospital,
  LucideLayoutDashboard,
  LucideLogOut,
  LucideMonitorCog,
  LucidePipette,
  LucideWallet,
  LucideWrench,
} from '@lucide/angular';
import { AuthStore } from '../../../features/auth/state/auth.store';
import { HasPermissionDirective } from '../../../shared/directives/has-permission.directive';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-sidebar-staff',
  imports: [
    HlmCollapsible,
    HlmCollapsibleContent,
    HlmCollapsibleTrigger,
    HlmSidebar,
    LucideChevronRight,
    HlmSidebarImports,
    LucideLogOut,
    LucideLayoutDashboard,
    LucideDynamicIcon,
    HasPermissionDirective,
    RouterLink,
    RouterLinkActive,
  ],
  templateUrl: './sidebar-staff.component.html',
  styleUrl: './sidebar-staff.component.scss',
})
export class SidebarStaffComponent {
  protected authStore = inject(AuthStore);
  protected readonly rawItems = signal([
    {
      title: 'المرضي',
      icon: LucidePipette,
      items: [
        {
          title: 'تسجيل مريض جديد',
          permission: 'Patient_Create',
          link: '/staff/patient/add',
        },
        {
          title: 'قائمه المرضي',
          permission: 'Patient_Read_Basic',
          link: '/staff/patient/list',
        },
      ],
    },
    {
      title: 'المواعيد',
      icon: LucideFileClock,
      items: [
        {
          title: 'حجز موعد جديد',
          permission: 'Appointment_Create',
          link: '/staff',
        },
        {
          title: ' جدول المواعيد',
          permission: 'Appointment_Read',
          link: '/staff',
        },
      ],
    },
    {
      title: 'العياده',
      icon: LucideHospital,
      items: [
        {
          title: 'بدأ كشف',
          permission: 'Prescription_Create',
          link: '/staff',
        },
        {
          title: 'سجل الروشتات',
          permission: 'Medical_Report_Generate',
          link: '/staff',
        },
        {
          title: 'العلامات الحيويه',
          permission: 'Vitals_Record',
          link: '/staff',
        },
      ],
    },
    {
      title: 'المحتبر والاشعه',
      icon: LucideMonitorCog,
      items: [
        {
          title: 'طلبات الفحوصات',
          permission: 'Lab_Request_Read',
          link: '/staff',
        },
        {
          title: 'نتائج المختبر',
          permission: 'Lab_Result_Upload',
          link: '/staff',
        },
      ],
    },
    {
      title: 'الصيدليه و المخازن',
      icon: LucideHandHeart,
      items: [
        {
          title: 'صرف الادويه',
          permission: 'Pharmacy_Dispense',
          link: '/staff',
        },
        {
          title: 'اداره المحزون',
          permission: 'Inventory_Read',
          link: '/staff',
        },
      ],
    },
    {
      title: 'الحسابات',
      icon: LucideWallet,
      items: [
        {
          title: 'الفواتير',
          permission: 'Invoice_Read',
          link: '/staff',
        },
        {
          title: 'تقارير ماليه',
          permission: 'Financial_Report_View',
          link: '/staff',
        },
      ],
    },
    {
      title: 'اعدادات الفروع',
      icon: LucideGitBranchMinus,
      items: [
        {
          title: 'الفروع',
          permission: 'Branch_Create',
          link: '/staff/branch',
        },
        {
          title: 'الاقسام',
          permission: 'Department_Management',
          link: '/staff/department',
        },
        {
          title: 'الغرف',
          permission: 'Room_Management',
          link: '/staff/room',
        },
      ],
    },
    {
      title: 'الاداره',
      icon: LucideWrench,
      items: [
        {
          title: 'الموظفين',
          permission: 'User_Read',
          link: '/staff/employee',
        },
        {
          title: 'الاطباء',
          permission: 'User_Read',
          link: '/staff/doctor',
        },
        {
          title: 'اداره مواعيد الاطباء',
          permission: 'User_Read',
          link: '/staff/doctorSchedule',
        },
        {
          title: 'الادوار والصلاحيات',
          permission: 'Role_Manage',
          link: '/staff/role',
        },
        {
          title: 'سجلات النظام',
          permission: 'System_Logs_View',
          link: '/staff',
        },
      ],
    },
  ]);
  protected readonly items = computed(() => {
    return this.rawItems()
      .map((item) => {
        const filteredSubItems = item.items.filter((subItem) =>
          this.authStore.hasPermission()(subItem.permission),
        );

        return { ...item, items: filteredSubItems };
      })
      .filter((item) => item.items.length > 0);
  });

  logout() {
    this.authStore.logoutUser();
  }
}
