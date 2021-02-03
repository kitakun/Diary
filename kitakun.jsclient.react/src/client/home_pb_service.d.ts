/* eslint-disable */
// package: home
// file: home.proto
declare const proto: any;
declare const COMPILED: any;

import * as home_pb from "./home_pb";
import { grpc } from "@improbable-eng/grpc-web";

type HomeFetchHomePreviewRecords = {
  readonly methodName: string;
  readonly service: typeof Home;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof home_pb.FetchHomePreviewRecordsRequest;
  readonly responseType: typeof home_pb.FetchHomePreviewRecordsResponse;
};

export class Home {
  static readonly serviceName: string;
  static readonly FetchHomePreviewRecords: HomeFetchHomePreviewRecords;
}

export type ServiceError = { message: string, code: number; metadata: grpc.Metadata }
export type Status = { details: string, code: number; metadata: grpc.Metadata }

interface UnaryResponse {
  cancel(): void;
}
interface ResponseStream<T> {
  cancel(): void;
  on(type: 'data', handler: (message: T) => void): ResponseStream<T>;
  on(type: 'end', handler: (status?: Status) => void): ResponseStream<T>;
  on(type: 'status', handler: (status: Status) => void): ResponseStream<T>;
}
interface RequestStream<T> {
  write(message: T): RequestStream<T>;
  end(): void;
  cancel(): void;
  on(type: 'end', handler: (status?: Status) => void): RequestStream<T>;
  on(type: 'status', handler: (status: Status) => void): RequestStream<T>;
}
interface BidirectionalStream<ReqT, ResT> {
  write(message: ReqT): BidirectionalStream<ReqT, ResT>;
  end(): void;
  cancel(): void;
  on(type: 'data', handler: (message: ResT) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'end', handler: (status?: Status) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'status', handler: (status: Status) => void): BidirectionalStream<ReqT, ResT>;
}

export class HomeClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  fetchHomePreviewRecords(
    requestMessage: home_pb.FetchHomePreviewRecordsRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError | null, responseMessage: home_pb.FetchHomePreviewRecordsResponse | null) => void
  ): UnaryResponse;
  fetchHomePreviewRecords(
    requestMessage: home_pb.FetchHomePreviewRecordsRequest,
    callback: (error: ServiceError | null, responseMessage: home_pb.FetchHomePreviewRecordsResponse | null) => void
  ): UnaryResponse;
}

