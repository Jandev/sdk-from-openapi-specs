# Manufacturing SDK

The SDKs created over here can be used to integrate with the [Microsoft Cloud for Manufacturing platform](https://learn.microsoft.com/en-us/industry/manufacturing/overview).

## Modifications to the original Open API specification files

The Open API specification files in this repository are downloaded from a preview version of [manufacturing data solutions in Microsoft Fabric](https://learn.microsoft.com/en-us/industry/manufacturing/manufacturing-data-solutions/overview-manufacturing-data-solutions).

These files contain a couple of errors causing the OpenAPI generator not being able to operate. The changes necessary to the files are described in this topic.

> [!WARNING]
> These changes will cause incompatibilities with the actual endpoints.

### mds-080-preview.yaml

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

#### C# / csharp

To create a simple SDK based on the [copilot-080-preview.yaml](./src/openapi-specs/copilot-080-preview.yaml) specifications, you can use the following command.

```powershell
java -jar openapi-generator-cli.jar generate `
    -g csharp `
    -c ./src/openapi3/config-csharp-copilot.json
```

To create a simple SDK based on the [mds-080-preview.yaml](./src/openapi-specs/mds-080-preview.yaml) specifications, you can use the following command.

```powershell
java -jar openapi-generator-cli.jar generate `
    -g csharp `
    -c ./src/openapi3/config-csharp-mds.json
```

#### Python

To create a simple SDK based on the [copilot-080-preview.yaml](./src/openapi-specs/copilot-080-preview.yaml) specifications, you can use the following command.

```powershell
java -jar openapi-generator-cli.jar generate `
    -g python `
    -c ./src/openapi3/config-python-copilot.json
```

To create a simple SDK based on the [mds-080-preview.yaml](./src/openapi-specs/mds-080-preview.yaml) specifications, you can use the following command.

```powershell
java -jar openapi-generator-cli.jar generate `
    -g python `
    -c ./src/openapi3/config-python-mds.json
```

## Templates

The Open API Generator output can be customized using Templates.  
The default templates can be extracted with the following commands:

```powershell
# For C#
java -jar openapi-generator-cli.jar author template -g csharp -o ./src/openapi3/templates/ootb/csharp

# For Python
java -jar openapi-generator-cli.jar author template -g python -o ./src/openapi3/templates/ootb/python
```
