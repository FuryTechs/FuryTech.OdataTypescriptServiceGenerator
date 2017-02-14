// created by FuryTech.ODataTypeScriptGenerator
import { Employee } from './../../../NorthwindModel/Employee';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class EmployeesODataService extends AureliaOdataServiceBase<Employee> {

    constructor() {
        super('Employees');
    }
}
