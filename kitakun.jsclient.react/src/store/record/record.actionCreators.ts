import { dispatchStoreAction } from "store/base.store"
import { ISpaceRecord } from "../../types"
import * as actionTypes from "./record.actionTypes"
import { RecordAction } from "./record.types"

export function createRecord(record: ISpaceRecord) {
    const action: RecordAction = {
        type: actionTypes.CREATE_NEW_RECORD,
        record,
    }
debugger
    return dispatchStoreAction(action);
}

// export function loadLastSpaces(action: SpaceAction) {
//     return (dispatch: DispatchType) => {
//         setTimeout(() => {
//             dispatch(action)
//         }, 500)
//     }
// }