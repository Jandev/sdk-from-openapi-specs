// See https://aka.ms/new-console-template for more information
using Microsoft.Industry.Manufacturing.Copilot.Sdk.Api;
using Microsoft.Industry.Manufacturing.Copilot.Sdk.Client;
using Microsoft.Industry.Manufacturing.Copilot.Sdk.Model;

Console.WriteLine("Hello user! We're now going to invoke the Manufacturing Copilot endpoints using the generated SDK code.");

var gpt4oInstance = new
{
	BaseUrl = "https://[your-mds-instance].eastus2.cloudapp.azure.com",
	ApplicationIdUri = "api://[your-application-id]"
};

var instanceToUse = gpt4oInstance;

var configuration = new Configuration
{
	BasePath = instanceToUse.BaseUrl,
	MdsApplicationIdUri = instanceToUse.ApplicationIdUri,
	// Leave the below properties empty if you want to use a System Managed Identity or local identity. It will use the `DefaultAzureCredential` to authenticate.
	// Use these values if you want to use a User Managed Identity
	UserAssignedManagedIdentityClientId = string.Empty,
	// Use these values if you want to use a Service Principal to authorize
	OAuthClientId = string.Empty,
	OAuthClientSecret = string.Empty,
	OAuthScope = string.Empty
};

var queryApi = new CopilotV3QueryApi(configuration);
var request = new QueryRequestV3
{
	Ask = "Show me the Products that were produced on 3/30/2023 in BREADBLAST07?"
};
try
{
	var response = await queryApi.CopilotV3QueryPostAsync(
		ApiVersions.June2024Preview, 
		queryRequestV3: request);

	Console.WriteLine("Response received from the API:");
	Console.WriteLine(response.Result);
	Console.WriteLine("Summary:");
	Console.WriteLine(response.Summary);
}
catch(ApiException e)
{
	Console.WriteLine("An error occurred while invoking the API: " + e.Message);
}
catch (Exception e)
{
	Console.WriteLine("An error occurred: " + e.Message);
}
Console.ReadLine();
