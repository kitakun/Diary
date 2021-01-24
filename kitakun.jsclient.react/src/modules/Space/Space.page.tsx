import React from 'react';
import { useParams } from 'react-router-dom';
import './Space.scss';
// Locals
import { IReactPropPageType, ISpaceRecordPreview } from 'types';
// Local components
import { customHistory } from '../../index';
import Container from 'library/common/Layout/Container/Container';
import NoRecords from './components/NoRecords/NoRecords';
import CreateNewRecord from './components/CreateNewRecord/CreateNewRecord';
import DateDelimeter from './components/DateDelimeter/DateDelimeter';
import RecordsGrid from './components/RecordsGrid/RecordsGrid';
import RecordView from '../../library/components/RecordView/RecordView';
import DialogRecordView from './dialogs/DialogRecordView/DialogRecordView';


interface ISpaceRouteParams {
    spaceId: string;
}

const tempData = {
    title: 'Title!',
    date: new Date(),
    tags: ['tag1', 'tag2']
} as ISpaceRecordPreview;

function SpacePage(props: IReactPropPageType) {
    let { spaceId } = useParams<ISpaceRouteParams>();
    const date = new Date();
    return (
        <Container>
            <CreateNewRecord></CreateNewRecord>
            <DateDelimeter date={date}></DateDelimeter>
            <RecordsGrid match={props.match}></RecordsGrid>
            {/* <NoRecords date={date}></NoRecords> */}
            {/* <RecordView record={tempData}></RecordView> */}
            {/* <DialogRecordView history={customHistory} match={props.match}></DialogRecordView> */}
        </Container>
    );
}

export default SpacePage;
