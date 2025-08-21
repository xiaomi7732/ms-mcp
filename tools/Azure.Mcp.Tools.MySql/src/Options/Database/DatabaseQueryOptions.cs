// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using Azure.Mcp.Tools.MySql.Options;

namespace Azure.Mcp.Tools.MySql.Options.Database;

public class DatabaseQueryOptions : MySqlDatabaseOptions
{
    [JsonPropertyName(MySqlOptionDefinitions.QueryText)]
    public string? Query { get; set; }
}
