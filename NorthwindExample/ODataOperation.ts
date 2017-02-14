export abstract class ODataOperation<T> {
    protected _expand: string;
    protected _select: string;

    public Expand(...expand: string[]) {
        this._expand = this.parseStringOrStringArray(...expand);
        return this;
    }

    public Select(...select: string[]) {
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
