/* eslint-disable */
declare const proto: any;
declare const COMPILED: any;
// package: home
// file: home.proto

import * as jspb from "google-protobuf";
import * as google_protobuf_timestamp_pb from "google-protobuf/google/protobuf/timestamp_pb";

export class FetchHomePreviewRecordsRequest extends jspb.Message {
  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): FetchHomePreviewRecordsRequest.AsObject;
  static toObject(includeInstance: boolean, msg: FetchHomePreviewRecordsRequest): FetchHomePreviewRecordsRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
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
  setId(value: string): void;

  getSpaceid(): string;
  setSpaceid(value: string): void;

  getTitle(): string;
  setTitle(value: string): void;

  hasDate(): boolean;
  clearDate(): void;
  getDate(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setDate(value?: google_protobuf_timestamp_pb.Timestamp): void;

  clearTagsList(): void;
  getTagsList(): Array<string>;
  setTagsList(value: Array<string>): void;
  addTags(value: string, index?: number): string;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): HomePreveiwRecordItem.AsObject;
  static toObject(includeInstance: boolean, msg: HomePreveiwRecordItem): HomePreveiwRecordItem.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
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
  clearRecordsList(): void;
  getRecordsList(): Array<HomePreveiwRecordItem>;
  setRecordsList(value: Array<HomePreveiwRecordItem>): void;
  addRecords(value?: HomePreveiwRecordItem, index?: number): HomePreveiwRecordItem;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): FetchHomePreviewRecordsResponse.AsObject;
  static toObject(includeInstance: boolean, msg: FetchHomePreviewRecordsResponse): FetchHomePreviewRecordsResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: FetchHomePreviewRecordsResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): FetchHomePreviewRecordsResponse;
  static deserializeBinaryFromReader(message: FetchHomePreviewRecordsResponse, reader: jspb.BinaryReader): FetchHomePreviewRecordsResponse;
}

export namespace FetchHomePreviewRecordsResponse {
  export type AsObject = {
    recordsList: Array<HomePreveiwRecordItem.AsObject>,
  }
}

