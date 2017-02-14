// created by FuryTech.ODataTypeScriptGenerator
import { Customer } from './../../../NorthwindModel/Customer';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class CustomersODataService extends AureliaOdataServiceBase<Customer> {

    constructor() {
        super('Customers');
    }
}
