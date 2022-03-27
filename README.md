# Az.Functions.MetadataToJson

## Summary

This package is a band-aid to fix an issue with Azure Functions Core Tools not recognizing functions metadata for projects with `dotnet-isolated` worker runtime when deploying to Kubernetes.

See the original issue here: https://github.com/Azure/azure-functions-core-tools/issues/2825

## Details

Apparently, the Kubernetes deployment part of the function tools was not updated to support `dotnet-isolated` worker runtime yet since it still looks for `function.json` files which [are not being generated with the new runtime](https://docs.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide#differences-with-net-class-library-functions).

That being said, the functions metadata still gets created in the `functions.metadata` file which is essentially an array of objects with similar to `function.json` structure.

This package adds a post-build action to your functions project that splits the new `functions.metadata` file into multiple `function.json`-s, each in its own folder, similarly to what the `in-process` SDK generates.

This helps Azure Functions Core Tools identify that metadata and properly generate Kubernetes deploy yaml files.

## Usage

Add a reference to this package in your Azure Functions project:

```xml
<ItemGroup>
  <PackageReference Include="Az.Functions.MetadataToJson" Version="1.0.0" />
</ItemGroup>
```

After that, the `func kubernetes deploy --dotnet-isolated` should work as expected.
