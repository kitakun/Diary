import { DispatchType, SpaceState } from '../store';

export interface IDispatcher {
    dispatch: DispatchType;
}

export interface IRootStore {
    spaceStore: SpaceState;
}
