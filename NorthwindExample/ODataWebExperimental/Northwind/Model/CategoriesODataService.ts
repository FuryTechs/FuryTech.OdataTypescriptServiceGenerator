// created by FuryTech.ODataTypeScriptGenerator
import { Category } from './../../../NorthwindModel/Category';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class CategoriesODataService extends AureliaOdataServiceBase<Category> {

    constructor() {
        super('Categories');
    }
}
