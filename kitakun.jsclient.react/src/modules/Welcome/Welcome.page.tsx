import React from 'react';
import './Welcome.scss';
// store
import { useSelector } from 'react-redux';
import { fetchWelcomePreview } from 'store/space/space.actionCreators';
import { useDiaryStore } from 'store/base.store';
// Locals
import { IReactPropPageType, IRootStore, ISpaceRecordPreview, LoadingState } from 'types';
// Local components
import Panel from 'library/common/Layout/Panel/Panel';
import LinkToDiaryButton from 'library/common/Controls/LinkToDiaryButton/LinkToDiaryButton';
import Loading from 'library/common/Layout/Loading/Loading';
import StringDelimeter from '../Space/components/StringDelimeter/StringDelimeter';
import PreviewRecordGrid from './components/PreviewRecordGrid/PreviewRecordGrid';

function WelcomePage(props: IReactPropPageType) {
    const { dispatch } = useDiaryStore();
    const welcomePreviewState = useSelector<IRootStore>(state => state.spaceStore.welcomeRecordsState);
    const welcomeRecordsPreview = useSelector<IRootStore, ISpaceRecordPreview[]>(state => state.spaceStore.welcomeRecordsPreview);

    let bottomComponent = <></>;
    switch (welcomePreviewState) {
        case LoadingState.NotLoaded:
            dispatch(fetchWelcomePreview());
            bottomComponent = <Loading classNames="small-loading"></Loading>;
            break;
        case LoadingState.InLoading:
            bottomComponent = <Loading classNames="small-loading"></Loading>;
            break;
        case LoadingState.Loaded:
            bottomComponent =
                <>
                    {welcomeRecordsPreview?.length > 0 && <StringDelimeter text={'Свежейшие записи'}></StringDelimeter>}
                    <PreviewRecordGrid records={welcomeRecordsPreview} match={props.match}></PreviewRecordGrid>
                </>;
            break;
        case LoadingState.Error:
            console.log('err :c');
            break;
    }

    let doIhasSpace = 2;
    let openOrCreateSpaceComponent = <></>;
    switch (doIhasSpace) {
        case 1:
            openOrCreateSpaceComponent = <span className="text-muted">Для создания дневника войдите в систему</span>;
            break;
        case 2:
            openOrCreateSpaceComponent = <LinkToDiaryButton spaceId="1" text="Открыть ваш дневник"></LinkToDiaryButton>;
            break;
        case 3:
            openOrCreateSpaceComponent = <LinkToDiaryButton spaceId="0" text="Создать свой дневник"></LinkToDiaryButton>;
            break;
    }

    return (
        <Panel>
            <div className="welcome-page">
                <h1 className="header">Привет!</h1>
                <div className="text">Вы попали на сайт-дневник-ежедневник <small>называйте как хотите</small></div>
                <div className="text">Предполагается что здесь вы можете записывать какие-то события вашей жизни и :</div>
                <br />
                <div className="text">
                    <ol style={{ textAlign: 'left', margin: '0 auto', width: '600px' }}>
                        <li>Фильтровать события по дате</li>
                        <li>Добавлять теги на события</li>
                        <li>Ограничивать блог по ссылке/паролю/видим всем</li>
                        <li>Ограничивать каждую запись по паролю/ссылке/скрывать полностью от всех</li>
                        <li>Записи с паролями кодируются и никто не сможет получить к ним доступ кроме вас</li>
                    </ol>
                </div>
                {openOrCreateSpaceComponent}
            </div>
            <div>
                {bottomComponent}
            </div>
        </Panel>
    );
}

export default WelcomePage;
