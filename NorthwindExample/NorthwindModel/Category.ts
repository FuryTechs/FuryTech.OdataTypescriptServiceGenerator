// Created by FuryTech.ODataTypeScriptGenerator
import { Product } from './Product';

export class Category {

    /* Navigation properties */
    public Products: Product[];

    /* Properties */
    public CategoryID: number;
    public CategoryName: string;
    public Description: string;
    public Picture: string;

}