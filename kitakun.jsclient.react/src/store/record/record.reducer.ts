import { IStoreAction } from 'store/base.store';
import * as actionTypes from './record.actionTypes';
import {
    RecordState,
    RecordsStateAction,
    StateAction,
} from './record.types';

const initialState: RecordState = {
    states: [],
    //
    records: [],
}

const reducer = (
    state: RecordState = initialState,
    action: IStoreAction,
): RecordState => {
    switch (action.type) {
        case actionTypes.INTERNAL_PART_UPDATE:
            debugger
            const newData = (action as RecordsStateAction);
            return {
                ...state,
                ...newData,
            }

        case actionTypes.INTERNAL_Remove_STATE_IF_EXISTS:
            const stateToRemoveData = (action as StateAction).state;
            const removeIndex = state.states.indexOf(stateToRemoveData);
            if (removeIndex >= 0) {
                state.states.splice(removeIndex, 1);
                return {
                    ...state,
                    states: [...state.states],
                };
            }
            return state;

        case actionTypes.INTERNAL_ADD_STATE_IF_NO_EXISTS:
            const stateToAddData = (action as StateAction).state;
            state.states.push(stateToAddData);
            return {
                ...state,
                states: [...state.states],
            };
    }
    return state
}

export default reducer