// created by FuryTech.ODataTypeScriptGenerator
import { Supplier } from './../../../NorthwindModel/Supplier';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class SuppliersODataService extends AureliaOdataServiceBase<Supplier> {

    constructor() {
        super('Suppliers');
    }
}
