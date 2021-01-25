import { delay } from "library/utils";
import { dispatchStoreAction, ThunkResult } from "store/base.store";
import { LoadingState } from "types"
import * as actionTypes from "./space.actionTypes"
import { DispatchType, SpaceAction, SpaceState, SpaceStateAction } from "./space.types"

export function fetchSpacesPreview() {
    const action: SpaceAction = {
        type: actionTypes.FETCH_SPACES_PREVIEW,
    };

    return loadLastSpaces(action);
}

function internalUpdateState(newStateVals: Partial<SpaceState>) {
    const action: SpaceStateAction = {
        type: actionTypes.UPDATE_STATE,
        newStateVals,
    };

    return dispatchStoreAction(action)
}

export function loadLastSpaces(action: SpaceAction) : ThunkResult<Promise<void>> {
    return async (dispatch: DispatchType) => {
        dispatch(
            internalUpdateState({
                state: LoadingState.InLoading,
            })
        );
        try {
            await delay(500);
            dispatch(
                internalUpdateState({
                    state: LoadingState.Loaded,
                    spaces: [
                        {
                            id: '1',
                            humanName: 'test1',
                            urlName: 'url1',
                        },
                        {
                            id: '2',
                            humanName: 'test2',
                            urlName: 'url2',
                        }
                    ]
                })
            );
        } catch (er) {
            dispatch(
                internalUpdateState({
                    state: LoadingState.Error,
                })
            );
        }
    }
}