import { patchState, signalStore, withComputed, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { computed, inject } from '@angular/core';
import { catchError, EMPTY, pipe, switchMap, tap } from 'rxjs';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { EmployeeService } from '../service/employee.service';
import { PagedResult } from '../../../core/model/pagination.model';
import {
  EmployeeDetailsResponse,
  EmployeeQuery,
  EmployeeResponse,
} from '../model/employee-response.model';
import { CreateEmployeeRequest, UpdateEmployeeRequest } from '../model/employee-request.model';

interface IEmployeeState {
  items: EmployeeResponse[];
  totalCount: number;
  loading: boolean;
  error: string | null;
  queryParameters: EmployeeQuery;
  employeeDetails: EmployeeDetailsResponse | null;
}

const initialState: IEmployeeState = {
  items: [],
  totalCount: 0,
  loading: false,
  error: null,
  queryParameters: {
    pageNumber: 1,
    pageSize: 10,
  },
  employeeDetails: null,
};

export const EmployeeStore = signalStore(
  { providedIn: 'root' },
  withDevtools('EmployeeStore'),
  withState(() => initialState),
  withMethods((store, employeeService = inject(EmployeeService)) => {
    const loadEmployees = rxMethod<{
      params: EmployeeQuery;
    }>(
      pipe(
        tap(({ params }) =>
          patchState(store, {
            loading: true,
            error: null,
            queryParameters: params,
          }),
        ),
        switchMap(({ params }) =>
          employeeService.getEmployees(params).pipe(
            tap((response: PagedResult<EmployeeResponse>) =>
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
            tap((employeeDetails) => patchState(store, { loading: false, employeeDetails })),
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
      });
    };

    return {
      loadEmployees,
      loadEmployeeById,

      createEmployee: rxMethod<{ data: CreateEmployeeRequest }>(
        pipe(
          tap(() => patchState(store, { loading: true, error: null })),
          switchMap(({ data }) =>
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

      updateEmployee: rxMethod<{ id: number; data: UpdateEmployeeRequest }>(
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

      updateQueryParameters(queryParameters: Partial<EmployeeQuery>) {
        patchState(store, {
          queryParameters: {
            ...store.queryParameters(),
            ...queryParameters,
          },
        });
      },

      clearSelectedEmployee() {
        patchState(store, { employeeDetails: null });
      },
    };
  }),
);
