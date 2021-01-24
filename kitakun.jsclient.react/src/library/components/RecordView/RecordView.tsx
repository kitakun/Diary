import React, { useState } from 'react';
// 3paty
import ReactQuill from 'react-quill';
// Locals
import './RecordView.scss';
import { IReactPropType, ISpaceRecord, ISpaceRecordPreview } from 'types';

interface IRecordViewProps extends IReactPropType {
    record: ISpaceRecordPreview;
}

const tempData = {
    id: '1',
    createdAt: new Date(),
    markdownText: '<b>hi</b>',
    shortDescription: 'short descr',
    tokenUrl: 'token-token',
} as ISpaceRecord;

function RecordView(props: IRecordViewProps) {
    const [value, setValue] = useState('');

    return (
        <div className="d-record-view">
            <h3>{props.record.title}</h3>
            <ReactQuill theme="snow" value={value} onChange={setValue} />
        </div>
    );
}

export default RecordView;
