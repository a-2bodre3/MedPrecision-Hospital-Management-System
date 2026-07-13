import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { inject } from '@angular/core';
import { catchError, EMPTY, firstValueFrom, pipe, switchMap, tap } from 'rxjs';

import { RoleService } from '../service/role.service';
import { PermissionModel, RoleModel, RolePermissionsResponse } from '../model/Role.model';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { toast } from '@spartan-ng/brain/sonner';

interface IRoleState {
  isLoading: boolean;
  error: string | null;
  roles: RoleModel[];
  permissions: PermissionModel[];
  rolePermissionIds: number[];
  roleSelected: RolePermissionsResponse | null;
}

const initialState: IRoleState = {
  isLoading: false,
  error: null,
  roles: [],
  permissions: [],
  rolePermissionIds: [],
  roleSelected: null,
};

export const RoleStore = signalStore(
  { providedIn: 'root' },
  withDevtools('RoleStore'),
  withState(() => initialState),
  withMethods((store, roleService = inject(RoleService)) => {
    const getRoles = rxMethod<void>(
      pipe(
        tap(() => patchState(store, { isLoading: true })),
        switchMap(() =>
          roleService.getRoles().pipe(
            tap((rolesList) => patchState(store, { roles: rolesList, isLoading: false })),
            catchError((err) => {
              const errorMessage = err.message || 'حدث خطا اثاء جلب الادوار';
              toast.error(errorMessage);
              patchState(store, { isLoading: false, error: errorMessage });
              return EMPTY;
            }),
          ),
        ),
      ),
    );
    const getPermissions = rxMethod<void>(
      pipe(
        tap(() => patchState(store, { isLoading: true })),
        switchMap(() =>
          roleService.getPermissions().pipe(
            tap((permissionList) =>
              patchState(store, { permissions: permissionList, isLoading: false }),
            ),
            catchError((err) => {
              const errorMessage = err.message || 'حدث خطا اثاء جلب الصلاحيات';
              toast.error(errorMessage);
              patchState(store, { isLoading: false, error: errorMessage });
              return EMPTY;
            }),
          ),
        ),
      ),
    );
    const clearSelectedRole = tap(() => patchState(store, { roleSelected: null }));
    return {
      getRoles,
      getPermissions,
      clearSelectedRole,
      createRole: rxMethod<string>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((roleName) =>
            roleService.createRole(roleName).pipe(
              tap(() => {
                getRoles();
                patchState(store, { isLoading: false });
                toast.success('نم اضافه الدور بنجاح');
              }),
              catchError((err) => {
                const errorMessage = err.message || 'حدث خطا اثاء انشاء الدور';
                toast.error(errorMessage);
                patchState(store, { isLoading: false, error: errorMessage });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),
      getRolePermissions: rxMethod<number>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((id) =>
            roleService.getRolePermission(id).pipe(
              tap((rolePermissions) =>
                patchState(store, { roleSelected: rolePermissions, isLoading: false }),
              ),
              catchError((err) => {
                const errorMessage = err.message || 'حدث خطا اثاء جلب تفاصيل الدور';
                toast.error(errorMessage);
                patchState(store, { isLoading: false, error: errorMessage });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),
      updateRole: rxMethod<{ id: number; name: string }>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((data) =>
            roleService.updateRole(data.id, data.name).pipe(
              tap(() => {
                getRoles();
                patchState(store, { isLoading: false });
                toast.success('نم تعديل الدور بنجاح');
              }),
              catchError((err) => {
                const errorMessage = err.message || 'حدث خطا اثاء تعديل الدور';
                toast.error(errorMessage);
                patchState(store, { isLoading: false, error: errorMessage });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),
      deleteRole: rxMethod<number>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((id) =>
            roleService.deleteRole(id).pipe(
              tap(() => {
                getRoles();
                patchState(store, { isLoading: false });
                toast.info('نم حذف الدور بنجاح');
              }),
              catchError((err) => {
                const errorMessage = err.message || 'حدث خطا اثاء حذف الدور';
                toast.error(errorMessage);
                patchState(store, { isLoading: false, error: errorMessage });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),
      updateRolePermissions: rxMethod<{ id: number; permissions: number[] }>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((data) =>
            roleService.updateRolePermissions(data.id, data.permissions).pipe(
              tap(() => {
                patchState(store, { isLoading: false });
                toast.success('تم تعديل صلاحيات الدور بنجاح');
              }),
              catchError((err) => {
                const errorMessage = err.message || 'حدث خطا اثاء تعديل صلاحيات الدور';
                toast.error(errorMessage);
                patchState(store, { isLoading: false, error: errorMessage });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),
    };
  }),
);
