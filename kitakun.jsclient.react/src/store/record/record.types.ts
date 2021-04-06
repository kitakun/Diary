import { EmitDispatchType, IStoreAction } from 'store/base.store';
import { ICreateSpaceRecord, ISpaceRecord } from '../../types';

export type RecordState = {
    states: RecordStoreStates[];
    records: ISpaceRecord[];
}

export enum RecordStoreStates {
    CreateNewRecord = 0,
    LoadingRecords = 2,
    RecordsAreLoaded = 3,
}

export type StateAction = IStoreAction & {
    state: RecordStoreStates;
}

export type RecordAction = IStoreAction & {
    type: string;
    record: ICreateSpaceRecord
}

export type RecordsStateAction = Partial<RecordState> & IStoreAction & {

}

export declare type RecordActionTypes = EmitDispatchType | RecordsStateAction | RecordAction | StateAction;

export type DispatchType = (args: RecordActionTypes) => RecordAction;
