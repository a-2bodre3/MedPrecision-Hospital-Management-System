import { PatientDetailsDto, PatientDto, PatientQueryParameters } from '../model/patient.model';
import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { inject } from '@angular/core';
import { PatientService } from '../service/patient.service';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { catchError, EMPTY, pipe, switchMap, tap } from 'rxjs';
import { PagedResult } from '../../../core/model/pagination.model';

interface IPatientState {
  patient: PatientDto[];
  totalCount: number;
  loading: boolean;
  error: string | null;
  queryParams: PatientQueryParameters;
  selectedPatient: PatientDetailsDto | null;
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
  selectedPatient: null,
};

export const PatientStore = signalStore(
  { providedIn: 'root' },
  withDevtools('PatientStore'),
  withState(() => initialState),
  withMethods((store, patientService = inject(PatientService)) => {
    const loadPatient = rxMethod<{ params: PatientQueryParameters }>(
      pipe(
        tap(({ params }) =>
          patchState(store, {
            loading: true,
            queryParams: params,
          }),
        ),
        switchMap(({ params }) =>
          patientService.getPatients(params).pipe(
            tap((response: PagedResult<PatientDto>) =>
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
            tap((selectedPatient) =>
              patchState(store, { loading: false, selectedPatient: selectedPatient }),
            ),
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

      createPatient: rxMethod<FormData>(
        pipe(
          tap(() => patchState(store, { loading: true })),
          switchMap((data) =>
            patientService.createPatient(data).pipe(
              tap(() => {
                patchState(store, { loading: false });
                refreshPatient();
              }),
              catchError((e) => {
                patchState(store, {
                  loading: false,
                  error: e.message || 'حدث خطأ أثناء إضافة المريض',
                });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),

      updatePatient: rxMethod<{ id: number; data: FormData }>(
        pipe(
          tap(() => patchState(store, { loading: true })),
          switchMap(({ id, data }) =>
            patientService.updatePatient(id, data).pipe(
              tap((isSuccess) => {
                if (isSuccess) {
                  patchState(store, { loading: false });
                  loadPatientById(id);
                  refreshPatient();
                } else {
                  patchState(store, {
                    loading: false,
                    error: 'فشل في تعديل بيانات المريض',
                  });
                }
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
        patchState(store, { loading: false, selectedPatient: null });
      },
    };
  }),
);
