import { EmitDispatchType, IStoreAction } from 'store/base.store';
import { ICreateSpaceRecord, ISpaceRecord, LoadingState } from '../../types';

export type RecordState = {
    state: LoadingState;
    records: ISpaceRecord[];
}

export type RecordAction = IStoreAction & {
    type: string;
    record: ICreateSpaceRecord
}

export type RecordsStateAction = Partial<RecordState> & IStoreAction & {
    
}

export declare type RecordActionTypes = EmitDispatchType | RecordsStateAction | RecordAction;

export type DispatchType = (args: RecordActionTypes) => RecordAction;
