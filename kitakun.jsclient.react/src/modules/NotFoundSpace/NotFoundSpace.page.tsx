import React from 'react';
import { useParams } from 'react-router-dom';
// Locals
import { IReactPropType } from 'types';
// Local components
import Container from 'library/common/Layout/Container/Container';

interface ISpaceRouteParams {
    spaceId: string;
}

function NotFoundSpacePage(props: IReactPropType) {
    let { spaceId } = useParams<ISpaceRouteParams>();
    return (
        <Container>
            <div className="text-center">
                <h1>Не найдено</h1>

                <h2>По адресу <b>{spaceId}</b> нет странички!</h2>
                <p>Возможно она удалена или скрыта настройками приватности</p>
            </div>
        </Container>
    );
}

export default NotFoundSpacePage;
