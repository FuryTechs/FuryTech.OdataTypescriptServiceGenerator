// created by FuryTech.ODataTypeScriptGenerator
import { Invoice } from './../../../NorthwindModel/Invoice';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class InvoicesODataService extends AureliaOdataServiceBase<Invoice> {

    constructor() {
        super('Invoices');
    }
}
