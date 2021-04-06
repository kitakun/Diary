import { DispatchType as recordDispatchType, RecordState } from 'store/record/record.types';
import { DispatchType as storeDispatchType, SpaceState } from 'store/space/space.types';

export interface IDispatcher {
    dispatch: recordDispatchType | storeDispatchType;
}

export interface IRootStore {
    spaceStore: SpaceState;
    recordsStore: RecordState;
}
