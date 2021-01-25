import { EmitDispatchType, IStoreAction } from 'store/base.store';
import { ISpace, LoadingState } from 'types';

export type SpaceState = {
    state: LoadingState;
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
