# Create an SDK from Open API specs

The goal of this project is to create decent SDKs, for multiple programming languages, based on provided Open API specs.

The languages first investigated will be, in order of priority:

1. C# (csharp)
2. Python

Additional languages will also be supported, but not have a priority at first.

Please [refer to the documentation](./src/openapi3/README.md) on the generator for the specifics on how to build and generate the SDKs.

## Set up

This project builds on top of the official [OpenAPI Generator](https://openapi-generator.tech/), that also has a [GitHub repository](https://github.com/OpenAPITools/openapi-generator).

### Install the prerequisites

This tool relies on both Java and Maven installed on your system.  

#### Java

The following command  will install a Java version on your system, as provided in the [MS Learn docs](https://learn.microsoft.com/en-us/java/openjdk/install#install-on-windows-with-the-windows-package-manager-winget).

> [!NOTE]
> This command requires a Terminal session with Administrator privileges.

```powershell
winget install Microsoft.OpenJDK.21
```

In a new Terminal session, you should be able to run `java --version` to see which version got installed.

#### Maven

Maven can be [downloaded from the official Apache Maven Project page](https://maven.apache.org/download.cgi). The Binary zip archive is good enough for this use-case.

Extract the contents of the package to a folder of your choosing. adding it to my `C:\Tools\` folder.
Once extracted, be sure to add the `bin`-folder, containing the `mvn` file, to your `%PATH%`

In a new Terminal session, you should be able to run `mvn --version` to see which version got installed.

#### OpenAPI Generator

The easiest way to work with the generator is [by installing the JAR file](https://openapi-generator.tech/docs/installation#jar) in the root of this repository.

```powershell
Invoke-WebRequest -OutFile openapi-generator-cli.jar https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/7.9.0/openapi-generator-cli-7.9.0.jar
```

You should be able to run the following command and get details on the generator.

```powershell
java -jar openapi-generator-cli.jar help
```

The full list of commands available and how to use can be found in the [Usage documentation of the OpenAPI Generator](https://openapi-generator.tech/docs/usage).
