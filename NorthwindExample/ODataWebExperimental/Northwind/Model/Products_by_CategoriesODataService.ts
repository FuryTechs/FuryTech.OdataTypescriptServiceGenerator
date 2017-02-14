// created by FuryTech.ODataTypeScriptGenerator
import { Products_by_Category } from './../../../NorthwindModel/Products_by_Category';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class Products_by_CategoriesODataService extends AureliaOdataServiceBase<Products_by_Category> {

    constructor() {
        super('Products_by_Categories');
    }
}
