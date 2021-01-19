import React from 'react';
import './Panel.scss';
// Locals
import Container from '../Container/Container';
import { IReactPropType } from 'types';

function Panel(props: IReactPropType) {
    return (
        <Container styleName={props.styleName}>
            {props.children}
        </Container>
    );
}

export default Panel;
