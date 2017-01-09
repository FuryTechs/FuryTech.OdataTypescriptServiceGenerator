import * as https from 'https';
import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { ODataGetOperation } from './ODataGetOperation';
import { ODataServiceAbstract } from './ODataServiceAbstract';
import { ODataQuery } from './ODataQuery';
import { ODataQueryResult } from './ODataQueryResult';
import { ODataContext } from './ODataContext';

// created by FuryTech.ODataTypeScriptGenerator

class ODataError implements Error {
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

    private async evaluateGetOperation(queryString: string): Promise<T> {
        return this.http.get(this.entitySetUrl + queryString).map(a => {
            return a.json() as T;
        }).toPromise();
    }

    public Get(id: any): ODataGetOperation<T> {
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
    public async Post(entity: T): Promise<T> {
        return this.http.post(this.entitySetUrl, entity)
            .map(this.extractResponse)
            .toPromise();
    }

    public async Patch(id: any, entity: T): Promise<any> {
        let body = JSON.stringify(entity);

    }

    public async Put(id: any, entity: T): Promise<T> {
        return null;
    }

    public async Delete(id: any): Promise<any> {
        return null;
    }


    Query(): ODataQuery<T> {

        let http = this.http;
        let entitySetUrl = ODataContext.ODataRootPath + this.entitySetUrl;

        let evaluateQuery = (queryString: string): Promise<ODataQueryResult<T>> => {
            let url = entitySetUrl + queryString;
            let subscription = http.get(url).map(a => {
                return a.json() as ODataQueryResult<T>;
            });

            subscription.subscribe(a => {

            });

            return subscription.toPromise();
        };

        return new ODataQuery(evaluateQuery);
    }

}
