import React, { useState } from 'react';
// Locals
import { IReactPropType, ISpaceRecordPreview } from 'types';
// Local components
import Panel from 'library/common/Layout/Panel/Panel';
import Loading from 'library/common/Layout/Loading/Loading';
import NavigateBackButton from 'library/common/Layout/NavigateBackButton/NavigateBackButton';
import RecordView from 'library/components/RecordView/RecordView';

interface IRecordPageState {
    isLoading?: boolean;
    isLoaded?: boolean;
    record?: ISpaceRecordPreview;
}

function RecordViewPage(props: IReactPropType) {
    const [state, setState] = useState<IRecordPageState>({});
    if (!state.isLoading && !state.isLoaded) {
        setTimeout(() => {
            setState({
                isLoading: false,
                isLoaded: true,
                record: {
                    date: new Date(),
                    id: '123',
                    tags: ['loaded', 'wah'],
                    title: 'Loaded!!',
                    spaceId: '1'
                }
            });
        }, 2000);
        setState({ ...state, isLoading: true });
    }

    return (
        <Panel variant={'white'}>
            <NavigateBackButton></NavigateBackButton>
            { state.isLoading && <Loading></Loading>}
            { state.record && <RecordView record={state.record}></RecordView>}

        </Panel >
    );
}

export default RecordViewPage;
