import { patchState, signalStore, withComputed, withMethods, withState } from '@ngrx/signals';
import { withDevtools } from '@angular-architects/ngrx-toolkit';
import { computed, inject } from '@angular/core';
import { RoomService } from '../service/room.service';
import { catchError, EMPTY, firstValueFrom, pipe, switchMap, tap } from 'rxjs';
import { RoomDetailsResponse, RoomResponse } from '../model/RoomResponse.model';
import { RoomType } from '../../../core/enum/RoomType.enum';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { CreateRoomCommand, UpdateRoomCommand } from '../model/RoomCommand.model';
import { RoomCreateComponent } from '../components/room-create/room-create.component';

interface IRoomState {
  isLoading: boolean;
  error: string | null;
  rooms: RoomResponse[];
  roomDetails: RoomDetailsResponse | null;
  selectedType: RoomType | null;
}

const initialState: IRoomState = {
  isLoading: false,
  error: null,
  rooms: [],
  roomDetails: null,
  selectedType: null,
};

export const RoomStore = signalStore(
  { providedIn: 'root' },
  withDevtools('RoomStore'),
  withState(() => initialState),
  withComputed((store) => ({
    filteredRooms: computed(() => {
      const rooms = store.rooms();
      const type = store.selectedType();
      return rooms.filter((d) => d.roomType === type);
    }),
  })),

  withMethods((store, roomService = inject(RoomService)) => {
    const loadRooms = rxMethod<void>(
      pipe(
        tap(() => patchState(store, { isLoading: true })),
        switchMap(() =>
          roomService.getRooms().pipe(
            tap((data) => patchState(store, { isLoading: false, rooms: data })),
            catchError((e) => {
              const error: string = e.message || 'حدث خطاء اثناء تخميل الغرف';
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

    const removeRoomDetails = () => {
      patchState(store, {
        roomDetails: null,
      });
    };

    return {
      loadRooms,
      removeRoomDetails,
      GetRoomDetails: rxMethod<number>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((id: number) =>
            roomService.getRoomById(id).pipe(
              tap((data) => patchState(store, { isLoading: false, roomDetails: data })),
              catchError((e) => {
                const error: string = e.message || 'حدث خطاء اثناء تخميل الغرف';
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
      CreateRoom: rxMethod<CreateRoomCommand>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((data: CreateRoomCommand) =>
            roomService.createRoom(data).pipe(
              tap(() => {
                loadRooms();
                patchState(store, {
                  isLoading: false,
                });
              }),
              catchError((e) => {
                const error: string = e.message || 'حدث خطاء اثناء انشاء الغرف';
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
      UpdateRoom: rxMethod<{ id: number; data: UpdateRoomCommand }>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap(({ id, data }) =>
            roomService.updateRoom(id, data).pipe(
              tap(() => {
                loadRooms();
                patchState(store, {
                  isLoading: false,
                });
              }),
              catchError((e) => {
                const error: string = e.message || 'حدث خطاء اثناء تعديل الغرف';
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
      DeleteRoom: rxMethod<number>(
        pipe(
          tap(() => patchState(store, { isLoading: true })),
          switchMap((id) =>
            roomService.deleteRoom(id).pipe(
              tap(() => {
                loadRooms();
                patchState(store, {
                  isLoading: false,
                });
              }),
              catchError((e) => {
                const error: string = e.message || 'حدث خطاء اثناء حذف الغرف';
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
);
