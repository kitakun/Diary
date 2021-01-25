import { IStoreAction } from 'store/base.store';
import { LoadingState } from 'types';
import * as actionTypes from './space.actionTypes';
import {
    SpaceState,
    SpaceStateAction
} from './space.types';

const initialState: SpaceState = {
    state: LoadingState.NotLoaded,
    spaces: [],
}

const reducer = (
    state: SpaceState = initialState,
    action: IStoreAction,
): SpaceState => {
    switch (action.type) {
        case actionTypes.ADD_SPACE:
            break;
        case actionTypes.UPDATE_STATE:
            const newData = (action as SpaceStateAction).newStateVals;
            return {
                ...state,
                ...newData,
            }
    }
    return state
}

export default reducer