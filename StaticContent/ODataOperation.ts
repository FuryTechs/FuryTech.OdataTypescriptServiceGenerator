export abstract class ODataOperation<T> {
    protected _expand!: string;
    protected _select!: string;

    /**
     * Sets the OData $expand= property
     * @param ...expand The field name(s) to be expanded
     */
    public Expand<K extends keyof T>(...expand: K[]) {
        this._expand = this.parseStringOrStringArray(...expand);
        return this;
    }

    /**
     * Sets the OData $select= property
     * @param ...select The field name(s) to be included in the OData Select
     */
    public Select<K extends keyof T>(...select: K[]) {
        this._select = this.parseStringOrStringArray(...select);
        return this;
    }

    /**
     * Executes the operation, should return an awaitable Promise
     */
    public abstract Exec(): Promise<any>;

    protected parseStringOrStringArray(...input: Array<string | number | symbol>): string {
        if (input instanceof Array) {
            return input.join(",");
        }

        return input as string;
    }
}
