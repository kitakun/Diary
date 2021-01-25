import React from 'react';
// Locals
import './PreviewRecord.scss';
import { IMatch, IReactPropType, ISpaceRecordPreview } from 'types';
import PreviewRecordElement from './PreviewRecordElement';
import { takeEveryToChunks } from 'library/utils';

interface IPreviewRecordsGridProps extends IReactPropType {
    match: IMatch;
    records: ISpaceRecordPreview[];
}

function PreviewRecordGrid(props: IPreviewRecordsGridProps) {
    const showEvery = 3;
    const lines = takeEveryToChunks(props.records, showEvery);
    return (
        <div className="p-grid">
            {
                lines.map((arrLine, j) => {
                    return <div key={j} className="p-elements-row">
                        {arrLine.map((rec, i) => <PreviewRecordElement key={j * showEvery + i} record={rec} match={props.match}></PreviewRecordElement>)}
                    </div>;
                })}
        </div>
    );
}

export default PreviewRecordGrid;
