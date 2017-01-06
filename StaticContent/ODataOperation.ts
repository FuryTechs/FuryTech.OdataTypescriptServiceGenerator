export abstract class ODataOperation<T> {
    protected _expand: string;
    protected _select: string;

    private collectionUrl: string;
    protected get CollectionUrl(): string{
        return this.collectionUrl;
    }


    public Expand(expand: string | string[]) {
        this._expand = this.parseStringOrStringArray(expand);
        return this;
    }

    public Select(select: string | string[]) {
        this._select = this.parseStringOrStringArray(select);
        return this;
    }

    protected parseStringOrStringArray(input: string | string[]): string {
        if (input instanceof Array) {
            return input.join(',');
        }

        return input as string;
    }

    protected getEntityUriSegment(entityKey: string): string {
        if (!/^[0-9]*$/.test(entityKey)) {
            return `('${entityKey}')`;
        }

        return  `(${entityKey})`;
    }

    abstract Exec(...args): Promise<any>;

    constructor(collectionUrl: string) {
        this.collectionUrl = collectionUrl;
    }
}