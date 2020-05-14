## weather-api-org-wide-demo

This is sample to demonstrate the Custom Web API usage.

### Instruction
* Run ```gulp deploy-solution``` to deploy the solution to App Catalog and install it on a specific site collection
* Run ```gulp remove-solution``` to remove the solution from App Catalog and a specific site collection
* Run the following command to bundle, package and deploy the SPFx solution

```bash
gulp bundle --ship && gulp package-solution --ship && gulp deploy-solution
```
### Building the code

```bash
git clone the repo
npm i
npm i -g gulp
gulp
```

This package produces the following:

* lib/* - intermediate-stage commonjs build artifacts
* dist/* - the bundled script, along with other resources
* deploy/* - all resources which should be uploaded to a CDN.

### Build options

gulp clean - TODO
gulp test - TODO
gulp serve - TODO
gulp bundle - TODO
gulp package-solution - TODO
