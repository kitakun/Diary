// grpc
import { HomeClient } from "client/HomeServiceClientPb";
import { FetchHomePreviewRecordsRequest } from "client/home_pb";
// locals
import { createClient, delay } from "library/utils";
import { dispatchStoreAction, ThunkResult } from "store/base.store";
import { ISpaceRecordPreview, LoadingState } from "types"
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

            const client = createClient(HomeClient);
            const grpcResponse = await client.fetchHomePreviewRecords(new FetchHomePreviewRecordsRequest(), null);

            dispatch(
                internalUpdateState({
                    welcomeRecordsState: LoadingState.Loaded,
                    welcomeRecordsPreview: grpcResponse?.getRecordsList().map(m => {
                        return {
                            id: m.getId(),
                            date: m.getDate()?.toDate(),
                            spaceId: m.getSpaceid(),
                            title: m.getTitle(),
                            tags: m.getTagsList(),
                        } as ISpaceRecordPreview;
                    })
                })
            );
        } catch (er) {
            console.error(er);
            dispatch(
                internalUpdateState({ welcomeRecordsState: LoadingState.Error })
            );
        }
    }
}