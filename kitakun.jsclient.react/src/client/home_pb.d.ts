import * as jspb from 'google-protobuf'

import * as google_protobuf_timestamp_pb from 'google-protobuf/google/protobuf/timestamp_pb';


export class FetchHomePreviewRecordsRequest extends jspb.Message {
  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): FetchHomePreviewRecordsRequest.AsObject;
  static toObject(includeInstance: boolean, msg: FetchHomePreviewRecordsRequest): FetchHomePreviewRecordsRequest.AsObject;
  static serializeBinaryToWriter(message: FetchHomePreviewRecordsRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): FetchHomePreviewRecordsRequest;
  static deserializeBinaryFromReader(message: FetchHomePreviewRecordsRequest, reader: jspb.BinaryReader): FetchHomePreviewRecordsRequest;
}

export namespace FetchHomePreviewRecordsRequest {
  export type AsObject = {
  }
}

export class HomePreveiwRecordItem extends jspb.Message {
  getId(): string;
  setId(value: string): HomePreveiwRecordItem;

  getSpaceid(): string;
  setSpaceid(value: string): HomePreveiwRecordItem;

  getTitle(): string;
  setTitle(value: string): HomePreveiwRecordItem;

  getDate(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setDate(value?: google_protobuf_timestamp_pb.Timestamp): HomePreveiwRecordItem;
  hasDate(): boolean;
  clearDate(): HomePreveiwRecordItem;

  getTagsList(): Array<string>;
  setTagsList(value: Array<string>): HomePreveiwRecordItem;
  clearTagsList(): HomePreveiwRecordItem;
  addTags(value: string, index?: number): HomePreveiwRecordItem;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): HomePreveiwRecordItem.AsObject;
  static toObject(includeInstance: boolean, msg: HomePreveiwRecordItem): HomePreveiwRecordItem.AsObject;
  static serializeBinaryToWriter(message: HomePreveiwRecordItem, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): HomePreveiwRecordItem;
  static deserializeBinaryFromReader(message: HomePreveiwRecordItem, reader: jspb.BinaryReader): HomePreveiwRecordItem;
}

export namespace HomePreveiwRecordItem {
  export type AsObject = {
    id: string,
    spaceid: string,
    title: string,
    date?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    tagsList: Array<string>,
  }
}

export class FetchHomePreviewRecordsResponse extends jspb.Message {
  getRecordsList(): Array<HomePreveiwRecordItem>;
  setRecordsList(value: Array<HomePreveiwRecordItem>): FetchHomePreviewRecordsResponse;
  clearRecordsList(): FetchHomePreviewRecordsResponse;
  addRecords(value?: HomePreveiwRecordItem, index?: number): HomePreveiwRecordItem;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): FetchHomePreviewRecordsResponse.AsObject;
  static toObject(includeInstance: boolean, msg: FetchHomePreviewRecordsResponse): FetchHomePreviewRecordsResponse.AsObject;
  static serializeBinaryToWriter(message: FetchHomePreviewRecordsResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): FetchHomePreviewRecordsResponse;
  static deserializeBinaryFromReader(message: FetchHomePreviewRecordsResponse, reader: jspb.BinaryReader): FetchHomePreviewRecordsResponse;
}

export namespace FetchHomePreviewRecordsResponse {
  export type AsObject = {
    recordsList: Array<HomePreveiwRecordItem.AsObject>,
  }
}

