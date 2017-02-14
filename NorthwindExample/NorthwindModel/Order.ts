// Created by FuryTech.ODataTypeScriptGenerator
import { Customer } from './Customer';
import { Employee } from './Employee';
import { Order_Detail } from './Order_Detail';
import { Shipper } from './Shipper';

export class Order {

    /* Navigation properties */
    public Customer: Customer;
    public Employee: Employee;
    public Order_Details: Order_Detail[];
    public Shipper: Shipper;

    /* Properties */
    public OrderID: number;
    public CustomerID: string;
    public EmployeeID: number;
    public OrderDate: Date;
    public RequiredDate: Date;
    public ShippedDate: Date;
    public ShipVia: number;
    public Freight: number;
    public ShipName: string;
    public ShipAddress: string;
    public ShipCity: string;
    public ShipRegion: string;
    public ShipPostalCode: string;
    public ShipCountry: string;

}