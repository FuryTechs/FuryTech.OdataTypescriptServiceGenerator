import { ODataOperation } from './ODataOperation';

export abstract class ODataGetOperation<T> extends ODataOperation<T> {
    public abstract Exec(): Promise<T>;
}
