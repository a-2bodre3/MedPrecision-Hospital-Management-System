import { Component, computed, effect, inject, OnInit, signal } from '@angular/core';
import {
  LucideCircleX,
  LucideEye,
  LucideKeyRound,
  LucidePencil,
  LucideSearch,
  LucideShieldPlus,
  LucideSlidersHorizontal,
  LucideTrash2,
} from '@lucide/angular';
import { toast } from '@spartan-ng/brain/sonner';
import { HlmButton } from '@spartan-ng/helm/button';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { RoleStore } from '../../state/role.store';
import { PermissionModel, RoleModel } from '../../model/Role.model';

type RoleModalMode = 'create' | 'update' | 'view' | 'delete' | 'permissions' | null;

interface PermissionGroup {
  module: string;
  permissions: PermissionModel[];
}

@Component({
  selector: 'app-role-management',
  imports: [
    HeaderComponent,
    HlmButton,
    LucideCircleX,
    LucideEye,
    LucideKeyRound,
    LucidePencil,
    LucideSearch,
    LucideShieldPlus,
    LucideSlidersHorizontal,
    LucideTrash2,
  ],
  templateUrl: './role-management.component.html',
  styleUrl: './role-management.component.scss',
})
export class RoleManagementComponent implements OnInit {
  protected store = inject(RoleStore);

  protected searchTerm = signal('');
  protected modalMode = signal<RoleModalMode>(null);
  protected selectedRole = signal<RoleModel | null>(null);
  protected roleName = signal('');
  protected touchedRoleName = signal(false);
  protected selectedPermissionIds = signal<Set<number>>(new Set());
  private lastError: string | null = null;

  // -----------------------------------------------
  // Computed
  // -----------------------------------------------
  protected filteredRoles = computed(() => {
    const search = this.searchTerm().trim().toLowerCase();
    const roles = this.store.roles();
    if (!search) return roles;
    return roles.filter((role) => role.name.toLowerCase().includes(search));
  });

  protected roleNameError = computed(() => {
    const name = this.roleName().trim();
    if (!name) return 'اسم الدور مطلوب';
    if (name.length > 100) return 'اسم الدور يجب ألا يتجاوز 100 حرف';
    return null;
  });

  protected isRoleFormInvalid = computed(() => this.roleNameError() !== null);

  protected permissionGroups = computed<PermissionGroup[]>(() => {
    const groups = new Map<string, PermissionModel[]>();
    for (const permission of this.store.permissions()) {
      const module = permission.module || 'General';
      const modulePermissions = groups.get(module) ?? [];
      modulePermissions.push(permission);
      groups.set(module, modulePermissions);
    }
    return Array.from(groups.entries())
      .sort(([a], [b]) => a.localeCompare(b))
      .map(([module, permissions]) => ({
        module,
        permissions: permissions.sort((a, b) => a.description.localeCompare(b.description)),
      }));
  });

  /** الصلاحيات الحالية للدور المحدد (للعرض فقط) */
  protected currentRolePermissions = computed(() => {
    return this.store.roleSelected()?.permissions ?? [];
  });

  protected selectedPermissionCount = computed(() => this.selectedPermissionIds().size);

  // -----------------------------------------------
  // Constructor / Effects
  // -----------------------------------------------
  constructor() {
    // عرض الأخطاء
    effect(() => {
      const error = this.store.error();
      if (error && error !== this.lastError) {
        this.lastError = error;
        toast.error(error);
      }
    });

    // لما يتحمّل roleSelected وكنا في وضع permissions، نهيّئ الـ checkboxes
    effect(() => {
      const mode = this.modalMode();
      const roleSelected = this.store.roleSelected();
      if (mode === 'permissions' && roleSelected) {
        this.selectedPermissionIds.set(
          new Set(roleSelected.permissions.map((p) => p.id)),
        );
      }
    });
  }

  // -----------------------------------------------
  // Lifecycle
  // -----------------------------------------------
  ngOnInit() {
    this.store.getRoles();
    this.store.getPermissions();
  }

  // -----------------------------------------------
  // Modal openers
  // -----------------------------------------------
  protected openCreateModal() {
    this.selectedRole.set(null);
    this.roleName.set('');
    this.touchedRoleName.set(false);
    this.modalMode.set('create');
  }

  protected openUpdateModal(role: RoleModel) {
    this.selectedRole.set(role);
    this.roleName.set(role.name);
    this.touchedRoleName.set(false);
    this.modalMode.set('update');
  }

  protected openViewModal(role: RoleModel) {
    this.selectedRole.set(role);
    this.store.getRolePermissions(role.id); // يجلب الصلاحيات ويحدّث roleSelected
    this.modalMode.set('view');
  }

  protected openDeleteModal(role: RoleModel) {
    this.selectedRole.set(role);
    this.modalMode.set('delete');
  }

  protected openPermissionsModal(role: RoleModel) {
    this.selectedRole.set(role);
    this.selectedPermissionIds.set(new Set()); // إعادة ضبط مؤقتاً ريثما يتحمّل
    this.store.getRolePermissions(role.id);    // الـ effect سيهيّئ الـ checkboxes عند الاستجابة
    this.modalMode.set('permissions');
  }

  protected closeModal() {
    this.modalMode.set(null);
    this.selectedRole.set(null);
    this.roleName.set('');
    this.touchedRoleName.set(false);
    this.selectedPermissionIds.set(new Set());
  }

  // -----------------------------------------------
  // Actions
  // -----------------------------------------------
  protected submitRoleForm(event: Event) {
    event.preventDefault();
    this.touchedRoleName.set(true);

    if (this.isRoleFormInvalid()) {
      toast.error(this.roleNameError() ?? 'بيانات الدور غير صحيحة');
      return;
    }

    const name = this.roleName().trim();

    if (this.modalMode() === 'create') {
      this.store.createRole(name);
      this.closeModal();
      return;
    }

    const role = this.selectedRole();
    if (!role) {
      toast.error('لم يتم تحديد أي دور');
      return;
    }

    this.store.updateRole({ id: role.id, name });
    this.closeModal();
  }

  protected confirmDeleteRole() {
    const role = this.selectedRole();
    if (!role) {
      toast.error('لم يتم تحديد أي دور');
      return;
    }
    this.store.deleteRole(role.id);
    this.closeModal();
  }

  protected hasPermission(permissionId: number): boolean {
    return this.selectedPermissionIds().has(permissionId);
  }

  protected togglePermission(permissionId: number, checked: boolean) {
    this.selectedPermissionIds.update((ids) => {
      const next = new Set(ids);
      if (checked) next.add(permissionId);
      else next.delete(permissionId);
      return next;
    });
  }

  protected savePermissions() {
    const role = this.selectedRole();
    if (!role) {
      toast.error('لم يتم تحديد أي دور');
      return;
    }
    this.store.updateRolePermissions({
      id: role.id,
      permissions: Array.from(this.selectedPermissionIds()),
    });
    this.closeModal();
  }
}
