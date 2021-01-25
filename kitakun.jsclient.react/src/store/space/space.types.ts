import { EmitDispatchType, IStoreAction } from 'store/base.store';
import { ISpace, ISpaceRecordPreview, LoadingState } from 'types';

export type SpaceState = {
    // welcome state
    welcomeRecordsState: LoadingState;
    welcomeRecordsPreview: ISpaceRecordPreview[];
    // 
    spaces: ISpace[];
}

export type SpaceAction = IStoreAction & {
    space?: ISpace
}

export type SpaceStateAction = IStoreAction & {
    newStateVals?: Partial<SpaceState>;
}

export declare type SpaceActionTypes = EmitDispatchType | SpaceAction | SpaceStateAction;

export type DispatchType = (args: SpaceActionTypes) => SpaceAction;
