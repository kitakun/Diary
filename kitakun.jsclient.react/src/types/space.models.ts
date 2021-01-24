export interface ISpaceRecordPreview {
    id: string;
    title: string;
    date: Date;
    tags: string[];
}

export interface ISpaceRecord {
    id: string;
    tokenUrl: string;
    createdAt: Date;
    shortDescription: string;
    markdownText: string;
}
