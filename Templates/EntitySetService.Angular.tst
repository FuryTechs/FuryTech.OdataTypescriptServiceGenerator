// created by FuryTech.ODataTypeScriptGenerator
$imports$

import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { AngularODataServiceBase } from '../AngularOdataServiceBase';

@Injectable()
export class $Name$ extends AngularODataServiceBase<$entityTypeName$> {
    public entitySetUrl = '$entitySetUrl$';

    constructor(protected http: Http) {
        super();
    }
}
