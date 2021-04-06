import React, { Dispatch, useState } from 'react';
import { CSSTransition } from 'react-transition-group';
// datepicker
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
// redux
import { createRecord } from 'store/record/record.actionCreators';
import { useSelector } from 'react-redux';
import { RecordStoreStates } from 'store/record/record.types';
import { useDiaryStore } from 'store';
// Locals
import { ICreateSpaceRecord, IReactPropType, IRootStore } from 'types';
import './CreateNewRecord.scss';
// Components
import Panel from 'library/common/Layout/Panel/Panel';
import TagSelector from 'library/common/Controls/TagSelector/TagSelector';
import Wysiwyg from 'library/common/Controls/Wysiwyg/Wysiwyg';
import Selector, { ISelectOption } from 'library/common/Controls/Selector/Selector';

interface ICreateNewRecordProp extends IReactPropType {
}

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

function CreateNewRecord(props: ICreateNewRecordProp) {
    // redux stuff
    const { dispatch } = useDiaryStore();
    const storeRecordStates = useSelector<IRootStore, RecordStoreStates[]>(state => state.recordsStore.states);

    // states
    const [isVisible, setVisible] = useState<boolean>(false)
    const onToggleCreateNewRecord = () => setVisible(!isVisible);
    const isFormDisabled = storeRecordStates.some(s => s === RecordStoreStates.CreateNewRecord);
    // datetime
    const [startDate, setStartDate] = useState(new Date());
    const setDateAction = (date: Date) => setStartDate(date);
    // tags
    const [selectedTags, setSelectedTags] = useState([] as string[]);
    // long text
    const [wysiwygValue, setWysiwygValue] = useState('');
    // visibility option
    const [selectedVisibilityOption, setVisbilityOption] = useState<ISelectOption>(protectionOptions[0]);

    // refs
    const shortDescription = React.createRef() as React.RefObject<HTMLInputElement>;
    const protectWithPassBool = React.createRef() as React.RefObject<HTMLInputElement>;
    const passwordRef = React.createRef() as React.RefObject<HTMLInputElement>;
    // create callback
    const callCreateRecord = () => {
        const recordData = {
            markdownText: wysiwygValue,
            shortDescription: shortDescription.current?.value,
            createdAt: startDate,
            selectedTags: selectedTags,
            visibilityOption: selectedVisibilityOption,
            protectedByPassword: protectWithPassBool.current?.checked,
            password: passwordRef.current?.value,
        } as ICreateSpaceRecord;

        dispatch(createRecord(recordData));
    };

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
                        <fieldset disabled={isFormDisabled}>
                            <div className="form-group">
                                <label className="control-label" htmlFor="ShortDescription">Краткое описание</label>
                                <input className="form-control" ref={shortDescription} type="text" />
                            </div>

                            <div className="form-group">
                                <label className="control-label" htmlFor="OnDate">Дата записи</label>
                                <div>
                                    <DatePicker readOnly={isFormDisabled} className="form-control" selected={startDate} onChange={setDateAction} />
                                </div>
                            </div>

                            <div className="form-group">
                                <label className="control-label" htmlFor="tagSelector">Теги</label>
                                <TagSelector readonly={isFormDisabled} presettedTags={selectedTags} onChanged={setSelectedTags}></TagSelector>
                            </div>

                            <div className="form-group">
                                <label className="control-label" htmlFor="insideText">Внутренний текст</label>
                                <Wysiwyg readonly={isFormDisabled} initialValue={wysiwygValue} setValue={setWysiwygValue}></Wysiwyg>
                            </div>

                            <div className="form-group">
                                <label className="control-label" htmlFor="insideText">Настройки приватности</label>
                                <Selector values={protectionOptions} onChanged={setVisbilityOption}></Selector>
                            </div>

                            <div className="form-group">
                                <label className="control-label" htmlFor="codeText">Закодировать текст</label>
                                <input className="form-control" ref={protectWithPassBool} type="checkbox" id="codeText" name="codeText" />
                            </div>

                            <div className="form-group">
                                <label className="control-label" htmlFor="codePass">Пароль</label>
                                <input className="form-control" ref={passwordRef} type="password" id="codePass" name="codePass" />
                            </div>

                            <button className="btn" onClick={callCreateRecord} type="button" aria-expanded="false">
                                Создать запись
                        </button>
                        </fieldset>
                    </form>
                </Panel>
            </CSSTransition>
        </div >
    );
}

export default CreateNewRecord;
