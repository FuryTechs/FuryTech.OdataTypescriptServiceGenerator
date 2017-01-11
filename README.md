# FuryTech.OdataTypescriptServiceGenerator
This project is a small command line .NET application to help scaffolding an Angular2 module for a specified OData Endpoint from metadata.xml

##Usage

1. Download & Configure
  * edit settings in app.config
  * edit the "metadataPath" setting to point to your endpoint (save your metadata.xml to a file if you have authentication issues)
  * set your "endpointName" variable
2. Build & Run the tool
3. Grab the generated Angular module from your output folder

##What works now
* EntityTypes to Typescript model classes
* ComplexTypes to Typescript model classes
* Primitive EDM types to Typescript types
* EnumTypes to Typescript Enums
* EntitySets to Angular2 Service
 * Strongly typed CustomActions (for entity and for collection)
 * Strongly typed CustomFunction (for entity and for collection)
* ODataContext class generation to store some basic endpoint context data
