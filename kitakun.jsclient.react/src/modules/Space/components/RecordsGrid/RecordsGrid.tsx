import React from 'react';
// Locals
import './RecordsGrid.scss';
import { IMatch, IReactPropType, ISpaceRecordPreview } from 'types';
import RecordElement from './RecordElement';

const recordPreview = {
    id: '777',
    title: 'Test Record',
    date: new Date(),
    tags: ['npbs', 'other', 'hi-hello', 'hi2', 'h3', 'h4', 'npbs', 'other', 'hi-hello', 'hi2', 'h3', 'h4']
} as ISpaceRecordPreview;

interface IRecordsGridProps extends IReactPropType {
    match: IMatch;
}

function RecordsGrid(props: IRecordsGridProps) {
    return (
        <div className="d-grid">
            <RecordElement record={recordPreview} match={props.match}></RecordElement>
            <RecordElement record={recordPreview} match={props.match}></RecordElement>
            <RecordElement record={recordPreview} match={props.match}></RecordElement>
        </div>
    );
}

export default RecordsGrid;
