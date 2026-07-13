import { patchState, signalStore, withComputed, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { computed, inject } from '@angular/core';
import { catchError, EMPTY, pipe, switchMap, tap } from 'rxjs';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { EmployeeService } from '../service/employee.service';
import { EmployeeDetailsDto, EmployeeDto, EmployeeQueryParameters } from '../model/employee.model';
import { DoctorService } from '../../doctor/service/doctor.service';
import {
  CreateDoctorDto,
  DoctorDetailsDto,
  UpdateDoctorDto,
} from '../../doctor/model/doctor.model';
import { PagedResult } from '../../../core/model/pagination.model';

interface IEmployeeState {
  items: EmployeeDto[];
  // doctors: EmployeeDto[];
  totalCount: number;
  loading: boolean;
  error: string | null;
  queryParameters: EmployeeQueryParameters;
  includeInactive: boolean;
  selectedEmployee: EmployeeDetailsDto | null;
  selectedDoctor: DoctorDetailsDto | null;
  Specializations: { id: number; name: string }[];
}

const initialState: IEmployeeState = {
  items: [],
  totalCount: 0,
  // doctors: [],
  loading: false,
  error: null,
  queryParameters: {
    searchTerm: '',
    roleId: null,
    departmentId: null,
    pageNumber: 1,
    pageSize: 10,
  },
  includeInactive: false,
  selectedEmployee: null,
  selectedDoctor: null,
  Specializations: [],
};

export const EmployeeStore = signalStore(
  { providedIn: 'root' },
  withDevtools('EmployeeStore'),
  withState(() => initialState),
  withComputed((store) => {
    return {
      filterDoctorEmployee: computed(() => {
        const employee = store.items();
        return employee.filter((d) => d.jobTitle === 'دكتور');
      }),
    };
  }),
  withMethods(
    (store, employeeService = inject(EmployeeService), doctorService = inject(DoctorService)) => {
      const loadEmployees = rxMethod<{
        params: EmployeeQueryParameters;
        includeInactive: boolean;
      }>(
        pipe(
          tap(({ params, includeInactive }) =>
            patchState(store, {
              loading: true,
              error: null,
              queryParameters: params,
              includeInactive,
            }),
          ),
          switchMap(({ params, includeInactive }) =>
            employeeService.getEmployees(params, includeInactive).pipe(
              tap((response: PagedResult<EmployeeDto>) =>
                patchState(store, {
                  loading: false,
                  items: response.items,
                  totalCount: response.totalCount,
                  queryParameters: {
                    ...params,
                    pageNumber: response.pageNumber,
                    pageSize: response.pageSize,
                  },
                }),
              ),
              catchError((e) => {
                patchState(store, {
                  loading: false,
                  error: e.message || 'حدث خطأ أثناء تحميل الموظفين',
                });
                return EMPTY;
              }),
            ),
          ),
        ),
      );

      const loadEmployeeById = rxMethod<number>(
        pipe(
          tap(() => patchState(store, { loading: true, error: null })),
          switchMap((id) =>
            employeeService.getEmployeeById(id).pipe(
              tap((selectedEmployee) => patchState(store, { loading: false, selectedEmployee })),
              catchError((e) => {
                patchState(store, {
                  loading: false,
                  error: e.message || 'حدث خطأ أثناء تحميل بيانات الموظف',
                });
                return EMPTY;
              }),
            ),
          ),
        ),
      );

      const loadDoctorById = rxMethod<number>(
        pipe(
          tap(() => patchState(store, { loading: true, error: null })),
          switchMap((id) =>
            doctorService.getDoctorById(id).pipe(
              tap((selectedDoctor) => patchState(store, { loading: false, selectedDoctor })),
              catchError((e) => {
                patchState(store, {
                  loading: false,
                  error: e.message || 'حدث خطأ أثناء تحميل بيانات الموظف',
                });
                return EMPTY;
              }),
            ),
          ),
        ),
      );

      const refreshEmployees = () => {
        loadEmployees({
          params: store.queryParameters(),
          includeInactive: store.includeInactive(),
        });
      };

      return {
        loadEmployees,
        loadEmployeeById,
        loadDoctorById,

        createEmployee: rxMethod<FormData>(
          pipe(
            tap(() => patchState(store, { loading: true, error: null })),
            switchMap((data) =>
              employeeService.createEmployee(data).pipe(
                tap(() => {
                  patchState(store, { loading: false });
                  refreshEmployees();
                }),
                catchError((e) => {
                  patchState(store, {
                    loading: false,
                    error: e.message || 'حدث خطأ أثناء إضافة الموظف',
                  });
                  return EMPTY;
                }),
              ),
            ),
          ),
        ),
        createDoctor: rxMethod<CreateDoctorDto>(
          pipe(
            tap(() => patchState(store, { loading: true, error: null })),
            switchMap((data) =>
              doctorService.createDoctor(data).pipe(
                tap(() => {
                  patchState(store, { loading: false });
                  refreshEmployees();
                }),
                catchError((e) => {
                  patchState(store, {
                    loading: false,
                    error: e.message || 'حدث خطأ أثناء إضافة الموظف',
                  });
                  return EMPTY;
                }),
              ),
            ),
          ),
        ),

        updateEmployee: rxMethod<{ id: number; data: FormData }>(
          pipe(
            tap(() => patchState(store, { loading: true, error: null })),
            switchMap(({ id, data }) =>
              employeeService.updateEmployee(id, data).pipe(
                tap((isSuccess) => {
                  if (isSuccess) {
                    patchState(store, { loading: false });
                    loadEmployeeById(id);
                    refreshEmployees();
                  } else {
                    patchState(store, {
                      loading: false,
                      error: 'فشل في تعديل بيانات الموظف',
                    });
                  }
                }),
                catchError((e) => {
                  patchState(store, {
                    loading: false,
                    error: e.message || 'حدث خطأ أثناء تعديل بيانات الموظف',
                  });
                  return EMPTY;
                }),
              ),
            ),
          ),
        ),
        updateDoctor: rxMethod<{ id: number; data: FormData }>(
          pipe(
            tap(() => patchState(store, { loading: true, error: null })),
            switchMap(({ id, data }) =>
              doctorService.updateDoctor(id, data).pipe(
                tap((isSuccess) => {
                  if (isSuccess) {
                    patchState(store, { loading: false });
                    loadDoctorById(id);
                    refreshEmployees();
                  } else {
                    patchState(store, {
                      loading: false,
                      error: 'فشل في تعديل بيانات الموظف',
                    });
                  }
                }),
                catchError((e) => {
                  patchState(store, {
                    loading: false,
                    error: e.message || 'حدث خطأ أثناء تعديل بيانات الموظف',
                  });
                  return EMPTY;
                }),
              ),
            ),
          ),
        ),

        changePassword: rxMethod<{ id: number; password: string }>(
          pipe(
            tap(() => patchState(store, { loading: true, error: null })),
            switchMap(({ id, password }) =>
              employeeService.changePassword(id, password).pipe(
                tap((isSuccess) =>
                  patchState(store, {
                    loading: false,
                    error: isSuccess ? null : 'فشل في تغيير كلمة المرور',
                  }),
                ),
                catchError((e) => {
                  patchState(store, {
                    loading: false,
                    error: e.message || 'حدث خطأ أثناء تغيير كلمة المرور',
                  });
                  return EMPTY;
                }),
              ),
            ),
          ),
        ),
        changeDoctorPassword: rxMethod<{ id: number; password: string }>(
          pipe(
            tap(() => patchState(store, { loading: true, error: null })),
            switchMap(({ id, password }) =>
              doctorService.changePassword(id, password).pipe(
                tap((isSuccess) =>
                  patchState(store, {
                    loading: false,
                    error: isSuccess ? null : 'فشل في تغيير كلمة المرور',
                  }),
                ),
                catchError((e) => {
                  patchState(store, {
                    loading: false,
                    error: e.message || 'حدث خطأ أثناء تغيير كلمة المرور',
                  });
                  return EMPTY;
                }),
              ),
            ),
          ),
        ),

        loadSpecializations: rxMethod<void>(
          pipe(
            tap(() => patchState(store, { loading: true, error: null })),
            switchMap(() =>
              doctorService.getSpecialization().pipe(
                tap((Specializations) => patchState(store, { loading: false, Specializations })),
                catchError((e) => {
                  patchState(store, {
                    loading: false,
                    error: e.message || 'حدث خطأ أثناء تحميل بيانات الموظف',
                  });
                  return EMPTY;
                }),
              ),
            ),
          ),
        ),

        deleteEmployee: rxMethod<number>(
          pipe(
            tap(() => patchState(store, { loading: true, error: null })),
            switchMap((id) =>
              employeeService.deleteEmployee(id).pipe(
                tap((isSuccess) => {
                  if (isSuccess) {
                    const items = store.items().filter((employee) => employee.id !== id);
                    patchState(store, {
                      loading: false,
                      items,
                      totalCount: Math.max(store.totalCount() - 1, 0),
                    });
                    refreshEmployees();
                  } else {
                    patchState(store, {
                      loading: false,
                      error: 'فشل في حذف الموظف',
                    });
                  }
                }),
                catchError((e) => {
                  patchState(store, {
                    loading: false,
                    error: e.message || 'حدث خطأ أثناء حذف الموظف',
                  });
                  return EMPTY;
                }),
              ),
            ),
          ),
        ),

        updateQueryParameters(queryParameters: Partial<EmployeeQueryParameters>) {
          patchState(store, {
            queryParameters: {
              ...store.queryParameters(),
              ...queryParameters,
            },
          });
        },

        clearSelectedEmployee() {
          patchState(store, { selectedEmployee: null });
        },
        clearSelectedDoctor() {
          patchState(store, { selectedDoctor: null });
        },
      };
    },
  ),
);
