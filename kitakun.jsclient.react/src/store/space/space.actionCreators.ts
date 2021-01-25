import { delay } from "library/utils";
import { dispatchStoreAction, ThunkResult } from "store/base.store";
import { LoadingState } from "types"
import * as actionTypes from "./space.actionTypes"
import { DispatchType, SpaceAction, SpaceState, SpaceStateAction } from "./space.types"

export function fetchWelcomePreview() {
    const action: SpaceAction = {
        type: actionTypes.FETCH_SPACES_WELCOME_PREVIEW,
    };

    return loadWelcomePreviews(action);
}

function internalUpdateState(newStateVals: Partial<SpaceState>) {
    const action: SpaceStateAction = {
        type: actionTypes.UPDATE_STATE,
        newStateVals,
    };

    return dispatchStoreAction(action);
}

export function loadWelcomePreviews(action: SpaceAction): ThunkResult<Promise<void>> {
    dispatchStoreAction(action);
    return async (dispatch: DispatchType) => {
        dispatch(
            internalUpdateState({ welcomeRecordsState: LoadingState.InLoading })
        );
        try {
            await delay(2500);
            dispatch(
                internalUpdateState({
                    welcomeRecordsState: LoadingState.Loaded,
                    welcomeRecordsPreview: [
                        {
                            id: '1',
                            title: 'hi1',
                            tags: ['hi1'],
                            date: new Date(),
                            spaceId: '1',
                        },
                        {
                            id: '2',
                            title: 'hi2',
                            tags: ['hi2'],
                            date: new Date(),
                            spaceId: '1',
                        },
                        {
                            id: '3',
                            title: 'hi3',
                            tags: ['hi2'],
                            date: new Date(),
                            spaceId: '1',
                        },
                        {
                            id: '4',
                            title: 'hi4',
                            tags: ['hi2'],
                            date: new Date(),
                            spaceId: '1',
                        }
                    ]
                })
            );
        } catch (er) {
            dispatch(
                internalUpdateState({ welcomeRecordsState: LoadingState.Error })
            );
        }
    }
}