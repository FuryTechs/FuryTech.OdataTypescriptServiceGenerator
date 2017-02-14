
type FilterArgs<T, K> = [K, string];
type FilterSegment<T> = ODataFilterExpression<T> | ODataFilterConnection<T>;

export class ODataFilterExpression<T>{

    private value: string = "";

    constructor(public filterBuilderRef: ODataFilterBuilder<T>) { }

    private Finialize() {
        this.filterBuilderRef.filterSegments.push(this);
        return new ODataFilterConnection<T>(this.filterBuilderRef);
    }

    public Equals<K extends keyof T>(field: K, value: string) {
        this.value = `${field} eq '${value}'`;
        return this.Finialize();
    }

    public NotEquals<K extends keyof T>(field: K, value: string) {
        this.value = `${field} ne '${value}'`;
        return this.Finialize();
    }

    public GreaterThan<K extends keyof T>(field: K, value: string) {
        this.value = `${field} gt '${value}'`;
        return this.Finialize();
    }

    public GreaterThanOrEquals<K extends keyof T>(field: K, value: string) {
        this.value = `${field} ge '${value}'`;
        return this.Finialize();
    }

    public LessThan<K extends keyof T>(field: K, value: string) {
        this.value = `${field} lt '${value}'`;
        return this.Finialize();
    }


    public LessThanOrEquals<K extends keyof T>(field: K, value: string) {
        this.value = `${field} le '${value}'`;
        return this.Finialize();
    }

    public Has<K extends keyof T>(field: K, value: string) {
        this.value = `${field} has '${value}'`;
        return this.Finialize();
    }

    public Not<K extends keyof T>(build: (b: ODataFilterExpression<T>) => void){
        let builder = ODataFilterBuilder.Create<T>();
        build(ODataFilterBuilder.Create<T>());
        this.value = `not (${builder.toString()})`;
        return this.Finialize();
    }

    public BuildFilter(build: (b: ODataFilterExpression<T>) => void) {
        let builder = ODataFilterBuilder.Create<T>();
        build(ODataFilterBuilder.Create<T>());
        this.value = `(${builder.toString()})`;
        return this.Finialize();
    }

    public toString(): string {
        return this.value;
    }

}

export class ODataFilterConnection<T>{

    private type: string;
    constructor(public filterBuilderRef: ODataFilterBuilder<T>) { }

    public get And() {
        this.type = "and";
        this.filterBuilderRef.filterSegments.push(this);
        return new ODataFilterExpression<T>(this.filterBuilderRef);
    }

    public get Or() {
        this.type = "or";
        this.filterBuilderRef.filterSegments.push(this);
        return new ODataFilterExpression<T>(this.filterBuilderRef);
    }

    public toString() {
        return this.type;
    }
}

export class ODataFilterBuilder<T>{

    public filterSegments: FilterSegment<T>[] = [];


    public static Create<T>(): ODataFilterExpression<T> {
        let builder = new ODataFilterBuilder();
        let firstSegment = new ODataFilterExpression(builder);
        return firstSegment;
    }


    public toString(): string {
        return this.filterSegments.map(s => s.toString()).join(' ');
    }
}