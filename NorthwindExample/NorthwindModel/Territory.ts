// Created by FuryTech.ODataTypeScriptGenerator
import { Region } from './Region';
import { Employee } from './Employee';

export class Territory {

    /* Navigation properties */
    public Region: Region;
    public Employees: Employee[];

    /* Properties */
    public TerritoryID: string;
    public TerritoryDescription: string;
    public RegionID: number;

}