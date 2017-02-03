// created by FuryTech.ODataTypeScriptGenerator
import { Territory } from './../../../NorthwindModel/Territory';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class TerritoriesODataService extends AureliaOdataServiceBase<Territory> {

    constructor() {
        super('Territories');
    }
}
