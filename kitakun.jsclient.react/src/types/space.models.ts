import { ISelectOption } from "library/common/Controls/Selector/Selector";

export interface ISpace {
    id: string;
    humanName: string;
    urlName: string;
    recordPreviews?: ISpaceRecordPreview[];
}

export interface ISpaceRecordPreview {
    id: string;
    spaceId: string;
    title: string;
    date: Date;
    tags: string[];
}

export interface ISpaceRecord {
    id?: string;
    tokenUrl: string;
    createdAt?: Date;
    shortDescription: string;
    markdownText: string;
    selectedTags: string[],
    visibilityOption: ISelectOption,
}

export interface ICreateSpaceRecord extends ISpaceRecord {
    protectedByPassword?: boolean;
    password?: string;
}