import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';
import { Observable } from 'rxjs/Observable';

import { ODataGetOperation } from './ODataGetOperation';
import { ODataServiceAbstract } from './ODataServiceAbstract';
import { ODataQuery } from './ODataQuery';
import { ODataQueryResult } from './ODataQueryResult';
import { ODataContext } from './ODataContext';
import { ODataError } from './ODataError';

// created by FuryTech.ODataTypeScriptGenerator

export abstract class AngularODataServiceBase<T> extends ODataServiceAbstract<T> {

  private requestOptions = {
    headers: new HttpHeaders({
      ['withCredentials']: 'true'
    })
  };

  protected abstract entitySetUrlSegment: string;

  protected get entitySetUrl(): string {
    return ODataContext.ODataRootPath + this.entitySetUrlSegment;
  }

  protected abstract http: HttpClient;
  constructor() {
    super();
  }

  public Get(id: any): ODataGetOperation<T> {
    const idSegment = this.getEntityUriSegment(id);
    return new ODataGetOperation<T>(idSegment, async (queryString: string) => {
      return this.http.get(this.entitySetUrl + idSegment + queryString, {
        withCredentials: true,
      }).map(a => {
        return a as T;
      }).toPromise();
    });
  }

  public Direct<T>(query: string): Observable<T> {
    return this.http.get(this.entitySetUrl + query, { withCredentials: true, })
      .map(a => a as T);
  }

  private extractResponse(res: Response) {
    if (res.status < 200 || res.status >= 300) {
      throw new ODataError(res.status, res.json());
    }
    const body: any = res.json();
    const entity: T = body;
    return entity || null;
  }

  public async Post(entity: T): Promise<T> {
    return this.http.post(this.entitySetUrl, entity, this.requestOptions)
      .map(this.extractResponse)
      .toPromise();
  }

  public async Patch(id: any, entity: T): Promise<any> {
    return this.http
      .patch(this.entitySetUrl + this.getEntityUriSegment(id), entity, this.requestOptions)
      .toPromise();
  }

  public async Put(id: any, entity: T): Promise<T> {
    return this.http
      .put(this.entitySetUrl + this.getEntityUriSegment(id), entity, this.requestOptions)
      .map(this.extractResponse)
      .toPromise();
  }

  public async Delete(id: any): Promise<any> {
    return this.http
      .delete(this.entitySetUrl + this.getEntityUriSegment(id), this.requestOptions)
      .toPromise();
  }

  protected async ExecCustomAction(actionName: string, entity: T, ...args: any[]): Promise<any> {
    return this.http
      .post(this.entitySetUrl + this.getEntityUriSegment(entity) + `/${actionName}`, null, this.requestOptions)
      .toPromise();
  }

  protected async ExecCustomCollectionAction(actionName: string, ...args: any[]): Promise<any> {
    return this.http
      .post(this.entitySetUrl + actionName, null, this.requestOptions)
      .toPromise();
  }

  protected async ExecCustomFunction(functionName: string, entity: T, ...args: any[]): Promise<any> {
    return this.http
      .get(this.entitySetUrl + this.getEntityUriSegment(entity) + `/${functionName}`, this.requestOptions)
      .toPromise();
  }

  protected async ExecCustomCollectionFunction(functionName: string, ...args: any[]): Promise<any> {
    return this.http
      .get(this.entitySetUrl + functionName, this.requestOptions)
      .toPromise();
  }

  Query(): ODataQuery<T> {
    const http = this.http;
    const entitySetUrl = this.entitySetUrl;

    const evaluateQuery = (queryString: string): Promise<ODataQueryResult<T>> => {
      const url = entitySetUrl + queryString;
      const subscription = http.get(url, {
        withCredentials: true
      }).map(a => {
        const result = new ODataQueryResult<T>();
        Object.assign(result, a);
        return result;
      });

      return subscription.toPromise();
    };

    return new ODataQuery(evaluateQuery);
  }
}
