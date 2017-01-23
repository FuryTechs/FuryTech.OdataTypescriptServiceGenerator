import { HttpClient, HttpResponseMessage } from 'aurelia-http-client';

import { ODataServiceAbstract } from './ODataServiceAbstract';
import { ODataContext } from './ODataContext';
import { ODataGetOperation } from './ODataGetOperation';
import { ODataError } from './ODataError';
import { ODataQuery } from './ODataQuery';
import { ODataQueryResult } from './ODataQueryResult';

export abstract class AureliaOdataServiceBase<T> extends ODataServiceAbstract<T>{

    private http: HttpClient;

    protected get entitySetUrl(): string {
        return ODataContext.ODataRootPath + this.entitySetUrlSegment + '/';
    }

    constructor(protected entitySetUrlSegment: string) {
        super();
        this.http = new HttpClient().configure(c => {
            c.withBaseUrl(this.entitySetUrl),
                c.withCredentials(true)
        });
    }

    private async evaluateGetOperation(queryString: string): Promise<T> {
        var result = await this.http.get(queryString);
        return result.content;
    }

    public Get(id: any): ODataGetOperation<T> {
        let idSegment = this.getEntityUriSegment(id);
        return new ODataGetOperation<T>(idSegment, async (queryString: string) => {
            var result = await this.http.get(idSegment + queryString);
            return result.content;
        });
    };

    private extractResponse(res: HttpResponseMessage) {
        if (res.statusCode < 200 || res.statusCode >= 300) {
            throw new ODataError(res.statusCode, res.content);
        }
        let entity: T = res.content;
        return entity || null;
    }

    public async Post(entity: T): Promise<T> {
        var result = await this.http.post(this.entitySetUrl, entity);
        return result.content;
    }

    public async Patch(id: any, entity: T): Promise<any> {
        var result = await this.http.patch(this.getEntityUriSegment(id), entity);
        return result.content;
    }

    public async Put(id: any, entity: T): Promise<T> {
        var result = await this.http.put(this.getEntityUriSegment(id), entity);
        return result.content;
    }

    public async Delete(id: any): Promise<any> {
        var result = await this.http.delete(this.getEntityUriSegment(id));
        return result.content;
    }

    protected async ExecCustomAction(actionName: string, entity: T, ...args: any[]): Promise<any> {
        var result = await this.http.post(this.getEntityUriSegment(entity) + `/${actionName}`, null);
        return result.content;
    }

    protected async ExecCustomCollectionAction(actionName: string, ...args: any[]): Promise<any> {
        var result = await this.http.post(actionName, null);
        return result.content;
    }

    protected async ExecCustomFunction(fucntionName: string, entity: T, ...args: any[]): Promise<any> {
        let result = await this.http.get(this.getEntityUriSegment(entity) + `/${fucntionName}`);
        return result.content;
    }

    protected async ExecCustomCollectionFunction(fucntionName: string, ...args: any[]): Promise<any> {
        let result = await this.http.get(fucntionName);
        return result.content;
    }


    Query(): ODataQuery<T> {

        let http = this.http;
        let entitySetUrl = this.entitySetUrl;

        let evaluateQuery = async (queryString: string): Promise<ODataQueryResult<T>> => {
            let url = this.entitySetUrl + queryString;
            let response = await http.get(url);
            return response.content;
        };

        return new ODataQuery(evaluateQuery);
    }

}