import React from 'react';
import './LinkToDiaryButton.scss';
// Locals
import { IReactPropType } from 'types';
import { Link } from 'react-router-dom';

interface LinkToDiaryButtonProps extends IReactPropType {
    spaceId: string;
    text: string;
}

function LinkToDiaryButton(props: LinkToDiaryButtonProps) {
    return (
        <div>
            <Link className="link-to-diary-button" to={`/space/${props.spaceId}`}>
                {props.text}
            </Link>
        </div>
    );
}

export default LinkToDiaryButton;
