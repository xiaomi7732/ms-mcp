// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using Azure.Mcp.Tools.MySql.Options;

namespace Azure.Mcp.Tools.MySql.Options.Server;

public class ServerParamGetOptions : MySqlServerOptions
{
    [JsonPropertyName(MySqlOptionDefinitions.ParamName)]
    public string? Param { get; set; }
}
