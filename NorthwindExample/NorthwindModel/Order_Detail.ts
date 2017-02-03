// Created by FuryTech.ODataTypeScriptGenerator
import { Order } from './Order';
import { Product } from './Product';

export class Order_Detail {

    /* Navigation properties */
    public Order: Order;
    public Product: Product;

    /* Properties */
    public OrderID: number;
    public ProductID: number;
    public UnitPrice: number;
    public Quantity: number;
    public Discount: Single;

}