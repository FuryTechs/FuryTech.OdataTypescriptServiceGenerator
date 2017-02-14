// Created by FuryTech.ODataTypeScriptGenerator
import { Order } from './Order';
import { Territory } from './Territory';

export class Employee {

    /* Navigation properties */
    public Employees1: Employee[];
    public Employee1: Employee;
    public Orders: Order[];
    public Territories: Territory[];

    /* Properties */
    public EmployeeID: number;
    public LastName: string;
    public FirstName: string;
    public Title: string;
    public TitleOfCourtesy: string;
    public BirthDate: Date;
    public HireDate: Date;
    public Address: string;
    public City: string;
    public Region: string;
    public PostalCode: string;
    public Country: string;
    public HomePhone: string;
    public Extension: string;
    public Photo: string;
    public Notes: string;
    public ReportsTo: number;
    public PhotoPath: string;

}