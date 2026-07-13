import { DepartmentDtoModel } from '../model/department-dto.model';
import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { inject } from '@angular/core';
import { DepartmentService } from '../service/department.service';
import { firstValueFrom } from 'rxjs';
import { CreateDepartmentDtoModel } from '../model/create-department-dto.model';
import { UpdateDepartmentDtoModel } from '../model/update-department-dto.model';

interface IDepartmentState {
  isLoading: boolean;
  error: string | null;
  departments: DepartmentDtoModel[];
  departmentSelected: DepartmentDtoModel | null;
}

const initialState: IDepartmentState = {
  isLoading: false,
  error: null,
  departments: [],
  departmentSelected: null,
};

export const DepartmentStore = signalStore(
  { providedIn: 'root' },
  withDevtools('DepartmentStore'),
  withState(() => initialState),
  withMethods((store, departmentService = inject(DepartmentService)) => ({
    async fetchDepartments() {
      patchState(store, { isLoading: true, error: null });
      try {
        const response = await firstValueFrom(departmentService.getDepartments());
        patchState(store, { isLoading: false, departments: response });
      } catch (e: any) {
        patchState(store, {
          isLoading: false,
          error: e.message || 'حدث خطأ أثناء تحميل الأقسام',
        });
      }
    },
    clearSelectedDepartments() {
      patchState(store, { departmentSelected: null });
    },
    loadDepartmentById(id: number) {
      const selected = store.departments().find((department) => department.id === id) ?? null;
      patchState(store, { departmentSelected: selected });
    },
    async createDepartment(department: CreateDepartmentDtoModel): Promise<boolean> {
      patchState(store, { isLoading: true, error: null });
      try {
        await firstValueFrom(departmentService.createDepartment(department));
        const departments = await firstValueFrom(departmentService.getDepartments());
        patchState(store, { isLoading: false, departments });
        return true;
      } catch (e: any) {
        patchState(store, {
          isLoading: false,
          error: e.message || 'حدث خطأ أثناء إضافة القسم الجديد',
        });
        return false;
      }
    },
    async updateDepartment(id: number, updatedData: UpdateDepartmentDtoModel): Promise<boolean> {
      patchState(store, { isLoading: true, error: null });
      try {
        const isSuccess = await firstValueFrom(departmentService.updateDepartment(id, updatedData));
        if (isSuccess) {
          const departments = await firstValueFrom(departmentService.getDepartments());
          patchState(store, { isLoading: false, departments, departmentSelected: null });
          return true;
        } else {
          patchState(store, {
            isLoading: false,
            error: 'فشل في حفظ التعديلات',
          });
          return false;
        }
      } catch (e: any) {
        patchState(store, {
          isLoading: false,
          error: e.message || 'حدث خطأ أثناء تعديل القسم',
        });
        return false;
      }
    },
    async deleteDepartment(id: number): Promise<boolean> {
      patchState(store, { isLoading: true, error: null });
      try {
        await firstValueFrom(departmentService.deleteDepartment(id));
        const updateDepartments = store
          .departments()
          .filter((item: DepartmentDtoModel) => item.id !== id);
        const selected = store.departmentSelected();
        patchState(store, {
          isLoading: false,
          departments: updateDepartments,
          departmentSelected: selected?.id === id ? null : selected,
        });
        return true;
      } catch (e: any) {
        patchState(store, {
          isLoading: false,
          error: e.message || 'حدث خطأ أثناء حذف القسم',
        });
        return false;
      }
    },
  })),
);
