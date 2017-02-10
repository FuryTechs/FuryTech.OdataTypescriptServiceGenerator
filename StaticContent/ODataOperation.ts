export abstract class ODataOperation<T> {
    protected _expand: string;
    protected _select: string;

    public Expand<K extends keyof T>(...expand: K[]) {
        this._expand = this.parseStringOrStringArray(...expand);
        return this;
    }

    public Select<K extends keyof T>(...select: K[]) {
        this._select = this.parseStringOrStringArray(...select);
        return this;
    }

    protected parseStringOrStringArray(...input: string[]): string {
        if (input instanceof Array) {
            return input.join(',');
        }

        return input as string;
    }

    abstract Exec(...args): Promise<any>;
}
