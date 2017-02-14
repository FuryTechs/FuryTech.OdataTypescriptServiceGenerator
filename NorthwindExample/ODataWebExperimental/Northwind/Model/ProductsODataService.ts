// created by FuryTech.ODataTypeScriptGenerator
import { Product } from './../../../NorthwindModel/Product';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class ProductsODataService extends AureliaOdataServiceBase<Product> {

    constructor() {
        super('Products');
    }
}
