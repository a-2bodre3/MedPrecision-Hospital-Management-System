import {
  AdjustScheduleValidityDto,
  DoctorScheduleDetailsDTO,
  DoctorScheduleDisplayDTO,
  DoctorScheduleFormDTO,
} from '../model/doctorSchedule.model';
import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { inject } from '@angular/core';
import { DoctorScheduleService } from '../service/doctor-schedule.service';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { catchError, EMPTY, pipe, switchMap, tap } from 'rxjs';
import { toast } from '@spartan-ng/brain/sonner';
import { PagedResult } from '../../../core/model/pagination.model';

interface IDoctorScheduleState {
  isLoading: boolean;
  error: string | null;
  doctorSchedule: DoctorScheduleDisplayDTO[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  selectedSchedule: DoctorScheduleDetailsDTO | null;
}

const initialState: IDoctorScheduleState = {
  isLoading: false,
  error: null,
  doctorSchedule: [],
  totalCount: 0,
  pageNumber: 1,
  pageSize: 10,
  selectedSchedule: null,
};

export const doctorScheduleState = signalStore(
  { providedIn: 'root' },
  withDevtools('DoctorScheduleStore'),
  withState(() => initialState),
  withMethods((store, doctorScheduleService = inject(DoctorScheduleService)) => {
    const loadAllDoctorSchedule = rxMethod<{ pageNumber?: number; pageSize?: number; specializationId?: number }>(
      pipe(
        tap(() => patchState(store, { isLoading: true })),
        switchMap(({ pageNumber = 1, pageSize = 10, specializationId }) =>
          doctorScheduleService.getDoctorSchedules(pageNumber, pageSize, specializationId).pipe(
            tap((data: PagedResult<DoctorScheduleDisplayDTO>) =>
              patchState(store, {
                isLoading: false,
                doctorSchedule: data.items,
                totalCount: data.totalCount,
                pageNumber: data.pageNumber,
                pageSize: data.pageSize,
              }),
            ),
            catchError((e) => {
              patchState(store, {
                isLoading: false,
                error: e.message || 'فشل في تحميل مواعيد الأطباء',
              });
              return EMPTY;
            }),
          ),
        ),
      ),
    );

    const loadDoctorScheduleById = rxMethod<number>(
      pipe(
        tap(() => patchState(store, { isLoading: true })),
        switchMap((id: number) =>
          doctorScheduleService.getDoctorScheduleById(id).pipe(
            tap((selectedSchedule) =>
              patchState(store, { isLoading: false, selectedSchedule }),
            ),
            catchError((e) => {
              patchState(store, {
                isLoading: false,
                error: e.message || 'فشل في تحميل بيانات الجدول',
              });
              return EMPTY;
            }),
          ),
        ),
      ),
    );

    const refreshSchedules = () => {
      loadAllDoctorSchedule({
        pageNumber: store.pageNumber(),
        pageSize: store.pageSize(),
      });
    };

    return {
      loadAllDoctorSchedule,
      loadDoctorScheduleById,

      createDoctorSchedule: rxMethod<DoctorScheduleFormDTO>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((data) =>
            doctorScheduleService.createDoctorSchedule(data).pipe(
              tap(() => {
                patchState(store, { isLoading: false });
                refreshSchedules();
                toast.success('تم إضافة موعد جديد بنجاح');
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
                refreshSchedules();
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

      adjustScheduleValidity: rxMethod<{ id: number; data: AdjustScheduleValidityDto }>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap(({ id, data }) =>
            doctorScheduleService.adjustScheduleValidity(id, data).pipe(
              tap(() => {
                patchState(store, { isLoading: false });
                refreshSchedules();
                toast.success('تم تعديل صلاحية الجدول بنجاح');
              }),
              catchError((e) => {
                const errorMessage = e.message || 'حدث خطأ أثناء تعديل صلاحية الجدول';
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
                refreshSchedules();
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

      clearSelectedSchedule() {
        patchState(store, { selectedSchedule: null });
      },
    };
  }),
);
