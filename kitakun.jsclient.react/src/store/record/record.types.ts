import { IStoreAction } from 'store/base.store';
import { ISpace, ISpaceRecord, LoadingState } from '../../types';

export type RecordState = {
    state: LoadingState;
    records: ISpaceRecord[];
}

export type RecordAction = IStoreAction & {
    record: ISpaceRecord
}

export type DispatchType = (args: RecordAction) => RecordAction;
