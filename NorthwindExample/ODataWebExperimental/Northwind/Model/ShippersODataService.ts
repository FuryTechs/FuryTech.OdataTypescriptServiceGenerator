// created by FuryTech.ODataTypeScriptGenerator
import { Shipper } from './../../../NorthwindModel/Shipper';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class ShippersODataService extends AureliaOdataServiceBase<Shipper> {

    constructor() {
        super('Shippers');
    }
}
