// created by FuryTech.ODataTypeScriptGenerator
$imports$

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AngularODataServiceBase } from '../AngularOdataServiceBase';

@Injectable()
export class $Name$ extends AngularODataServiceBase<$entityTypeName$> {
    public entitySetUrlSegment = '$entitySetUrl$';
$customActions$$customFunctions$
    constructor(protected http: HttpClient) {
        super();
    }
}
