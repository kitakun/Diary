import React from 'react';
import classNames from 'classnames';
// Locals
import './Loading.scss';
import { IReactPropType } from 'types';

interface IPanelProps extends IReactPropType {
    classNames?: string | string[];
}


function Loading(props: IPanelProps) {
    return (
        <div className={classNames('spinner-loader', props.classNames)}>Loading...</div>
    );
}

export default Loading;
