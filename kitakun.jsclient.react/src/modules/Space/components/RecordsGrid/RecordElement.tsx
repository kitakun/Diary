import React from 'react';
// Locals
import './RecordsGrid.scss';
import { IMatch, IReactPropType, ISpaceRecordPreview } from 'types';
import { formatDate } from 'library/utils';
import { Link } from 'react-router-dom';

interface IDateDelimetedProps extends IReactPropType {
    record: ISpaceRecordPreview;
    match: IMatch;
}

function RecordElement(props: IDateDelimetedProps) {
    const formatedDate = formatDate(props.record.date, 'dd.MM.yyyy');
    return (
        <div className="d-card">
            <div className="card-body">
                <h5 className="card-title">{props.record.title}</h5>
                <h6 className="card-subtitle">{formatedDate}</h6>
                <div className="badge-container">
                    {props.record.tags.map((tag, i) => <span key={i} className="badge">{tag}</span>)}
                </div>

                <div className="float-right">
                    <Link to={`${props.match!.url}/view/${props.record.id}`}>Прочитать</Link>
                </div>
            </div>
        </div>
    );
}

export default RecordElement;
