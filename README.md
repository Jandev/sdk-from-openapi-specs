# Create an SDK from Open API specs

The goal of this project is to create decent SDKs, for multiple programming languages, based on provided Open API specs.

The languages first investigated will be, in order of priority:

1. C# (csharp)
2. Python

Additional languages will also be supported, but not have a priority at first.

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

You should be able to run the following command and get details on the generator.

```powershell
java -jar openapi-generator-cli.jar help
```

The full list of commands available and how to use can be found in the [Usage documentation of the OpenAPI Generator](https://openapi-generator.tech/docs/usage).

### Modifications to the original Open API specification files

The Open API specification files in this repository are downloaded from a preview version of [manufacturing data solutions in Microsoft Fabric](https://learn.microsoft.com/en-us/industry/manufacturing/manufacturing-data-solutions/overview-manufacturing-data-solutions).

These files contain a couple of errors causing the OpenAPI generator not being able to operate. The changes necessary to the files are described in this topic.

> [!WARNING]
> These changes will cause incompatibilities with the actual endpoints.

#### mds-080-preview.yaml

This file has the following modifications.

The following operation needs a change.

```yml
    delete:
      tags:
        - Admin
      summary: Delete existing DMM Data version
      description: "\nRequired roles: Admin"
      parameters:
        - name: api-version
          in: query
          description: The requested API version
          required: true
          schema:
            type: string
            default: 2024-09-30-preview
      requestBody:
        description: ''
        content:
          application/json-patch+json:
            schema:
              $ref: '#/components/schemas/DataVersionRequest'
          application/json:
            schema:
              $ref: '#/components/schemas/DataVersionRequest'
          text/json:
            schema:
              $ref: '#/components/schemas/DataVersionRequest'
          application/*+json:
            schema:
              $ref: '#/components/schemas/DataVersionRequest'
        required: true
      responses:
        '202':
          description: Accepted
        '400':
          description: Bad Request
          content:
            text/plain:
              schema:
                $ref: '#/components/schemas/ProblemDetails'
            application/json:
              schema:
                $ref: '#/components/schemas/ProblemDetails'
            text/json:
              schema:
                $ref: '#/components/schemas/ProblemDetails'
```

According to the HTTP standards, a `DELETE` endpoint should not have a request body. Therefore, this part has been reworked to the following:

```yml
    delete:
      tags:
        - Admin
      summary: Delete existing DMM Data version
      description: "\nRequired roles: Admin"
      parameters:
        - name: api-version
          in: query
          description: The requested API version
          required: true
          schema:
            type: string
            default: 2024-09-30-preview
        - name: versionId
          in: query
          description: The version to delete
          required: true
          schema:
            type: string
      responses:
        '202':
          description: Accepted
        '400':
          description: Bad Request
          content:
            text/plain:
              schema:
                $ref: '#/components/schemas/ProblemDetails'
            application/json:
              schema:
                $ref: '#/components/schemas/ProblemDetails'
            text/json:
              schema:
                $ref: '#/components/schemas/ProblemDetails'
```

The `/components/schemas/JToken` entity has a circular reference, causing an infinite loop which can cause a variety of problems.

```yml
    JToken:
      type: array
      items:
        $ref: '#/components/schemas/JToken'
```

This entity has now been changed to the following:

```yml
    JToken:
      type: array
      items:
        $ref: '#/components/schemas/ChildJToken'
    ChildJToken:
      type: string
```

## Sample usage

The first step of the process is to test if the generation of the SDK works, based on the provided Open API specification files.

### Validating the files

Validating the files before generating code from it can be done with the following commands.

```powershell
java -jar openapi-generator-cli.jar validate `
    -i ./src/openapi-specs/copilot-080-preview.yaml

java -jar openapi-generator-cli.jar validate `
    -i ./src/openapi-specs/mds-080-preview.yaml
```

### Create a simple SDK

To create a simple SDK based on the [copilot-080-preview.yaml](./src/openapi-specs/copilot-080-preview.yaml) specifications, you can use the following command.

```powershell
java -jar openapi-generator-cli.jar generate `
    -i ./src/openapi-specs/copilot-080-preview.yaml `
    -g csharp `
    -o ./src/generated/copilot/csharp
```

To create a simple SDK based on the [mds-080-preview.yaml](./src/openapi-specs/mds-080-preview.yaml) specifications, you can use the following command.

```powershell
java -jar openapi-generator-cli.jar generate `
    -i ./src/openapi-specs/mds-080-preview.yaml `
    -g csharp `
    -o ./src/generated/mds/csharp
```
