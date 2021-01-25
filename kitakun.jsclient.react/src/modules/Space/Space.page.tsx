import React from 'react';
import './Space.scss';
// Locals
import { IReactPropPageType } from 'types';
// Local components
// import { customHistory } from '../../index';
import Container from 'library/common/Layout/Container/Container';
// import NoRecords from './components/NoRecords/NoRecords';
import CreateNewRecord from './components/CreateNewRecord/CreateNewRecord';
import DateDelimeter from './components/DateDelimeter/DateDelimeter';
import RecordsGrid from './components/RecordsGrid/RecordsGrid';
// import RecordView from '../../library/components/RecordView/RecordView';
// import DialogRecordView from './dialogs/DialogRecordView/DialogRecordView';
import { createRecord } from 'store/record/record.actionCreators';

// interface ISpaceRouteParams {
//     spaceId: string;
// }

function SpacePage(props: IReactPropPageType) {
    // const { } = useParams<ISpaceRouteParams>();
    const date = new Date();
    return (
        <Container>
            <CreateNewRecord createNewRecord={createRecord}></CreateNewRecord>
            <DateDelimeter date={date}></DateDelimeter>
            <RecordsGrid match={props.match}></RecordsGrid>
            {/* <NoRecords date={date}></NoRecords> */}
            {/* <RecordView record={tempData}></RecordView> */}
            {/* <DialogRecordView history={customHistory} match={props.match}></DialogRecordView> */}
        </Container>
    );
}

export default SpacePage;
