import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { ODataGetOperation } from './ODataGetOperation';
import { ODataServiceAbstract } from './ODataServiceAbstract';
import { ODataQuery } from './ODataQuery';

// created by FuryTech.ODataTypeScriptGenerator

class AngularGetOperation<T> extends ODataGetOperation<T> {
    public Exec(): Promise<T>{
        return null;
    }
}

@Injectable()
export abstract class AngularODataServiceBase<T> extends ODataServiceAbstract<T> {
    constructor(private http: Http) {
        super();
     }

    public Get(id: any): ODataGetOperation<T>{
        return new AngularGetOperation<T>(this.entitySetUrl);
    };

    public async Post(entity: T): Promise<T>{
        return null;
    }

    public async Patch(id: any, entity: T): Promise<T>{
        return null;
    }

    public async Put(id: any, entity: T): Promise<T>{
        return null;
    }

    public async Delete(id: any): Promise<any>{
        return null;
    }

    Query(): ODataQuery<T>{
        return null;
    }

}
