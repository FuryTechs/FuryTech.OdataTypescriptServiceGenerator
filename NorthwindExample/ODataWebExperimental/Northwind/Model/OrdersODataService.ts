// created by FuryTech.ODataTypeScriptGenerator
import { Order } from './../../../NorthwindModel/Order';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class OrdersODataService extends AureliaOdataServiceBase<Order> {

    constructor() {
        super('Orders');
    }
}
