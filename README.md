# FuryTech.OdataTypescriptServiceGenerator
This project is a small command line .NET application to help scaffolding an Angular2 module for a specified OData Endpoint from metadata.xml

##Usage

1. download
  * edit settings in app.config
  * edit the "metadataPath" setting to point to your endpoint
  * set your "endpointName" variable
2. Build & Run the tool
3. Grab the generated Angular module from your output folder

##What works now
* EntityTypes to Typescript classes
* EnumTypes to Typescript Enums
* EntitySets to Angular2 Service
* ODataContext class generation to store some basic endpoint context data

##What's in progress(or partially done... or planned:))
* Custom Action & Function generation for entities and collection into the generated Angular2 servie
* TypeScript class generation for ComplexTypes

