import React from 'react';
import { useParams } from 'react-router-dom';
import './Space.scss';
// Locals
import { IReactPropType } from 'types';
// Local components
import Container from 'library/common/Layout/Container/Container';
import NoRecords from './components/NoRecords/NoRecords';
import CreateNewRecord from './components/CreateNewRecord/CreateNewRecord';

interface ISpaceRouteParams {
    spaceId: string;
}

function SpacePage(props: IReactPropType) {
    let { spaceId } = useParams<ISpaceRouteParams>();
    const date = new Date();
    return (
        <Container>
            <CreateNewRecord></CreateNewRecord>
            <h4>spaceId={spaceId}</h4>
            <NoRecords date={date}></NoRecords>
        </Container>
    );
}

export default SpacePage;
