import { useStore } from "react-redux";
import { ThunkAction, ThunkDispatch } from "redux-thunk";
import { IRootStore } from "types";

export interface IStoreAction {
    type: string
}

export type ThunkResult<R> = ThunkAction<R, IRootStore, undefined, any>;
export type DiaryThunkDispatch = ThunkDispatch<IRootStore, any, IStoreAction>;

export type EmitDispatchType = (dispatch: (data: IStoreAction) => any) => any;

export function dispatchStoreAction(action: IStoreAction): EmitDispatchType {
    return (dispatch: (data: IStoreAction) => void) => {
        dispatch(action);
    };
}

export function useDiaryStore() {
    const baseStore = useStore<IRootStore>();
    return {
        dispatch: baseStore.dispatch as DiaryThunkDispatch,
        getState: baseStore.getState,
        replaceReducer: baseStore.replaceReducer,
        subscribe: baseStore.subscribe,
    };
}