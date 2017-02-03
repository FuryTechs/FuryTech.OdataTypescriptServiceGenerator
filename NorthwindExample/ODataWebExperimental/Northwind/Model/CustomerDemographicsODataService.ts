// created by FuryTech.ODataTypeScriptGenerator
import { CustomerDemographic } from './../../../NorthwindModel/CustomerDemographic';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class CustomerDemographicsODataService extends AureliaOdataServiceBase<CustomerDemographic> {

    constructor() {
        super('CustomerDemographics');
    }
}
