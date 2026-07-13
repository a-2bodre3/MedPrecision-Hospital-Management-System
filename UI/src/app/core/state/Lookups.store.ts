import { Lookups } from '../model/Lookups.model';
import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { inject } from '@angular/core';
import { LookupsService } from '../service/lookups.service';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { catchError, EMPTY, pipe, switchMap, tap } from 'rxjs';
import { toast } from '@spartan-ng/brain/sonner';

interface ILookupsState {
  lookups: Lookups[];
  loading: boolean;
  error: string | null;
}

const initialState: ILookupsState = {
  lookups: [],
  loading: false,
  error: null,
};

export const LookupsState = signalStore(
  { providedIn: 'root' },
  withDevtools('LookupsStore'),
  withState(() => initialState),
  withMethods((store, lookupsService = inject(LookupsService)) => {
    return {
      getLookups: rxMethod<void>(
        pipe(
          tap(() => patchState(store, { loading: true })),
          switchMap(() =>
            lookupsService.getLookups().pipe(
              tap((lookups) => patchState(store, { loading: false, lookups: lookups })),
              catchError((e) => {
                const error = e.message || 'حدث خطاء اثناء تحميل اللوك اب';
                toast.error(error);
                patchState(store, {
                  loading: false,
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
);
