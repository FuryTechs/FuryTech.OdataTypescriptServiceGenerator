import * as https from 'https';
import { ODataQueryResult } from './ODataQueryResult';
import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { ODataGetOperation } from './ODataGetOperation';
import { ODataServiceAbstract } from './ODataServiceAbstract';
import { ODataQuery } from './ODataQuery';

// created by FuryTech.ODataTypeScriptGenerator

class ODataError implements Error{
    public Status: number;
    public Response: Response;

    public name: string;
    public message: string;

    constructor(status: number, response: any) {
        this.name = 'OData Request Error';
        this.message = response;
    }
}

export abstract class AngularODataServiceBase<T> extends ODataServiceAbstract<T> {

    protected abstract http: Http;
    constructor() {
        super();
     }

    private async evaluateGetOperation(queryString: string): Promise<T>{
        return this.http.get(this.entitySetUrl + queryString).map(a => {
            return a.json() as T;
        }).toPromise();
    }

    public Get(id: any): ODataGetOperation<T>{
        let idSegment = this.getEntityUriSegment(id);
        return new ODataGetOperation<T>(idSegment, this.evaluateGetOperation);
    };

    private extractResponse(res: Response) {
        if (res.status < 200 || res.status >= 300) {
            throw new ODataError(res.status, res.json());
        }
        let body: any = res.json();
        let entity: T = body;
        return entity || null;
    }
    public async Post(entity: T): Promise<T>{
        return this.http.post(this.entitySetUrl, entity)
            .map(this.extractResponse)
            .toPromise();
    }

    public async Patch(id: any, entity: T): Promise<any>{
        let body = JSON.stringify(entity);

    }

    public async Put(id: any, entity: T): Promise<T>{
        return null;
    }

    public async Delete(id: any): Promise<any>{
        return null;
    }


    private async evaluateQuery(queryString:string): Promise<ODataQueryResult<T>>{

        //ToDo: FIXME!!!! :(

        let url = this.entitySetUrl + queryString;
        let http = this.http;
        return http.get(url).map(a => {
            return a.json() as ODataQueryResult<T>;
        }).toPromise();
    }

    Query(): ODataQuery<T>{
        return new ODataQuery(this.evaluateQuery);
    }

}
