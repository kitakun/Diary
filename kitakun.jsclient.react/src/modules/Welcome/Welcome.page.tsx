import React, { useEffect } from 'react';
import './Welcome.scss';
// Locals
import { IReactPropType, IRootStore, LoadingState } from 'types';
// Local components
import Panel from 'library/common/Layout/Panel/Panel';
import LinkToDiaryButton from 'library/common/Controls/LinkToDiaryButton/LinkToDiaryButton';
import { useSelector } from 'react-redux';
import { fetchSpacesPreview } from 'store/space/space.actionCreators';
import { useDiaryStore } from 'store/base.store';

function WelcomePage(props: IReactPropType) {
    const { dispatch, subscribe } = useDiaryStore();
    useEffect(() => subscribe(console.log), []);
    const currentSpaceStoreState = useSelector<IRootStore>(state => state.spaceStore.state);
    switch (currentSpaceStoreState) {
        case LoadingState.NotLoaded:
            console.log('before start loading');
            dispatch(fetchSpacesPreview());
            break;
        case LoadingState.InLoading:
            console.log('in loading!');
            break;
        case LoadingState.Loaded:
            console.log('loaded!');
            break;
        case LoadingState.Error:
            console.log('err :c');
            break;
    }


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
