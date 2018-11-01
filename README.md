# SMART on FHIR demo implementation

## Introduction
This demo show cases Smart Health IT's Pediatric Growth Application, a web app using the SMART on FHIR specification to access data from the EHR

The demo consist of four components:
* FHIR Server hosted in Azure - https://smart-fhir-api.azurewebsites.net/
* OAuth Server hosted in Azure - https://smart-auth.azurewebsites.net/.well-known/openid-configuration
* SMART on FHIR compliant web application - http://examples.smarthealthit.org/growth-chart-app/
* EHR Desktop application - which will be run by you on your desktop

## How to use the demo:

Note: Step 4 and step 7 might take some time to execute if the web apps are cold started

1. Set both Solution Platform and Platform Target in Visual Studio to x86 for the EHRApp project. This step is required because we use the CefSharp browser component.
2. Start EHRApp
3. Select File -> Open -> Patient
4. In the Find who text field enter: susan
5. Select Susan Clark with Patient Id smart-1482713, click the button Open
6. Susan Clark's Patient form is open and the EHR context is Susan Clark
7. Select Tools -> Pediatric Growth Application.
8. If a consent screen pops up, let the defaults be and press the button "Yes, allow". This will authorize the Pediatric Growth Application to access "your" data
9. You have now started a web app which is running in the context of your EHR authorized by your EHR system to use your data
