// created by FuryTech.ODataTypeScriptGenerator
import { Sales_by_Category } from './../../../NorthwindModel/Sales_by_Category';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class Sales_by_CategoriesODataService extends AureliaOdataServiceBase<Sales_by_Category> {

    constructor() {
        super('Sales_by_Categories');
    }
}
