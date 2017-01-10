import { RequestArgs } from '@angular/http/src/interfaces';
import { ODataQuery } from './ODataQuery';
import {ODataGetOperation} from './ODataGetOperation';

export abstract class ODataServiceAbstract<T> {

    protected abstract entitySetUrl: string;

    public async abstract Post(entity: T): Promise<T>;

    public async abstract Patch(id: any, entity: T): Promise<T>;

    public async abstract Put(id: any, entity: T): Promise<T>;

    public async abstract Delete(id: any): Promise<any>;

    protected abstract ExecCustomAction(id: any, ...args: any[]): Promise<any>;
    protected abstract ExecCustomCollectionAction(...args: any[]): Promise<any>;
    protected abstract ExecCustomFunction(id: any, ...args: any[]): Promise<any>;
    protected abstract ExecCustomCollectionFunction(...args: any[]): Promise<any>;

    public abstract Get(id: any): ODataGetOperation<T>;

    public abstract Query(): ODataQuery<T>;

    protected getEntityUriSegment(entityKey: any): string {
        entityKey = entityKey.toString();
        if (!/^[0-9]*$/.test(entityKey)) {
            return `('${entityKey}')`;
        }

        return  `(${entityKey})`;
    }
}
