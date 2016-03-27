var jsonfile = require('jsonfile');

// Read in the file to be patched
var file = process.argv[2]; // e.g. '../src/MyProject/project.json'
if (!file)
    console.log("No filename provided");
console.log("File: " + file);

// Read in the build number (this might be provided by the CI server)
var buildNumber = process.argv[3]; // e.g. '42'
if (!buildNumber)
    console.log("No build number provided");

// Read in the optional release
var release = process.argv[4]; // e.g. 'alpha'
if (release)
    buildNumber = buildNumber + "-" + release;
console.log("Build: " + buildNumber);

jsonfile.readFile(file, function (err, project) {
    // Patch the project.version to replace the last integer component with
    // the build version that was supplied as an argument
    var findPoint       = project.version.lastIndexOf(".");
    var basePackageVer  = project.version.substring(0, findPoint + 1);
    var productVersion      = basePackageVer + buildNumber;
    console.log("Version: " + productVersion);


    project.version = productVersion;
    jsonfile.writeFile(file, project, {spaces: 2}, function(err) {
        console.error(err);
    });
})
