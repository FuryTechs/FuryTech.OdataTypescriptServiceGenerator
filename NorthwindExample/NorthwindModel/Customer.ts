// Created by FuryTech.ODataTypeScriptGenerator
import { Order } from './Order';
import { CustomerDemographic } from './CustomerDemographic';

export class Customer {

    /* Navigation properties */
    public Orders: Order[];
    public CustomerDemographics: CustomerDemographic[];

    /* Properties */
    public CustomerID: string;
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

}