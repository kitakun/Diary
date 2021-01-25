import React from 'react';
// Locals
import './StringDelimeter.scss';
import { IReactPropType } from 'types';

interface IStringDelimetedProps extends IReactPropType {
    text: string;
}

function StringDelimeter(props: IStringDelimetedProps) {
    return (
        <div className="d-s-separator">{props.text}</div>
    );
}

export default StringDelimeter;
