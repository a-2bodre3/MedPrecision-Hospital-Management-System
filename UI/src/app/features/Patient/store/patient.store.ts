import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { inject } from '@angular/core';
import { PatientService } from '../service/patient.service';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { catchError, EMPTY, pipe, switchMap, tap } from 'rxjs';
import { PagedResult } from '../../../core/model/pagination.model';
import {
  PatientDetailsResponse,
  PatientQuery,
  PatientResponse,
} from '../model/patient.response.model';
import { CreatePatientRequest, UpdatePatientRequest } from '../model/patient.request.model';
import { toast } from '@spartan-ng/brain/sonner';
import { Router } from '@angular/router';

interface IPatientState {
  patient: PatientResponse[];
  totalCount: number;
  loading: boolean;
  error: string | null;
  queryParams: PatientQuery;
  PatientDetails: PatientDetailsResponse | null;
}

const initialState: IPatientState = {
  patient: [],
  totalCount: 0,
  loading: false,
  error: null,
  queryParams: {
    searchTerm: '',
    pageNumber: 1,
    pageSize: 10,
  },
  PatientDetails: null,
};

export const PatientStore = signalStore(
  { providedIn: 'root' },
  withDevtools('PatientStore'),
  withState(() => initialState),
  withMethods((store, router = inject(Router), patientService = inject(PatientService)) => {
    const loadPatient = rxMethod<{ params: PatientQuery }>(
      pipe(
        tap(({ params }) =>
          patchState(store, {
            loading: true,
            queryParams: params,
          }),
        ),
        switchMap(({ params }) =>
          patientService.getPatients(params).pipe(
            tap((response: PagedResult<PatientResponse>) =>
              patchState(store, {
                loading: false,
                patient: response.items,
                totalCount: response.totalCount,
                queryParams: {
                  ...params,
                  pageNumber: response.pageNumber,
                  pageSize: response.pageSize,
                },
              }),
            ),
            catchError((e) => {
              patchState(store, {
                loading: false,
                error: e.message || 'حدث حطأ اثناء تحميل المرضي',
              });
              return EMPTY;
            }),
          ),
        ),
      ),
    );
    const loadPatientById = rxMethod<number>(
      pipe(
        tap(() => patchState(store, { loading: true })),
        switchMap((id: number) =>
          patientService.getPatientById(id).pipe(
            tap((data) => patchState(store, { loading: false, PatientDetails: data })),
            catchError((e) => {
              patchState(store, {
                loading: false,
                error: e.message || 'حدث خطأ أثناء تحميل بيانات المريض',
              });
              return EMPTY;
            }),
          ),
        ),
      ),
    );
    const refreshPatient = () => {
      loadPatient({
        params: store.queryParams(),
      });
    };
    return {
      loadPatient,
      loadPatientById,

      createPatient: rxMethod<{ data: CreatePatientRequest }>(
        pipe(
          tap(() => patchState(store, { loading: true })),
          switchMap(({ data }) =>
            patientService.createPatient(data).pipe(
              tap(() => {
                toast.success('تم إنشاء حساب الموظف الجديد بنجاح مذهل!');
                router.navigate(['/staff/patient/list']);
                patchState(store, { loading: false });
                refreshPatient();
              }),
              catchError((e) => {
                const error = e.message || 'حدث خطأ أثناء إضافة المريض';
                patchState(store, {
                  loading: false,
                  error: error,
                });
                toast.error(error);
                return EMPTY;
              }),
            ),
          ),
        ),
      ),

      updatePatient: rxMethod<{ id: number; data: UpdatePatientRequest }>(
        pipe(
          tap(() => patchState(store, { loading: true })),
          switchMap(({ id, data }) =>
            patientService.updatePatient(id, data).pipe(
              tap(() => {
                toast.success('تم تعديل بيانات المريض بنجاح');
                patchState(store, { loading: false , PatientDetails : null});
                refreshPatient();
                router.navigate(['/staff/patient/list']);

              }),
              catchError((e) => {
                patchState(store, {
                  loading: false,
                  error: e.message || 'فشل في تعديل بيانات المريض',
                });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),
      changePassword: rxMethod<{ id: number; password: string }>(
        pipe(
          tap(() => patchState(store, { loading: true })),
          switchMap(({ id, password }) =>
            patientService.changePassword(id, password).pipe(
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
      deletePatient: rxMethod<number>(
        pipe(
          tap(() => patchState(store, { loading: true })),
          switchMap((id) =>
            patientService.deletePatient(id).pipe(
              tap(() => {
                patchState(store, { loading: false });
                refreshPatient();
              }),
              catchError((e) => {
                patchState(store, {
                  loading: false,
                  error: e.message || 'حدث خطأ أثناء حذف المريض',
                });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),
      clearSelectedPatient() {
        patchState(store, { loading: false, PatientDetails: null });
      },
    };
  }),
);
