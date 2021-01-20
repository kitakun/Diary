import React from 'react';
// Locals
import { IReactPropType } from 'types';
import { formatDate } from 'library/utils/date.utils';
import Container from 'library/common/Layout/Container/Container';

interface INoRecordsProps extends IReactPropType {
    date: Date;
}

function NoRecords(props: INoRecordsProps) {
    const formatedDate = formatDate(props.date, 'dd.MM.yyyy');
    return (
        <Container>
            <div className="col-9">
                <div className="text-center">
                    <h2>Пусто</h2>
                    <p>На дату {formatedDate} нет записей</p>
                </div>
            </div>
        </Container>
    );
}

export default NoRecords;
