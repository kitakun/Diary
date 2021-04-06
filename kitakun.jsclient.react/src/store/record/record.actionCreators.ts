import { DiaryRecordClient } from "client/DiaryRecordServiceClientPb";
import {
    CreateNewRecordRequest,
    DiaryRecordItem
} from "client/diaryRecord_pb";
import { Timestamp } from "google-protobuf/google/protobuf/timestamp_pb";
// store
import { dispatchStoreAction, ThunkResult } from "store/base.store"
import { ICreateSpaceRecord, LoadingState } from "../../types"
import * as actionTypes from "./record.actionTypes"
import { DispatchType, RecordAction, RecordsStateAction, RecordStoreStates, StateAction } from "./record.types"
// locals
import { createClient, delay } from "library/utils"

export function createRecord(record: ICreateSpaceRecord) {
    const action: RecordAction = {
        type: actionTypes.CREATE_NEW_RECORD,
        record,
    }

    return loadWelcomePreviews(action);
}

function internalUpdateState(newStateVals: Partial<RecordsStateAction>) {
    const action: RecordsStateAction = {
        type: actionTypes.INTERNAL_PART_UPDATE,
        ...newStateVals
    };

    return dispatchStoreAction(action);
}

function internalAddState(stateToAdd: RecordStoreStates) {
    const action: StateAction = {
        type: actionTypes.INTERNAL_ADD_STATE_IF_NO_EXISTS,
        state: stateToAdd,
    };

    return dispatchStoreAction(action);
}

function internalRemoveState(stateToDel: RecordStoreStates) {
    const action: StateAction = {
        type: actionTypes.INTERNAL_Remove_STATE_IF_EXISTS,
        state: stateToDel,
    };

    return dispatchStoreAction(action);
}

export function loadWelcomePreviews(action: RecordAction): ThunkResult<Promise<void>> {
    dispatchStoreAction(action);
    return async (dispatch: DispatchType) => {
        dispatch(internalAddState(RecordStoreStates.CreateNewRecord));
        try {
            await delay(2500);

            createClient(DiaryRecordClient)

            const grpcClient = createClient(DiaryRecordClient);

            const recordToSend = new DiaryRecordItem();
            const createdAtTimestamp = new Timestamp();
            createdAtTimestamp.fromDate(action.record.createdAt!);
            recordToSend
                .setCreatedat(createdAtTimestamp)
                .setMarkdown(action.record.markdownText)
                .setShortdescription(action.record.shortDescription)
                .setTagsList(action.record.selectedTags);

            if (action.record.protectedByPassword) {
                recordToSend
                    .setProtectedbypassword(true)
                    .setPassword(action.record.password!);
            }

            const request = new CreateNewRecordRequest();
            request.setRecord(recordToSend);

            const grpcResponse = await grpcClient.createNewRecord(request, null);
            const response = grpcResponse.toObject();

            if (response?.issuccess) {
                dispatch(internalRemoveState(RecordStoreStates.CreateNewRecord));
                // dispatch(
                //     internalUpdateState({
                //         state: LoadingState.Loaded,
                //         records: [

                //         ]
                //     })
                // );
            }
        } catch (er) {
            console.error(er);
            dispatch(internalRemoveState(RecordStoreStates.CreateNewRecord));
        }
    }
}