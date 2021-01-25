import { useState } from 'react';
// 3paty
import ReactQuill from 'react-quill';
// Locals
import './RecordView.scss';
import { IReactPropType, ISpaceRecordPreview } from 'types';

interface IRecordViewProps extends IReactPropType {
    record: ISpaceRecordPreview;
}

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
