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

Install JDK 21 on your platform:

Windows (as documented in [MS Learn](https://learn.microsoft.com/en-us/java/openjdk/install#install-on-windows-with-the-windows-package-manager-winget)):

```powershell
winget install Microsoft.OpenJDK.21
```

macOS (Homebrew):

```bash
brew install openjdk@21
```

Linux:

```bash
# Debian/Ubuntu
sudo apt update && sudo apt install -y openjdk-21-jdk

# Fedora/RHEL
sudo dnf install -y java-21-openjdk-devel
```

> [!TIP]
> If you have multiple Java versions installed, set `JAVA_HOME` to JDK 21 before running Maven.

Verify the installation:

```bash
java --version
```

#### Maven

Install Maven on your platform:

Windows:

```powershell
winget install Apache.Maven
```

macOS (Homebrew):

```bash
brew install maven
```

Linux:

```bash
# Debian/Ubuntu
sudo apt install -y maven

# Fedora/RHEL
sudo dnf install -y maven
```

Verify the installation:

```bash
mvn --version
```

#### OpenAPI Generator

The easiest way to work with the generator is [by installing the JAR file](https://openapi-generator.tech/docs/installation#jar) in the root of this repository.

Windows:

```powershell
Invoke-WebRequest -OutFile openapi-generator-cli.jar https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/7.9.0/openapi-generator-cli-7.9.0.jar
```

macOS / Linux:

```bash
curl -fL "https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/7.9.0/openapi-generator-cli-7.9.0.jar" -o "openapi-generator-cli.jar"
```

You should be able to run the following command and get details on the generator.

```bash
java -jar openapi-generator-cli.jar help
```

The full list of commands available and how to use can be found in the [Usage documentation of the OpenAPI Generator](https://openapi-generator.tech/docs/usage).
