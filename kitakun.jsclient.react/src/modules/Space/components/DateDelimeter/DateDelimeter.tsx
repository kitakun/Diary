import React from 'react';
// Locals
import './DateDelimeter.scss';
import { IReactPropType } from 'types';
import { formatDate } from 'library/utils/date.utils';

interface IDateDelimetedProps extends IReactPropType {
    date: Date;
}

function DateDelimeter(props: IDateDelimetedProps) {
    const formatedDate = formatDate(props.date, 'dd.MM.yyyy');
    return (
        <div className="d-d-separator">{formatedDate}</div>
    );
}

export default DateDelimeter;
