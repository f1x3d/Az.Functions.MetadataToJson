using System.Text.Json;
using Az.Functions.MetadataToJson.Models;
using Microsoft.Build.Framework;

namespace Az.Functions.MetadataToJson;

public class FunctionsMetadataToJson : Microsoft.Build.Utilities.Task
{
    [Required]
    public string? OutputDirectory { get; set; }

    public override bool Execute()
    {
        var functionsMetadataPath = Path.Combine(OutputDirectory!, "functions.metadata");

        if (!AssertPathIsValid(OutputDirectory!, functionsMetadataPath))
            return false;

        var functionsMetadata = GetFunctionsMetadata(functionsMetadataPath);

        ProcessFunctionsMetadata(OutputDirectory!, functionsMetadata);

        return true;
    }

    private bool AssertPathIsValid(string directoryPath, string functionsMetadataPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Log.LogError("The parameter value provided is not a valid directory path.");
            return false;
        }

        if (!File.Exists(functionsMetadataPath))
        {
            Log.LogError("The directory does not contain a functions.metadata file.");
            return false;
        }

        return true;
    }

    private IEnumerable<FunctionMetadata> GetFunctionsMetadata(string functionsMetadataPath)
    {
        var functionsMetadataString = File.ReadAllText(functionsMetadataPath);
        return JsonSerializer.Deserialize<IEnumerable<FunctionMetadata>>(functionsMetadataString)!;
    }

    private void ProcessFunctionsMetadata(string directoryPath, IEnumerable<FunctionMetadata> functionsMetadata)
    {
        foreach (var functionMetadata in functionsMetadata)
        {
            var functionDirectoryPath = Path.Combine(directoryPath, functionMetadata.Name);
            var functionDirectory = Directory.CreateDirectory(functionDirectoryPath);
            var functionMetadataFilePath = Path.Combine(functionDirectory.FullName, "function.json");

            var functionMetadataString = JsonSerializer.Serialize(functionMetadata);
            File.WriteAllText(functionMetadataFilePath, functionMetadataString);
        }
    }
}
