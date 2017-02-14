// created by FuryTech.ODataTypeScriptGenerator
import { Region } from './../../../NorthwindModel/Region';
import { ODataContext } from './../../../ODataContext';


import { AureliaOdataServiceBase } from '../AureliaOdataServiceBase';

export class RegionsODataService extends AureliaOdataServiceBase<Region> {

    constructor() {
        super('Regions');
    }
}
