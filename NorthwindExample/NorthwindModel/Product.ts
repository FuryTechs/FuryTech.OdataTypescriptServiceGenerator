// Created by FuryTech.ODataTypeScriptGenerator
import { Category } from './Category';
import { Order_Detail } from './Order_Detail';
import { Supplier } from './Supplier';

export class Product {

    /* Navigation properties */
    public Category: Category;
    public Order_Details: Order_Detail[];
    public Supplier: Supplier;

    /* Properties */
    public ProductID: number;
    public ProductName: string;
    public SupplierID: number;
    public CategoryID: number;
    public QuantityPerUnit: string;
    public UnitPrice: number;
    public UnitsInStock: number;
    public UnitsOnOrder: number;
    public ReorderLevel: number;
    public Discontinued: boolean;

}