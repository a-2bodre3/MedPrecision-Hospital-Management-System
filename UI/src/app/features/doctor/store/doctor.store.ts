import { patchState, signalStore, withComputed, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { computed, inject } from '@angular/core';
import { catchError, EMPTY, pipe, switchMap, tap } from 'rxjs';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { DoctorService } from '../service/doctor.service';
import { PagedResult } from '../../../core/model/pagination.model';
import {
  DoctorDetailsResponse,
  DoctorQuery,
  DoctorResponse,
} from '../model/doctor-response.model';
import { CreateDoctorRequest, UpdateDoctorRequest } from '../model/doctor-request.model';

interface IDoctorState {
  items: DoctorResponse[];
  totalCount: number;
  loading: boolean;
  error: string | null;
  queryParameters: DoctorQuery;
  doctorDetails: DoctorDetailsResponse | null;
}

const initialState: IDoctorState = {
  items: [],
  totalCount: 0,
  loading: false,
  error: null,
  queryParameters: {
    pageNumber: 1,
    pageSize: 10,
  },
  doctorDetails: null,
};

export const DoctorStore = signalStore(
  { providedIn: 'root' },
  withDevtools('DoctorStore'),
  withState(() => initialState),
  withMethods((store, doctorService = inject(DoctorService)) => {
    const loadDoctors = rxMethod<{
      params: DoctorQuery;
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
          doctorService.getDoctors(params).pipe(
            tap((response: PagedResult<DoctorResponse>) =>
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
                error: e.message || 'حدث خطأ أثناء تحميل الأطباء',
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
            tap((doctorDetails) => patchState(store, { loading: false, doctorDetails })),
            catchError((e) => {
              patchState(store, {
                loading: false,
                error: e.message || 'حدث خطأ أثناء تحميل بيانات الطبيب',
              });
              return EMPTY;
            }),
          ),
        ),
      ),
    );

    const refreshDoctors = () => {
      loadDoctors({
        params: store.queryParameters(),
      });
    };

    return {
      loadDoctors,
      loadDoctorById,

      createDoctor: rxMethod<{ data: CreateDoctorRequest }>(
        pipe(
          tap(() => patchState(store, { loading: true, error: null })),
          switchMap(({ data }) =>
            doctorService.createDoctor(data).pipe(
              tap(() => {
                patchState(store, { loading: false });
                refreshDoctors();
              }),
              catchError((e) => {
                patchState(store, {
                  loading: false,
                  error: e.message || 'حدث خطأ أثناء إضافة الطبيب',
                });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),

      updateDoctor: rxMethod<{ id: number; data: UpdateDoctorRequest }>(
        pipe(
          tap(() => patchState(store, { loading: true, error: null })),
          switchMap(({ id, data }) =>
            doctorService.updateDoctor(id, data).pipe(
              tap((isSuccess) => {
                if (isSuccess) {
                  patchState(store, { loading: false });
                  loadDoctorById(id);
                  refreshDoctors();
                } else {
                  patchState(store, {
                    loading: false,
                    error: 'فشل في تعديل بيانات الطبيب',
                  });
                }
              }),
              catchError((e) => {
                patchState(store, {
                  loading: false,
                  error: e.message || 'حدث خطأ أثناء تعديل بيانات الطبيب',
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

      updateQueryParameters(queryParameters: Partial<DoctorQuery>) {
        patchState(store, {
          queryParameters: {
            ...store.queryParameters(),
            ...queryParameters,
          },
        });
      },

      clearSelectedDoctor() {
        patchState(store, { doctorDetails: null });
      },
    };
  }),
);
