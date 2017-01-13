# FuryTech.OdataTypescriptServiceGenerator
This project is a small command line tool written in .NET, to help scaffolding TypeScript model classes and services from an OData Metadata XML. It has templates bundled for Angular(>2) and Aurelia.

##Usage

1. Download & Configure
  * edit settings in app.config (choose the templates between Aurelia / Angular)
  * edit the "metadataPath" setting to point to your endpoint (save your metadata.xml to a file if you have authentication issues)
  * set your "endpointName" variable
2. Build & Run the tool
3. Grab the generated TypeScript files
 * All files from the StaticContent directory will be copied to the output. Please delete the not necessary files (e.g. is you're using Aurelia, delete *AngularOdataServiceBase.ts* to avoid missing import errors during tsc)

##What works now
* EntityTypes to Typescript model classes
* ComplexTypes to Typescript model classes
* Primitive EDM types to Typescript types
* EnumTypes to Typescript Enums
* EntitySets to TypeScript services (for angular2, use *AngularEntitySetService.tst*, for Aurelia, use *AureliaEntitySetService.tst*)
 * Strongly typed CustomActions (for entity and for collection)
 * Strongly typed CustomFunction (for entity and for collection)
* ODataContext class generation to store some basic endpoint context data
