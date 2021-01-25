import React, { Dispatch, useState } from 'react';
import { CSSTransition } from 'react-transition-group';
// redux
import { useDispatch } from 'react-redux';
// Locals
import { IReactPropType, ISpaceRecord } from 'types';
import './CreateNewRecord.scss';
// Components
import Panel from 'library/common/Layout/Panel/Panel';
import TagSelector from 'library/common/Controls/TagSelector/TagSelector';
import Wysiwyg from 'library/common/Controls/Wysiwyg/Wysiwyg';
import Selector, { ISelectOption } from 'library/common/Controls/Selector/Selector';

const tempData = {
    createdAt: new Date(),
    markdownText: 'hi',
    shortDescription: 'hi descr',
    tokenUrl: 'hellow',
} as ISpaceRecord;

interface ICreateNewRecordProp extends IReactPropType {
    createNewRecord(newRecord: ISpaceRecord): void;
}

function CreateNewRecord(props: ICreateNewRecordProp) {
    const protectionOptions = [
        {
            value: '0',
            label: 'Видим всем'
        },
        {
            value: '1',
            label: 'Доступ по ссылке'
        },
    ] as ISelectOption[];
    //states
    const [isVisible, setVisible] = useState<boolean>(false)
    const onToggleCreateNewRecord = () => setVisible(!isVisible);
    // redux
    const dispatch = useDispatch() as Dispatch<any>;
    const createRecord = React.useCallback(
        (record: ISpaceRecord) => dispatch(props.createNewRecord(record)),
        [dispatch, props.createNewRecord]
    );

    return (
        <div >
            <button className="btn create-record-button" type="button" aria-expanded="false" onClick={onToggleCreateNewRecord}>
                Создать запись
            </button>

            <CSSTransition
                in={isVisible}
                timeout={500}
                classNames='create-record-animation'
                unmountOnExit
                appear
            >
                <Panel variant={'white'} classNames={'create-new-record'}>
                    <form>
                        <div className="form-group">
                            <label className="control-label" htmlFor="ShortDescription">Краткое описание</label>
                            <input className="form-control" id="ShortDescription" name="ShortDescription" type="text" readOnly value="" />
                        </div>

                        <div className="form-group">
                            <label className="control-label" htmlFor="OnDate">Дата записи</label>
                            <input className="form-control" id="OnDate" name="OnDate" type="datetime-local" readOnly value="2021-01-19T00:00:00.000" />
                        </div>

                        <div className="form-group">
                            <label className="control-label" htmlFor="tagSelector">Теги</label>
                            <TagSelector></TagSelector>
                        </div>

                        <div className="form-group">
                            <label className="control-label" htmlFor="insideText">Внутренний текст</label>
                            {/* <input className="form-control" id="insideText" name="insideText" /> */}
                            <Wysiwyg></Wysiwyg>
                        </div>

                        <div className="form-group">
                            <label className="control-label" htmlFor="insideText">Настройки приватности</label>
                            {/* <input className="form-control" id="insideText" name="insideText" /> */}
                            <Selector values={protectionOptions}></Selector>
                        </div>

                        <div className="form-group">
                            <label className="control-label" htmlFor="codeText">Закодировать текст</label>
                            <input className="form-control" type="checkbox" id="codeText" name="codeText" />
                        </div>

                        <div className="form-group">
                            <label className="control-label" htmlFor="codePass">Пароль</label>
                            <input className="form-control" id="codePass" name="codePass" />
                        </div>

                        <button className="btn" onClick={() => createRecord(tempData)} type="button" aria-expanded="false">
                            Создать запись
                        </button>
                    </form>
                </Panel>
            </CSSTransition>
        </div >
    );
}

export default CreateNewRecord;
