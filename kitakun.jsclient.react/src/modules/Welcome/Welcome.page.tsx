import React from 'react';
import './Welcome.scss';
// Locals
import { IReactPropType } from 'types';
// Local components
import Panel from 'library/common/Layout/Panel/Panel';
import LinkToDiaryButton from 'library/common/Controls/LinkToDiaryButton/LinkToDiaryButton';

function WelcomePage(props: IReactPropType) {
    return (
        <Panel>
            <div className="welcome-page">
                <h1 className="header">Привет!</h1>
                <div>Вы попали на сайт-дневник-ежедневник <small>называйте как хотите</small></div>
                <div>Предполагается что здесь вы можете записывать какие-то события вашей жизни и :</div>
                <br />
                <div>
                    <ol style={{ textAlign: 'left', margin: '0 auto', width: '600px' }}>
                        <li>Фильтровать события по дате</li>
                        <li>Добавлять теги на события</li>
                        <li>Ограничивать блог по ссылке/паролю/видим всем</li>
                        <li>Ограничивать каждую запись по паролю/ссылке/скрывать полностью от всех</li>
                        <li>Записи с паролями кодируются и никто не сможет получить к ним доступ кроме вас</li>
                    </ol>
                </div>
                <div>
                    {/* <span className="text-muted">Для создания дневника войдите в систему</span> */}
                    <LinkToDiaryButton spaceId="1" text="Открыть ваш дневник"></LinkToDiaryButton>
                    {/* <LinkToDiaryButton spaceId="0" text="Создать свой дневник"></LinkToDiaryButton> */}
                </div>
            </div>
        </Panel>
    );
}

export default WelcomePage;
