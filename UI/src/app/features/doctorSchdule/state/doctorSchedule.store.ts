import { DoctorScheduleDisplayDTO, DoctorScheduleFormDTO } from '../model/doctorSchedule.model';
import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { inject } from '@angular/core';
import { DoctorScheduleService } from '../service/doctor-schedule.service';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { catchError, EMPTY, pipe, switchMap, tap } from 'rxjs';
import { toast } from '@spartan-ng/brain/sonner';

interface IDoctorScheduleState {
  isLoading: boolean;
  error: string | null;
  doctorSchedule: DoctorScheduleDisplayDTO[];
}

const initialState: IDoctorScheduleState = {
  isLoading: false,
  error: null,
  doctorSchedule: [],
};
export const doctorScheduleState = signalStore(
  { providedIn: 'root' },
  withDevtools('DoctorScheduleStore'),
  withState(() => initialState),
  withMethods((store, doctorScheduleService = inject(DoctorScheduleService)) => {
    const loadAllDoctorSchedule = rxMethod<void>(
      pipe(
        tap(() => patchState(store, { isLoading: true })),
        switchMap(() =>
          doctorScheduleService.getDoctorSchedule().pipe(
            tap((data) => patchState(store, { isLoading: false, doctorSchedule: data })),
            catchError((e) => {
              patchState(store, {
                isLoading: false,
                error: e.message || 'فشل في نحميل كل المواعيد',
              });
              return EMPTY;
            }),
          ),
        ),
      ),
    );

    const loadDoctorScheduleByDoctorId = rxMethod<number>(
      pipe(
        tap(() => patchState(store, { isLoading: true })),
        switchMap((id: number) =>
          doctorScheduleService.getDoctorSchedule(id).pipe(
            tap((data) => patchState(store, { isLoading: false, doctorSchedule: data })),
            catchError((e) => {
              patchState(store, {
                isLoading: false,
                error: e.message || 'فشل في نحميل كل المواعيد',
              });
              return EMPTY;
            }),
          ),
        ),
      ),
    );

    return {
      loadAllDoctorSchedule,
      loadDoctorScheduleByDoctorId,
      createDoctorSchedule: rxMethod<DoctorScheduleFormDTO>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((data) =>
            doctorScheduleService.createDoctorSchedule(data).pipe(
              tap(() => {
                patchState(store, { isLoading: false });
                loadAllDoctorSchedule();
                toast.success('تم اضافه معاد جديد بنجاح');
              }),
              catchError((e) => {
                const errorMessage = e.message || 'حدث خطأ أثناء إضافة ميعاد جديد';
                patchState(store, {
                  isLoading: false,
                  error: errorMessage,
                });
                toast.error(errorMessage);
                return EMPTY;
              }),
            ),
          ),
        ),
      ),

      updateDoctorSchedule: rxMethod<{ id: number; data: DoctorScheduleFormDTO }>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap(({ id, data }) =>
            doctorScheduleService.updateDoctorSchedule(id, data).pipe(
              tap(() => {
                patchState(store, { isLoading: false });
                loadAllDoctorSchedule();
                toast.success('تم تعديل الميعاد بنجاح');
              }),
              catchError((e) => {
                const errorMessage = e.message || 'حدث خطأ أثناء تعديل الميعاد';
                patchState(store, {
                  isLoading: false,
                  error: errorMessage,
                });
                toast.error(errorMessage);
                return EMPTY;
              }),
            ),
          ),
        ),
      ),

      deleteDoctorSchedule: rxMethod<number>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((id: number) =>
            doctorScheduleService.deleteDoctorSchedule(id).pipe(
              tap(() => {
                patchState(store, { isLoading: false });
                loadAllDoctorSchedule();
                toast.warning('تم حذف الميعاد بنجاح');
              }),
              catchError((e) => {
                const errorMessage = e.message || 'حدث خطأ أثناء حذف الميعاد';
                patchState(store, {
                  isLoading: false,
                  error: errorMessage,
                });
                toast.error(errorMessage);
                return EMPTY;
              }),
            ),
          ),
        ),
      ),
    };
  }),
);
