import React from 'react';
// Locals
import './PreviewRecord.scss';
import { IMatch, IReactPropType, ISpaceRecordPreview } from 'types';
import { Link } from 'react-router-dom';

interface IPreviewDateDelimetedProps extends IReactPropType {
    record: ISpaceRecordPreview;
    match: IMatch;
    showTags?: boolean;
}

function PreviewRecordElement(props: IPreviewDateDelimetedProps) {
    return (
        <div className="p-card">
            <div className="card-body">
                <h5 className="card-title">{props.record.title}</h5>
                {
                    props.showTags && <div className="badge-container">
                        {props.record.tags.map((tag, i) => <span key={i} className="badge">{tag}</span>)}
                    </div>
                }

                <div className="float-right">
                    <Link to={`/space/${props.record.spaceId}`}>Перейти</Link>
                </div>
            </div>
        </div>
    );
}

export default PreviewRecordElement;
