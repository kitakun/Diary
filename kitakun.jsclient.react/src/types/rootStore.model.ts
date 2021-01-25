import { Store } from 'redux';
import { DispatchType, SpaceAction, SpaceState } from '../store';

export interface IDispatcher {
    dispatch: DispatchType;
}

export interface IRootStore {
    spaceStore: SpaceState;
}
