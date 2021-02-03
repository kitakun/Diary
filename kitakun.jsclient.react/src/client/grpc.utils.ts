export type ServiceError = { message: string, code: number; metadata: any }

interface IGrpcCallback<TModelResp> {
    (error: ServiceError | null, responseMessage: TModelResp | null): void;
}

interface IGrpReqMethod<TReq, TResp, TMResp> {
    (requestMessage: TReq, metadata: any, callback: IGrpcCallback<TMResp>): TResp;
}

/* saddly, typescript can't resolse TMResp */
export function toPromise<TMResp, TReq, TResp>(method: IGrpReqMethod<TReq, TResp, TMResp>, req: TReq): Promise<TMResp | null> {
    return new Promise<TMResp | null>((res, rej) => {
        method(req, {}, (error: ServiceError | null, responseMessage: TMResp | null) => {
            if (error) {
                rej(error);
            } else {
                res(responseMessage);
            }
        });
    });
}

export function toPromiseAny<TMResp>(method: IGrpReqMethod<any, any, TMResp>, req: any): Promise<TMResp | null> {
    return new Promise<TMResp | null>((res, rej) => {
        method(req, {}, (error: ServiceError | null, responseMessage: TMResp | null) => {
            if (error) {
                rej(error);
            } else {
                res(responseMessage);
            }
        });
    });
}

