export class ODataQueryResult<T>{
    public '@odata.context': string;
    public '@odata.count': number;

    public get Context(): string {
        return this['@odata.context'];
    }

    public get Count(): number {
        return this['@odata.count'];
    }

    public value: T[];
}
