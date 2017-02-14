// Created by FuryTech.ODataTypeScriptGenerator
import { Product } from './Product';

export class Supplier {

    /* Navigation properties */
    public Products: Product[];

    /* Properties */
    public SupplierID: number;
    public CompanyName: string;
    public ContactName: string;
    public ContactTitle: string;
    public Address: string;
    public City: string;
    public Region: string;
    public PostalCode: string;
    public Country: string;
    public Phone: string;
    public Fax: string;
    public HomePage: string;

}