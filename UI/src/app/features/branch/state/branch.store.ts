import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { inject } from '@angular/core';
import { BranchService } from '../service/branch.service';
import { catchError, EMPTY, firstValueFrom, pipe, switchMap, tap } from 'rxjs';
import { toast } from '@spartan-ng/brain/sonner';
import { BranchDetails, BranchListItem } from '../model/branch-response.model';
import { CreateBranchRequest, UpdateBranchRequest } from '../model/branch-request.model';
import { rxMethod } from '@ngrx/signals/rxjs-interop';

interface IBranchState {
  isLoading: boolean;
  error: string | null;
  branches: BranchListItem[];
  branchDetails: BranchDetails | null;
}
const initialState: IBranchState = {
  isLoading: false,
  error: null,
  branches: [],
  branchDetails: null,
};

export const BranchStore = signalStore(
  { providedIn: 'root' },
  withDevtools('BranchStore'),
  withState(() => {
    return initialState;
  }),
  withMethods((store, branchService = inject(BranchService)) => {
    const loadBranch = rxMethod(
      pipe(
        tap(() =>
          patchState(store, {
            isLoading: true,
          }),
        ),
        switchMap((response) =>
          branchService.getAllBranches().pipe(
            tap((response) =>
              patchState(store, {
                isLoading: false,
                branches: response,
              }),
            ),
            catchError((e) => {
              const error = e.message || 'حدث خطأ اثناء تحميل الفروع';
              toast.error(error);
              patchState(store, {
                isLoading: false,
                error: error,
              });
              return EMPTY;
            }),
          ),
        ),
      ),
    );
    const loadBranchById = rxMethod<number>(
      pipe(
        tap(() => patchState(store, { isLoading: true })),
        switchMap((id) =>
          branchService.getBranchById(id).pipe(
            tap((response) => patchState(store, { isLoading: false, branchDetails: response })),
            catchError((e) => {
              const error = e.message || 'حدث خطأ اثناء تحميل الفروع';
              toast.error(error);
              patchState(store, {
                isLoading: false,
                error: error,
              });
              return EMPTY;
            }),
          ),
        ),
      ),
    );

    return {
      loadBranch,
      loadBranchById,

      createBranch: rxMethod<CreateBranchRequest>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((data) =>
            branchService.createBranch(data).pipe(
              tap(() => {
                patchState(store, { isLoading: false });
                loadBranch({});
              }),
              catchError((e) => {
                const error = e.message || 'حدث خطأ اثناء انشاء الفروع';
                toast.error(error);
                patchState(store, {
                  isLoading: false,
                  error: error,
                });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),
      updateBranch: rxMethod<{ id: number; data: UpdateBranchRequest }>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap(({ id, data }) =>
            branchService.updateBranch(id, data).pipe(
              tap(() => {
                patchState(store, { isLoading: false });
                loadBranch({});
              }),
              catchError((e) => {
                const error = e.message || 'حدث خطأ اثناء تعديل الفروع';
                toast.error(error);
                patchState(store, {
                  isLoading: false,
                  error: error,
                });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),
      deleteBranch: rxMethod<number>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((id) =>
            branchService.deleteBranch(id).pipe(
              tap(() => {
                patchState(store, { isLoading: false });
                loadBranch({});
              }),
              catchError((e) => {
                const error = e.message || 'حدث خطأ اثناء حذف الفروع';
                toast.error(error);
                patchState(store, {
                  isLoading: false,
                  error: error,
                });
                return EMPTY;
              }),
            ),
          ),
        ),
      ),
    };
  }),
  // withMethods((store, branchService = inject(BranchService)) => ({
  //   async loadBranches() {
  //     patchState(store, { isLoading: true, error: null });
  //     try {
  //       const response = await firstValueFrom(branchService.getAllBranches());
  //       patchState(store, {
  //         isLoading: false,
  //         branches: response,
  //       });
  //     } catch (err: any) {
  //       patchState(store, {
  //         isLoading: false,
  //         error: err.message || 'حدث خطأ أثناء تحميل الفروع',
  //       });
  //     }
  //   },
  //   async loadBranchById(id: number) {
  //     patchState(store, { isLoading: true, error: null });
  //     try {
  //       const response = await firstValueFrom(branchService.getBranchById(id));
  //       patchState(store, {
  //         isLoading: false,
  //         branchDetails: response,
  //       });
  //     } catch (err: any) {
  //       patchState(store, {
  //         isLoading: false,
  //         error: err.message || 'حدث خطأ أثناء تحميل الفرع',
  //       });
  //     }
  //   },
  //   clearSelectedBranch() {
  //     patchState(store, { branchDetails: null });
  //   },
  //   async updateBranch(id: number, branchForm: UpdateBranchRequest) {
  //     patchState(store, { isLoading: true, error: null });
  //     try {
  //       const isSuccess = await firstValueFrom(branchService.updateBranch(id, branchForm));
  //
  //       if (isSuccess) {
  //         const updatedBranches = store.branches().map((branch: BranchListModel) => {
  //           if (branch.id === id) {
  //             return {
  //               ...branch,
  //               name: branchForm.Name,
  //               code: branchForm.Code,
  //               phoneNumber: branchForm.PhoneNumber,
  //             };
  //           }
  //           return branch;
  //         });
  //
  //         const currentDetails = store.branchDetails();
  //         const updatedDetails =
  //           currentDetails && currentDetails.id === id
  //             ? {
  //                 ...currentDetails,
  //                 name: branchForm.Name,
  //                 code: branchForm.Code,
  //                 phoneNumber: branchForm.PhoneNumber,
  //                 street: branchForm.Street,
  //                 city: branchForm.City,
  //                 country: branchForm.Country,
  //                 isActive: branchForm.IsActive,
  //               }
  //             : currentDetails;
  //
  //         patchState(store, {
  //           isLoading: false,
  //           branches: updatedBranches,
  //           branchDetails: updatedDetails,
  //         });
  //       } else {
  //         patchState(store, {
  //           isLoading: false,
  //           error: 'لم يتم حفظ التعديلات، يرجى المحاولة مرة أخرى',
  //         });
  //       }
  //     } catch (err: any) {
  //       patchState(store, {
  //         isLoading: false,
  //         error: err.message || 'حدث خطأ أثناء تعديل الفرع',
  //       });
  //     }
  //   },
  //   async createBranch(branchForm: BranchForm) {
  //     patchState(store, { isLoading: true, error: null });
  //     try {
  //       const response = await firstValueFrom(branchService.createBranch(branchForm));
  //       const newBranch: BranchListModel = {
  //         id: (response as any)?.id || Date.now(), // لو الباك إند مش برجع الـ id، بنحط temporary timestamp
  //         name: (response as any)?.name || branchForm.Name,
  //         code: (response as any)?.code || branchForm.Code,
  //         phoneNumber: (response as any)?.phoneNumber || branchForm.PhoneNumber,
  //         isActive: (response as any)?.isActive || branchForm.IsActive,
  //       };
  //
  //       patchState(store, {
  //         isLoading: false,
  //         branches: [...store.branches(), newBranch],
  //       });
  //       toast.success('done');
  //     } catch (err: any) {
  //       patchState(store, {
  //         isLoading: false,
  //         error: err.message || 'حدث خطأ أثناء إضافة الفرع',
  //       });
  //     }
  //   },
  // })),
);
