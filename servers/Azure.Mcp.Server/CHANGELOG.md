# CHANGELOG 📝

The Azure MCP Server updates automatically by default whenever a new release comes out 🚀. We ship updates twice a week on Tuesdays and Thursdays 😊

## 0.8.7 (Unreleased)

### Features Added

- Added support for sending email via Azure Communication Services with the command `azmcp_communication_email_send`. [[#690](https://github.com/microsoft/mcp/pull/690)]
- Added the following Event Hubs commands:
  - `azmcp_eventhubs_namespace_update`: Create or update an Event Hubs namespace.
  - `azmcp_eventhubs_namespace_delete`: Delete an existing Event Hubs namespace.
  - `azmcp_eventhubs_eventhub_update`: Create or update an Event Hub within a namespace.
  - `azmcp_eventhubs_eventhub_get`: Get details of an Event Hub within a namespace.
  - `azmcp_eventhubs_eventhub_delete`: Delete an Event Hub from a namespace.
  - `azmcp_eventhubs_eventhub_consumergroup_update`: Create or update a consumer group for an Event Hub.
  - `azmcp_eventhubs_eventhub_consumergroup_get`: Get details of a consumer group for an Event Hub.
  - `azmcp_eventhubs_eventhub_consumergroup_delete`: Delete a consumer group from an Event Hub.
- Added support for getting Azure AI Foundry (Cognitive Services) resource details via the command `azmcp_foundry_resource_get`. This unified command can list all AI Foundry resources in a subscription, filter by resource group, or get details for a specific resource including deployed models with their configurations (model name, version, SKU, capacity, and provisioning state). [[#762](https://github.com/microsoft/mcp/pull/762)]
- Added support for Azure Monitor Web Tests management operations: [[#529](https://github.com/microsoft/mcp/issues/529)]
  - `azmcp-monitor-webtests-list` - List all web tests in a subscription or optionally, within a resource group
  - `azmcp-monitor-webtests-get` - Get details for a specific web test
  - `azmcp-monitor-webtests-create` - Create a new web test in Azure Monitor
  - `azmcp-monitor-webtests-update` - Update an existing web test in Azure Monitor
- Added `azmcp extension cli generate` command for generating Azure Cli commands based on user intent. [[#203](https://github.com/microsoft/mcp/issues/203)]
- Added `azmcp extension cli install` command for getting installation instructions for Azure CLI, Azure Developer CLI and Azure Functions Core Tools. [[#74](https://github.com/microsoft/mcp/issues/74)]
- Added support for listing Application Insights distributed trace metadata via the command `azmcp_applicationinsights_apptrace_list` [[#671](https://github.com/microsoft/mcp/issues/671)]
- Added `IsServerCommandInvoked` telemetry field indicating that the MCP tool call resulted in a command invocation. [[#751](https://github.com/microsoft/mcp/pull/751)]

### Breaking Changes

- Fix flow of `Activity.Current` in telemetry service by changing `ITelemetryService`'s activity calls to synchronous. [[#558](https://github.com/microsoft/mcp/pull/558)]

### Bugs Fixed

### Other Changes

- Updated the description of the following commands to decrease ambiguity and increase selection accuracy by LLMs:
  - AKS (Azure Kubernetes Service)
    - `azmcp_aks_cluster_get`
    - `azmcp_aks_nodepool_get`
- Updated the description of Storage commands to decrease ambiguity and increase selection accuracy by LLMs: [[#650](https://github.com/microsoft/mcp/pull/650)]

## 0.8.6 (2025-10-09)

### Features Added

- Added `--tool` option to start Azure MCP server with only specific tools by name, providing fine-grained control over tool exposure. This option switches server mode to `--all` automatically. The `--namespace` and `--tool` options cannot be used together. [[#685](https://github.com/microsoft/mcp/issues/685)]
- Added support for getting ledger entries on Azure Confidential Ledger via the command `azmcp_confidentialledger_entries_get`. [[#705](https://github.com/microsoft/mcp/pull/723)]
- Added support for listing an Azure resource's activity logs via the command `azmcp_monitor_activitylog_list`. [[#720](https://github.com/microsoft/mcp/pull/720)]
- Added support for Azure AI Search knowledge bases and knowledge sources (preview):
  - `azmcp_search_knowledge_source_list` - List knowledge sources defined in an Azure AI Search service.
  - `azmcp_search_knowledge_base_list` - List knowledge bases defined in an Azure AI Search service.
  - `azmcp_search_knowledge_base_retrieve` - Execute a retrieval operation using a specified knowledge base with optional multi-turn conversation history.
  These commands enable agentic retrieval and reasoning grounded in Azure AI Search's new knowledge constructs.
- Bumped Azure AI Search .NET SDK dependency to align with knowledge agent APIs.

### Breaking Changes

- Unified required parameter validation: null or empty values now always throw `ArgumentException` with an improved message listing all invalid parameters. Previously this would throw either `ArgumentNullException` or `ArgumentException` only for the first invalid value. [[#718](https://github.com/microsoft/mcp/pull/718)]

### Other Changes

- Telemetry:
  - Added `ServerMode` telemetry tag to distinguish start-up modes for the MCP server. [[#738](https://github.com/microsoft/mcp/pull/738)]
  - Updated `ToolArea` telemetry field to be populated for namespace (and intent/learn) calls. [[#739](https://github.com/microsoft/mcp/pull/739)]

## 0.8.5 (2025-10-07)

### Features Added

- Added the following OpenAI commands: [[#647](https://github.com/microsoft/mcp/pull/647)]
  - `azmcp_foundry_openai_chat-completions-create`: Create interactive chat completions using Azure OpenAI chat models in AI Foundry.
  - `azmcp_foundry_openai_embeddings-create`: Generate vector embeddings using Azure OpenAI embedding models in AI Foundry
  - `azmcp_foundry_openai_models-list`: List all available OpenAI models and deployments in an Azure resource.
- Added support for sending SMS messages via Azure Communication Services with the command `azmcp_communication_sms_send`. [[#473](https://github.com/microsoft/mcp/pull/473)]
- Added support for appending tamper-proof ledger entries backed by TEEs and blockchain-style integrity guarantees in Azure Confidential Ledger via the command `azmcp_confidentialledger_entries_append`. [[#705](https://github.com/microsoft/mcp/pull/705)]
- Added the following Azure Managed Lustre commands:
  - `azmcp_azuremanagedlustre_filesystem_subnetsize_validate`: Check if the subnet can host the target Azure Managed Lustre SKU and size [[#110](https://github.com/microsoft/mcp/issues/110)].
  - `azmcp_azuremanagedlustre_filesystem_create`: Create an Azure Managed Lustre filesystem. [[#50](https://github.com/microsoft/mcp/issues/50)]
  - `azmcp_azuremanagedlustre_filesystem_update`: Update an Azure Managed Lustre filesystem. [[#50](https://github.com/microsoft/mcp/issues/50)]
- Added support for listing all Azure SignalR runtime instances or getting detailed information about a single one via the command `azmcp_signalr_runtime_get`. [[#83](https://github.com/microsoft/mcp/pull/83)]

### Breaking Changes

- Renamed `azmcp_azuremanagedlustre` commands to `azmcp_managedlustre`. [[#345](https://github.com/microsoft/mcp/issues/345)]
  - Renamed `azmcp_managedlustre_filesystem_required-subnet-size` to `azmcp_managedlustre_filesystem_subnetsize_ask`. [[#111](https://github.com/microsoft/mcp/issues/111)]
- Merged the following Azure Kubernetes Service (AKS) tools: [[#591](https://github.com/microsoft/mcp/issues/591)]
  - Merged `azmcp_aks_cluster_list` into `azmcp_aks_cluster_get`, which can perform both operations based on whether `--cluster` is passed.
  - Merged `azmcp_aks_nodepool_list` into `azmcp_aks_nodepool_get`, which can perform both operations based on whether `--nodepool` is passed.

### Bugs Fixed

- Improved description of Load Test commands. [[#92](https://github.com/microsoft/mcp/pull/92)]
- Fixed an issue where Azure Subscription tools were not available in the default (namespace) server mode. [[#634](https://github.com/microsoft/mcp/pull/634)]
- Improved error message for macOS users when interactive browser authentication fails due to broker threading requirements. The error now provides clear guidance to use Azure CLI, Azure PowerShell, or Azure Developer CLI for authentication instead. [[#684](https://github.com/microsoft/mcp/pull/684)]
- Added validation for the Cosmos query command `azmcp_cosmos_database_container_item_query`. [[#524](https://github.com/microsoft/mcp/pull/524)]
- Fixed the construction of Azure Resource Graph queries for App Configuration in the `FindAppConfigStore` method. The name filter is now correctly passed via the `additionalFilter` parameter instead of `tableName`, resolving "ExactlyOneStartingOperatorRequired" and "BadRequest" errors when setting key-value pairs. [[#670](https://github.com/microsoft/mcp/pull/670)]
- Updated the description of the Monitor tool and corrected the prompt for command `azmcp_monitor_healthmodels_entity_gethealth` to ensure that the LLM picks up the correct tool. [[#630](https://github.com/microsoft/mcp/pull/630)]
- Fixed "BadRequest" error in Azure Container Registry to get a registry, and in EventHubs to get a namespace. [[#729](https://github.com/microsoft/mcp/pull/729)]
- Added redundancy in Dockerfile to ensure the azmcp in the Docker image is actually executable. [[#732](https://github.com/microsoft/mcp/pull/732)]

### Other Changes

- Updated the description of `azmcp_bicepschema_get` to increase selection accuracy by LLMs. [[#649](https://github.com/microsoft/mcp/pull/649)]
- Update the `ToolName` telemetry field to use the normalized command name when the `CommandFactory` tool is used. [[#716](https://github.com/microsoft/mcp/pull/716)]
- Updated the default tool loading behavior to execute namespace tool calls directly instead of spawning separate child processes for each namespace. [[#704](https://github.com/microsoft/mcp/pull/704)]

#### Dependency updates

- Updated `Microsoft.Azure.Cosmos.Aot` from `0.1.2-preview.2` to `0.1.4-preview.2`, which upgrades the transitive Newtonsoft.Json dependency to `13.0.4`. [[#662](https://github.com/microsoft/mcp/pull/662)]

## 0.8.4 (2025-10-02)

### Features Added

- Added support to return metadata when using the `azmcp_tool_list` command. [[#564](https://github.com/microsoft/mcp/issues/564)]
- Added support for returning a list of tool namespaces instead of individual tools when using the `azmcp_tool_list` command with the `--namespaces` option. [[#496](https://github.com/microsoft/mcp/issues/496)]

### Breaking Changes

- Merged `azmcp_appconfig_kv_list` and `azmcp_appconfig_kv_show` into `azmcp_appconfig_kv_get` which can handle both listing and filtering key-values and getting a specific key-value. [[#505](https://github.com/microsoft/mcp/pull/505)]

### Bugs Fixed

- Fixed the name of the Key Vault Managed HSM settings get command from `azmcp_keyvault_admin_get` to `azmcp_keyvault_admin_settings_get`. [[#643](https://github.com/microsoft/mcp/issues/643)]
- Removed redundant DI instantiation of MCP server providers, as these are expected to be instantiated by the MCP server discovery mechanism. [[#644](https://github.com/microsoft/mcp/pull/644)]
- Fixed App Lens having a runtime error for reflection-based serialization when using native AoT MCP build. [[#639](https://github.com/microsoft/mcp/pull/639)]
- Added validation for the PostgreSQL database query command `azmcp_postgres_database_query`.[[#518](https://github.com/microsoft/mcp/pull/518)]

### Other Changes

- Change base Docker image from `bookworm-slim` to `alpine`. [[#651](https://github.com/microsoft/mcp/pull/651)]
- Refactored tool implementation to use Azure Resource Graph queries instead of direct ARM API calls:
  - Grafana [[#628](https://github.com/microsoft/mcp/pull/628)]
- Updated the description of the following commands to increase selection accuracy by LLMs:
  - App Deployment: `azmcp_deploy_app_logs_get` [[#640](https://github.com/microsoft/mcp/pull/640)]
  - Kusto: [[#666](https://github.com/microsoft/mcp/pull/666)]
    - `azmcp_kusto_cluster_get`
    - `azmcp_kusto_cluster_list`
    - `azmcp_kusto_database_list`
    - `azmcp_kusto_query`
    - `azmcp_kusto_sample`
    - `azmcp_kusto_table_list`
    - `azmcp_kusto_table_schema`
  - Redis: [[#655](https://github.com/microsoft/mcp/pull/655)]
    - `azmcp_redis_cache_list`
    - `azmcp_redis_cluster_list`
  - Service Bus: `azmcp_servicebus_topic_details` [[#642](https://github.com/microsoft/mcp/pull/642)]

#### Dependency Updates

- Updated the `ModelContextProtocol.AspNetCore` version from `0.3.0-preview.4` to `0.4.0-preview.1`. [[#576](https://github.com/Azure/azure-mcp/pull/576)]
- Removed the following dependencies:
  - `Azure.ResourceManager.Grafana` [[#628](https://github.com/microsoft/mcp/pull/622)]

## 0.8.3 (2025-09-30)

### Features Added

- Added support for Azure Developer CLI (azd) MCP tools when azd CLI is installed locally - [[#566](https://github.com/microsoft/mcp/issues/566)]
- Added support to proxy MCP capabilities when child servers leverage sampling or elicitation. [[#581](https://github.com/microsoft/mcp/pull/581)]
- Added support for publishing custom events to Event Grid topics via the command `azmcp_eventgrid_events_publish`. [[#514](https://github.com/microsoft/mcp/pull/514)]
- Added support for generating text completions using deployed Azure OpenAI models in AI Foundry via the command `azmcp_foundry_openai_create-completion`. [[#54](https://github.com/microsoft/mcp/pull/54)]
- Added support for speech recognition from an audio file with Azure AI Services Speech via the command `azmcp_speech_stt_recognize`. [[#436](https://github.com/microsoft/mcp/pull/436)]
- Added support for getting the details of an Azure Event Hubs namespace via the command `azmcp_eventhubs_namespace_get`. [[#105](https://github.com/microsoft/mcp/pull/105)]

### Breaking Changes

### Bugs Fixed

- Fixed an issue with the help option (`--help`) and enabled it across all commands and command groups. [[#583](https://github.com/microsoft/mcp/pull/583)]
- Fixed the following issues with Kusto commands:
  - `azmcp_kusto_cluster_list` and `azmcp_kusto_cluster_get` now accept the correct parameters expected by the service. [[#589](https://github.com/microsoft/mcp/issues/589)]
  - `azmcp_kusto_table_schema` now returns the correct table schema. [[#530](https://github.com/microsoft/mcp/issues/530)]
  - `azmcp_kusto_query` does not fail when the subscription id in the input query is enclosed in double quotes anymore. [[#152](https://github.com/microsoft/mcp/issues/152)]
  - All commands now return enough details in error messages when input parameters are invalid or missing. [[#575](https://github.com/microsoft/mcp/issues/575)]

### Other Changes

- Refactored tool implementation to use Azure Resource Graph queries instead of direct ARM API calls:
  - Authorization [[607](https://github.com/microsoft/mcp/pull/607)]
  - AppConfig [[606](https://github.com/microsoft/mcp/pull/606)]
  - ACR [[622](https://github.com/microsoft/mcp/pull/622)]
- Fixed the names of the following MySQL and Postgres commands: [[#614](https://github.com/microsoft/mcp/pull/614)]
  - `azmcp_mysql_server_config_config`    → `azmcp_mysql_server_config_get`
  - `azmcp_mysql_server_param_param`      → `azmcp_mysql_server_param_get`
  - `azmcp_mysql_table_schema_schema`     → `azmcp_mysql_table_schema_get`
  - `azmcp_postgres_server_config_config` → `azmcp_postgres_server_config_get`
  - `azmcp_postgres_server_param_param`   → `azmcp_postgres_server_param_get`
  - `azmcp_postgres_table_schema_schema`  → `azmcp_postgres_table_schema_get`
- Updated the description of the following commands to increase selection accuracy by LLMs:
  - AI Foundry: [[#599](https://github.com/microsoft/mcp/pull/599)]
    - `azmcp_foundry_agents_connect`
    - `azmcp_foundry_models_deploy`
    - `azmcp_foundry_models_deployments_list`
  - App Lens: `azmcp_applens_resource_diagnose` [[#556](https://github.com/microsoft/mcp/pull/556)]
  - Cloud Architect: `azmcp_cloudarchitect_design` [[#587](https://github.com/microsoft/mcp/pull/587)]
  - Cosmos DB: `azmcp_cosmos_database_container_item_query` [[#625](https://github.com/microsoft/mcp/pull/625)]
  - Event Grid: [[#552](https://github.com/microsoft/mcp/pull/552)]
    - `azmcp_eventgrid_subscription_list`
    - `azmcp_eventgrid_topic_list`
  - Key Vault: [[#608](https://github.com/microsoft/mcp/pull/608)]
    - `azmcp_keyvault_certificate_create`
    - `azmcp_keyvault_certificate_import`
    - `azmcp_keyvault_certificate_get`
    - `azmcp_keyvault_certificate_list`
    - `azmcp_keyvault_key_create`
    - `azmcp_keyvault_key_get`
    - `azmcp_keyvault_key_list`
    - `azmcp_keyvault_secret_create`
    - `azmcp_keyvault_secret_get`
    - `azmcp_keyvault_secret_list`
  - MySQL: [[#614](https://github.com/microsoft/mcp/pull/614)]
    - `azmcp_mysql_server_param_set`
  - Postgres: [[#562](https://github.com/microsoft/mcp/pull/562)]
    - `azmcp_postgres_database_query`
    - `azmcp_postgres_server_param_set`
  - Resource Health: [[#588](https://github.com/microsoft/mcp/pull/588)]
    - `azmcp_resourcehealth_availability-status_get`
    - `azmcp_resourcehealth_service-health-events_list`
  - SQL: [[#594](https://github.com/microsoft/mcp/pull/594)]
    - `azmcp_sql_db_delete`
    - `azmcp_sql_db_update`
    - `azmcp_sql_server_delete`
  - Subscriptions: `azmcp_subscription_list` [[#559](https://github.com/microsoft/mcp/pull/559)]

#### Dependency Updates

- Removed the following dependencies:
  - `Azure.ResourceManager.Authorization` [[#607](https://github.com/microsoft/mcp/pull/607)]
  - `Azure.ResourceManager.AppConfiguration` [[#606](https://github.com/microsoft/mcp/pull/606)]
  - `Azure.ResourceManager.ContainerRegistry` [[#622](https://github.com/microsoft/mcp/pull/622)]

## 0.8.2 (2025-09-25)

### Bugs Fixed

- Fixed `azmcp_subscription_list` to return empty enumerable instead of `null` when no subscriptions are found. [[#508](https://github.com/microsoft/mcp/pull/508)]

## 0.8.1 (2025-09-23)

### Features Added

- Added support for listing SQL servers in a subscription and resource group via the command `azmcp_sql_server_list`. [[#503](https://github.com/microsoft/mcp/issues/503)]
- Added support for renaming Azure SQL databases within a server while retaining configuration via the `azmcp sql db rename` command. [[#542](https://github.com/microsoft/mcp/pull/542)]
- Added support for Azure App Service database management via the command `azmcp_appservice_database_add`. [[#59](https://github.com/microsoft/mcp/pull/59)]
- Added the following Azure Foundry agents commands: [[#55](https://github.com/microsoft/mcp/pull/55)]
  - `azmcp_foundry_agents_connect`: Connect to an agent in an AI Foundry project and query it
  - `azmcp_foundry_agents_evaluate`: Evaluate a response from an agent by passing query and response inline
  - `azmcp_foundry_agents_query_and_evaluate`: Connect to an agent in an AI Foundry project, query it, and evaluate the response in one step
- Enhanced AKS managed cluster information with comprehensive properties. [[#490](https://github.com/microsoft/mcp/pull/490)]
- Added support retrieving Key Vault Managed HSM account settings via the command `azmcp-keyvault-admin-settings-get`. [[#358](https://github.com/microsoft/mcp/pull/358)]

### Breaking Changes

- Removed the following Storage tools: [[#500](https://github.com/microsoft/mcp/pull/500)]
  - `azmcp_storage_blob_batch_set-tier`
  - `azmcp_storage_datalake_directory_create`
  - `azmcp_storage_datalake_file-system_list-paths`
  - `azmcp_storage_queue_message_send`
  - `azmcp_storage_share_file_list`
  - `azmcp_storage_table_list`
- Updated the `OpenWorld` and `Destructive` hints for all tools. [[#510](https://github.com/microsoft/mcp/pull/510)]

### Bugs Fixed

- Fixed MCP server hanging on invalid transport arguments. Server now exits gracefully with clear error messages instead of hanging indefinitely. [[#511](https://github.com/microsoft/mcp/pull/511)]

### Other Changes

- Refactored Kusto service implementation to use Azure Resource Graph queries instead of direct ARM API calls. [[#528](https://github.com/microsoft/mcp/pull/528)]
- Refactored Storage service implementation [[#539](https://github.com/microsoft/mcp/pull/539)]
  - Replaced direct ARM API calls in `azmcp_storage_account_get` with Azure Resource Graph queries.
  - Updated `azmcp_storage_account_create` to use the GenericResource approach instead of direct ARM API calls.
- Updated `IAreaSetup` API so the area's command tree is returned rather than modifying an existing object. It's also more DI-testing friendly. [[#478](https://github.com/microsoft/mcp/pull/478)]
- Updated `CommandFactory.GetServiceArea` to check for a tool's service area with or without the root `azmcp` prefix. [[#478](https://github.com/microsoft/mcp/pull/478)]

#### Dependency Updates

- Removed the following dependencies:
  - `Azure.ResourceManager.Kusto` [[#528](https://github.com/microsoft/mcp/pull/528)]

## 0.8.0 (2025-09-18)

### Features Added

- Added the `--insecure-disable-elicitation` server startup switch. When enabled, the server will bypass user confirmation (elicitation) for tools marked as handling secrets and execute them immediately. This is **INSECURE** and meant only for controlled automation scenarios (e.g., CI or disposable test environments) because it removes a safety barrier that helps prevent accidental disclosure of sensitive data. [[#486](https://github.com/microsoft/mcp/pull/486)]
- Enhanced Azure authentication with targeted credential selection via the `AZURE_TOKEN_CREDENTIALS` environment variable: [[#56](https://github.com/microsoft/mcp/pull/56)]
  - `"dev"`: Development credentials (Visual Studio → Visual Studio Code → Azure CLI → Azure PowerShell → Azure Developer CLI)
  - `"prod"`: Production credentials (Environment → Workload Identity → Managed Identity)
  - Specific credential names (e.g., `"AzureCliCredential"`): Target only that credential
  - Improved Visual Studio Code credential error handling with proper exception wrapping for credential chaining
  - Replaced custom `DefaultAzureCredential` implementation with explicit credential chain for better control and transparency
  - For more details, see [Controlling Authentication Methods with AZURE_TOKEN_CREDENTIALS](https://github.com/microsoft/mcp/blob/main/servers/Azure.Mcp.Server/TROUBLESHOOTING.md#controlling-authentication-methods-with-azure_token_credentials)
- Enhanced AKS nodepool information with comprehensive properties. [[#454](https://github.com/microsoft/mcp/pull/454)]
- Added support for updating Azure SQL databases via the command `azmcp_sql_db_update`. [[#488](https://github.com/microsoft/mcp/pull/488)]
- Added support for listing Event Grid subscriptions via the command `azmcp_eventgrid_subscription_list`. [[#364](https://github.com/microsoft/mcp/pull/364)]
- Added support for listing Application Insights code optimization recommendations across components via the command `azmcp_applicationinsights_recommendation_list`. [#387](https://github.com/microsoft/mcp/pull/387)
- **Errata**: The following was announced as part of release `0.7.0, but was not actually included then.
  - Added support for creating and deleting SQL databases via the commands `azmcp_sql_db_create` and `azmcp_sql_db_delete`. [[#434](https://github.com/microsoft/mcp/pull/434)]
- Restored support for the following Key Vault commands: [[#506](https://github.com/microsoft/mcp/pull/506)]
  - `azmcp_keyvault_key_get`
  - `azmcp_keyvault_secret_get`

### Breaking Changes

- Redesigned how conditionally required options are handled. Commands now use explicit option registration via extension methods (`.AsRequired()`, `.AsOptional()`) instead of legacy patterns (`UseResourceGroup()`, `RequireResourceGroup()`). [[#452](https://github.com/microsoft/mcp/pull/452)]
- Removed support for the `AZURE_MCP_INCLUDE_PRODUCTION_CREDENTIALS` environment variable. Use `AZURE_TOKEN_CREDENTIALS` instead for more flexible credential selection. For migration details, see [Controlling Authentication Methods with AZURE_TOKEN_CREDENTIALS](https://github.com/microsoft/mcp/blob/main/servers/Azure.Mcp.Server/TROUBLESHOOTING.md#controlling-authentication-methods-with-azure_token_credentials). [[#56](https://github.com/microsoft/mcp/pull/56)]
- Merged `azmcp_appconfig_kv_lock` and `azmcp_appconfig_kv_unlock` into `azmcp_appconfig_kv_lock_set` which can handle locking or unlocking a key-value based on the `--lock` parameter. [[#485](https://github.com/microsoft/mcp/pull/485)]


### Other Changes

- Update `azmcp_foundry_models_deploy` to use "GenericResource" for deploying models to Azure AI Services. [[#456](https://github.com/microsoft/mcp/pull/456)]

#### Dependency Updates

- Replaced the `Azure.Bicep.Types.Az` dependency with `Microsoft.Azure.Mcp.AzTypes.Internal.Compact`. [[#472](https://github.com/microsoft/mcp/pull/472)]

## 0.7.0 (2025-09-16)

### Features Added

- Added support for getting a node pool in an AKS managed cluster via the command `azmcp_aks_nodepool_get`. [[#394](https://github.com/microsoft/mcp/pull/394)]
- Added support for diagnosing Azure Resources using the App Lens API via the command `azmcp_applens_resource_diagnose`. [[#356](https://github.com/microsoft/mcp/pull/356)]
- Added elicitation support. An elicitation request is sent if the tool annotation `secret` hint is true. [[#404](https://github.com/microsoft/mcp/pull/404)]
- Added `azmcp_sql_server_create`, `azmcp_sql_server_delete`, `azmcp_sql_server_show` to support SQL server create, delete, and show commands. [[#312](https://github.com/microsoft/mcp/pull/312)]
- Added the support for getting information about Azure Managed Lustre SKUs via the following command `azmcp_azuremanagedlustre_filesystem_get_sku_info`. [[#100](https://github.com/microsoft/mcp/issues/100)]
- Added support for creating and deleting SQL databases via the commands `azmcp_sql_db_create` and `azmcp_sql_db_delete`. [[#434](https://github.com/microsoft/mcp/pull/434)]
- `azmcp_functionapp_get` can now list Function Apps on a resource group level. [[#427](https://github.com/microsoft/mcp/pull/427)]

### Breaking Changes

- Merged `azmcp_functionapp_list` into `azmcp_functionapp_get`, which can perform both operations based on whether `--function-app` is passed. [[#427](https://github.com/microsoft/mcp/pull/427)]
- Removed Azure CLI (`az`) and Azure Developer CLI (`azd`) extension tools to reduce complexity and focus on native Azure service operations. [[#404](https://github.com/microsoft/mcp/pull/404)].

### Bugs Fixed

- Marked the `secret` hint of `azmcp_keyvault_secret_create` tool to "true". [[#430](https://github.com/microsoft/mcp/pull/430)]

### Other Changes

- Replaced bicep tool dependency on Azure.Bicep.Types.Az package with Microsoft.Azure.Mcp.AzTypes.Internal.Compact package. [[#472](https://github.com/microsoft/mcp/pull/472)]

## 0.6.0 (2025-09-11)

### Features Added

- **The Azure MCP Server is now also available on NuGet.org** [[#368](https://github.com/microsoft/mcp/pull/368)]
- Added support for listing node pools in an AKS managed cluster via the command `azmcp_aks_nodepool_list`. [[#360](https://github.com/microsoft/mcp/pull/360)]

### Breaking Changes

- To improve performance, packages now ship with trimmed binaries that have unused code and dependencies removed, resulting in significantly smaller file sizes, faster startup times, and reduced memory footprint. [Learn more](https://learn.microsoft.com/dotnet/core/deploying/trimming/trim-self-contained). [[#405](https://github.com/microsoft/mcp/pull/405)]
- Merged `azmcp_search_index_describe` and `azmcp_search_index_list` into `azmcp_search_index_get`, which can perform both operations based on whether `--index` is passed. [[#378](https://github.com/microsoft/mcp/pull/378)]
- Merged the following Storage tools: [[#376](https://github.com/microsoft/mcp/pull/376)]
  - `azmcp_storage_account_details` and `azmcp_storage_account_list` into `azmcp_storage_account_get`, which supports the behaviors of both tools based on whether `--account` is passed.
  - `azmcp_storage_blob_details` and `azmcp_storage_blob_list` into `azmcp_storage_blob_get`, which supports the behaviors of both tools based on whether `--blob` is passed.
  - `azmcp_storage_blob_container_details` and `azmcp_storage_blob_container_list` into `azmcp_storage_blob_container_get`, which supports the behaviors of both tools based on whether `--container` is passed.
- Updated the descriptions of all Storage tools. [[#376](https://github.com/microsoft/mcp/pull/376)]

### Other Changes

#### Dependency updates

- Updated the following dependencies: [[#380](https://github.com/microsoft/mcp/pull/380)]
  - Azure.Core: `1.47.1` → `1.48.0`
  - Azure.Identity: `1.15.0` → `1.16.0`

## 0.5.13 (2025-09-10)

### Features Added

- Added support for listing all Event Grid topics in a subscription via the command `azmcp_eventgrid_topic_list`. [[#43](https://github.com/microsoft/mcp/pull/43)]
- Added support for retrieving knowledge index schema information in Azure AI Foundry projects via the command `azmcp_foundry_knowledge_index_schema`. [[#41](https://github.com/microsoft/mcp/pull/41)]
- Added support for listing service health events in a subscription via the command `azmcp_resourcehealth_service-health-events_list`. [[#367](https://github.com/microsoft/mcp/pull/367)]

### Breaking Changes

- Updated/removed options for the following commands: [[#108](https://github.com/microsoft/mcp/pull/108)]
  - `azmcp_storage_account_create`: Removed the ability to configure `enable-https-traffic-only` (always `true` now), `allow-blob-public-access` (always `false` now), and `kind` (always `StorageV2` now).
  - `azmcp_storage_blob_container_create`: Removed the ability to configure `blob-container-public-access` (always `false` now).
  - `azmcp_storage_blob_upload`: Removed the ability to configure `overwrite` (always `false` now).

### Bugs Fixed

- Fixed telemetry bug where "ToolArea" was incorrectly populated in with "ToolName". [[#346](https://github.com/microsoft/mcp/pull/346)]

### Other Changes

- Added telemetry to log parameter values for the `azmcp_bestpractices_get` tool. [[#375](https://github.com/microsoft/mcp/pull/375)]
- Updated tool annotations. [[#377](https://github.com/microsoft/mcp/pull/377)]

#### Dependency updates

- Updated the following dependencies:
  - Azure.Identity: `1.14.0` → `1.15.0` [[#352](https://github.com/microsoft/mcp/pull/352)]
  - Azure.Identity.Broker: `1.2.0` → `1.3.0` [[#352](https://github.com/microsoft/mcp/pull/352)]
  - Microsoft.Azure.Cosmos.Aot: `0.1.1-preview.1` → `0.1.2-preview.1` [[#383](https://github.com/microsoft/mcp/pull/383)]
- Updated the following dependency to improve .NET Ahead-of-Time (AOT) compilation support: [[#363](https://github.com/microsoft/mcp/pull/363)]
  - Azure.ResourceManager.StorageCache: `1.3.1` → `1.3.2`

## 0.5.12 (2025-09-04)

### Features Added

- Added `azmcp_sql_server_firewall-rule_create` and `azmcp_sql_server_firewall-rule_delete` commands. [[#121](https://github.com/microsoft/mcp/pull/121)]

### Bugs Fixed

- Fixed a bug in MySQL query validation logic. [[#81](https://github.com/microsoft/mcp/pull/81)]

### Other Changes

AOT- Added a verb to the namespace name for bestpractices [[#109](https://github.com/microsoft/mcp/pull/109)]
- Added instructions about consumption plan for azure functions deployment best practices [[#218](https://github.com/microsoft/mcp/pull/218)]

## 0.5.11 (2025-09-02)

### Other Changes

- Fixed VSIX signing [[#91](https://github.com/microsoft/mcp/pull/91)]
- Included native packages in build artifacts and pack/release scripts. [[#51](https://github.com/microsoft/mcp/pull/51)]

## 0.5.10 (2025-08-28)

### Bugs fixed

- Fixed a bug with telemetry collection related to AppConfig tools. [[#44](https://github.com/microsoft/mcp/pull/44)]

## 0.5.9 (2025-08-26)

### Other Changes

#### Dependency Updates

- Updated the following dependencies to improve .NET Ahead-of-Time (AOT) compilation support:
  - Microsoft.Azure.Cosmos `3.51.0` → Microsoft.Azure.Cosmos.Aot `0.1.1-preview.1`. [[#37](https://github.com/microsoft/mcp/pull/37)]

## 0.5.8 (2025-08-21)

### Features Added

- Added support for listing knowledge indexes in Azure AI Foundry projects via the command `azmcp_foundry_knowledge_index_list`. [[#1004](https://github.com/Azure/azure-mcp/pull/1004)]
- Added support for getting details of an Azure Function App via the command `azmcp_functionapp_get`. [[#970](https://github.com/Azure/azure-mcp/pull/970)]
- Added the following Azure Managed Lustre commands: [[#1003](https://github.com/Azure/azure-mcp/issues/1003)]
  - `azmcp_azuremanagedlustre_filesystem_list`: List available Azure Managed Lustre filesystems.
  - `azmcp_azuremanagedlustre_filesystem_required-subnet-size`: Returns the number of IP addresses required for a specific SKU and size of Azure Managed Lustre filesystem.
- Added support for designing Azure Cloud Architecture through guided questions via the command `azmcp_cloudarchitect_design`. [[#890](https://github.com/Azure/azure-mcp/pull/890)]
- Added support for the following Azure MySQL operations: [[#855](https://github.com/Azure/azure-mcp/issues/855)]
  - `azmcp_mysql_database_list` - List all databases in a MySQL server.
  - `azmcp_mysql_database_query` - Executes a SELECT query on a MySQL Database. The query must start with SELECT and cannot contain any destructive SQL operations for security reasons.
  - `azmcp_mysql_table_list` - List all tables in a MySQL database.
  - `azmcp_mysql_table_schema_get` - Get the schema of a specific table in a MySQL database.
  - `azmcp_mysql_server_config_get` - Retrieve the configuration of a MySQL server.
  - `azmcp_mysql_server_list` - List all MySQL servers in a subscription & resource group.
  - `azmcp_mysql_server_param_get` - Retrieve a specific parameter of a MySQL server.
  - `azmcp_mysql_server_param_set` - Set a specific parameter of a MySQL server to a specific value.
- Added telemetry for tracking service area when calling tools. [[#1024](https://github.com/Azure/azure-mcp/pull/1024)]

### Breaking Changes

- Renamed the following Storage tool option names: [[#1015](https://github.com/Azure/azure-mcp/pull/1015)]
  - `azmcp_storage_account_create`: `account-name` → `account`.
  - `azmcp_storage_blob_batch_set-tier`: `blob-names` → `blobs`.

### Bugs Fixed

- Fixed SQL service test assertions to use case-insensitive string comparisons for resource type validation. [[#938](https://github.com/Azure/azure-mcp/pull/938)]
- Fixed HttpClient service test assertions to properly validate NoProxy collection handling instead of expecting a single string value. [[#938](https://github.com/Azure/azure-mcp/pull/938)]

### Other Changes

- Introduced the `BaseAzureResourceService` class to allow performing Azure Resource read operations using Azure Resource Graph queries. [[#938](https://github.com/Azure/azure-mcp/pull/938)]
- Refactored SQL service implementation to use Azure Resource Graph queries instead of direct ARM API calls. [[#938](https://github.com/Azure/azure-mcp/pull/938)]
  - Removed dependency on `Azure.ResourceManager.Sql` package by migrating to Azure Resource Graph queries, reducing package size and improving startup performance.
- Enhanced `BaseAzureService` with `EscapeKqlString` method for safe KQL query construction across all Azure services. [[#938](https://github.com/Azure/azure-mcp/pull/938)]
  - Fixed KQL string escaping in Workbooks service queries.
- Standardized Azure Storage command descriptions, option names, and parameter names for consistency across all storage commands. Updated JSON serialization context to remove unused model types and improve organization. [[#1015](https://github.com/Azure/azure-mcp/pull/1015)]
- Updated to .NET 10 SDK to prepare for .NET tool packing. [[#1023](https://github.com/Azure/azure-mcp/pull/1023)]
- Enhanced `bestpractices` and `azureterraformbestpractices` tool descriptions to better work with the vscode copilot tool grouping feature. [[#1029](https://github.com/Azure/azure-mcp/pull/1029)]
- The Azure MCP Server can now be packaged as a .NET SDK Tool for easier use by users with the .NET 10 SDK installed. [[#422](https://github.com/Azure/azure-mcp/issues/422)]

#### Dependency Updates

- Updated the following dependencies to improve .NET Ahead-of-Time (AOT) compilation support: [[#1031](https://github.com/Azure/azure-mcp/pull/1031)]
  - Azure.ResourceManager.ResourceHealth: `1.0.0` → `1.1.0-beta.5`

## 0.5.7 (2025-08-19)

### Features Added
- Added support for the following Azure Deploy and Azure Quota operations: [[#626](https://github.com/Azure/azure-mcp/pull/626)]
  - `azmcp_deploy_app_logs_get` - Get logs from Azure applications deployed using azd.
  - `azmcp_deploy_iac_rules_get` - Get Infrastructure as Code rules.
  - `azmcp_deploy_pipeline_guidance-get` - Get guidance for creating CI/CD pipelines to provision Azure resources and deploy applications.
  - `azmcp_deploy_plan_get` - Generate deployment plans to construct infrastructure and deploy applications on Azure.
  - `azmcp_deploy_architecture_diagram-generate` - Generate Azure service architecture diagrams based on application topology.
  - `azmcp_quota_region_availability-list` - List available Azure regions for specific resource types.
  - `azmcp_quota_usage_check` - Check Azure resource usage and quota information for specific resource types and regions.
- Added support for listing Azure Function Apps via the command `azmcp-functionapp-list`. [[#863](https://github.com/Azure/azure-mcp/pull/863)]
- Added support for importing existing certificates into Azure Key Vault via the command `azmcp-keyvault-certificate-import`. [[#968](https://github.com/Azure/azure-mcp/issues/968)]
- Added support for uploading a local file to an Azure Storage blob via the command `azmcp-storage-blob-upload`. [[#960](https://github.com/Azure/azure-mcp/pull/960)]
- Added support for the following Azure Service Health operations: [[#998](https://github.com/Azure/azure-mcp/pull/998)]
  - `azmcp-resourcehealth-availability-status-get` - Get the availability status for a specific resource.
  - `azmcp-resourcehealth-availability-status-list` - List availability statuses for all resources in a subscription or resource group.
- Added support for listing repositories in Azure Container Registries via the command `azmcp-acr-registry-repository-list`. [[#983](https://github.com/Azure/azure-mcp/pull/983)]

### Other Changes

- Improved guidance for LLM interactions with Azure MCP server by adding rules around bestpractices tool calling to server instructions. [[#1007](https://github.com/Azure/azure-mcp/pull/1007)]

#### Dependency Updates

- Updated the following dependencies to improve .NET Ahead-of-Time (AOT) compilation support: [[#893](https://github.com/Azure/azure-mcp/pull/893)]
  - Azure.Bicep.Types: `0.5.110` → `0.6.1`
  - Azure.Bicep.Types.Az: `0.2.771` → `0.2.792`
- Added the following dependencies to support Azure Managed Lustre
  - Azure.ResourceManager.StorageCache:  `1.3.1`

## 0.5.6 (2025-08-14)

### Features Added

- Added support for listing Azure Function Apps via the command `azmcp-functionapp-list`. [[#863](https://github.com/Azure/azure-mcp/pull/863)]
- Added support for getting details about an Azure Storage Account via the command `azmcp-storage-account-details`. [[#934](https://github.com/Azure/azure-mcp/issues/934)]

### Other Changes

- Refactored resource group option (`--resource-group`) handling and validation for all commands to a centralized location. [[#961](https://github.com/Azure/azure-mcp/issues/961)]

#### Dependency Updates

- Updated the following dependencies to improve .NET Ahead-of-Time (AOT) compilation support: [[#967](https://github.com/Azure/azure-mcp/issues/967)] [[#969](https://github.com/Azure/azure-mcp/issues/969)]
  - Azure.Monitor.Query: `1.6.0` → `1.7.1`
  - Azure.Monitor.Ingestion: `1.1.2` → `1.2.0`
  - Azure.Search.Documents: `11.7.0-beta.4` → `11.7.0-beta.6`
  - Azure.ResourceManager.ContainerRegistry: `1.3.0` → `1.3.1`
  - Azure.ResourceManager.DesktopVirtualization: `1.3.1` → `1.3.2`
  - Azure.ResourceManager.PostgreSql: `1.3.0` → `1.3.1`

## 0.5.5 (2025-08-12)

### Features Added

- Added support for listing ACR (Azure Container Registry) registries in a subscription via the command `azmcp-acr-registry-list`. [[#915](https://github.com/Azure/azure-mcp/issues/915)]
- Added the following Azure Storage commands:
  - `azmcp-storage-account-create`: Create a new Azure Storage account. [[#927](https://github.com/Azure/azure-mcp/issues/927)]
  - `azmcp-storage-queue-message-send`: Send a message to an Azure Storage queue. [[#794](https://github.com/Azure/azure-mcp/pull/794)]
  - `azmcp-storage-blob-details`: Get details about an Azure Storage blob. [[#930](https://github.com/Azure/azure-mcp/issues/930)]
  - `azmcp-storage-blob-container-create`: Create a new Azure Storage blob container. [[#937](https://github.com/Azure/azure-mcp/issues/937)]

### Breaking Changes

- The `azmcp-storage-account-list` command now returns account metadata objects instead of plain strings. Each item includes: `name`, `location`, `kind`, `skuName`, `skuTier`, `hnsEnabled`, `allowBlobPublicAccess`, `enableHttpsTrafficOnly`. Update scripts to read the `name` property. The underlying `IStorageService.GetStorageAccounts()` signature changed from `Task<List<string>>` to `Task<List<StorageAccountInfo>>`. [[#904](https://github.com/Azure/azure-mcp/issues/904)]

### Bugs Fixed

- Fixed best practices tool invocation failure when passing "all" action with "general" or "azurefunctions" resources. [[#757](https://github.com/Azure/azure-mcp/issues/757)]
- Updated metadata for CREATE and SET tools to `destructive = true`. [[#773](https://github.com/Azure/azure-mcp/pull/773)]

### Other Changes
- Consolidate "AzSubscriptionGuid" telemetry logic into `McpRuntime`. [[#935](https://github.com/Azure/azure-mcp/pull/935)]

## 0.5.4 (2025-08-07)

### Bugs Fixed

- Fixed subscription parameter handling across all Azure MCP service methods to consistently use `subscription` instead of `subscriptionId`, enabling proper support for both subscription IDs and subscription names. [[#877](https://github.com/Azure/azure-mcp/issues/877)]
- Fixed `ToolExecuted` telemetry activity being created twice. [[#741](https://github.com/Azure/azure-mcp/pull/741)]

### Other Changes

- Improved Azure MCP display name in VS Code from 'azure-mcp-server-ext' to 'Azure MCP' for better user experience in the Configure Tools interface. [[#871](https://github.com/Azure/azure-mcp/issues/871), [#876](https://github.com/Azure/azure-mcp/pull/876)]
- Updated the  following `CommandGroup` descriptions to improve their tool usage by Agents:
  - Azure AI Search [[#874](https://github.com/Azure/azure-mcp/pull/874)]
  - Storage [[#879](https://github.com/Azure/azure-mcp/pull/879)]

## 0.5.3 (2025-08-05)

### Features Added

- Added support for providing the `--content-type` and `--tags` properties to the `azmcp-appconfig-kv-set` command. [[#459](https://github.com/Azure/azure-mcp/pull/459)]
- Added `filter-path` and `recursive` capabilities to `azmcp-storage-datalake-file-system-list-paths`. [[#770](https://github.com/Azure/azure-mcp/issues/770)]
- Added support for listing files and directories in Azure File Shares via the `azmcp-storage-share-file-list` command. This command recursively lists all items in a specified file share directory with metadata including size, last modified date, and content type. [[#793](https://github.com/Azure/azure-mcp/pull/793)]
- Added support for Azure Virtual Desktop with new commands: [[#653](https://github.com/Azure/azure-mcp/pull/653)]
  - `azmcp-virtualdesktop-hostpool-list` - List all host pools in a subscription
  - `azmcp-virtualdesktop-sessionhost-list` - List all session hosts in a host pool
  - `azmcp-virtualdesktop-sessionhost-usersession-list` - List all user sessions on a specific session host
- Added support for creating and publishing DevDeviceId in telemetry. [[#810](https://github.com/Azure/azure-mcp/pull/810/)]

### Breaking Changes

- **Parameter Name Changes**: Removed unnecessary "-name" suffixes from command parameters across 25+ parameters in 12+ Azure service areas to improve consistency and usability. Users will need to update their command-line usage and scripts. [[#853](https://github.com/Azure/azure-mcp/pull/853)]
  - **AppConfig**: `--account-name` → `--account`
  - **Search**: `--service-name` → `--service`, `--index-name` → `--index`
  - **Cosmos**: `--account-name` → `--account`, `--database-name` → `--database`, `--container-name` → `--container`
  - **Kusto**: `--cluster-name` → `--cluster`, `--database-name` → `--database`, `--table-name` → `--table`
  - **AKS**: `--cluster-name` → `--cluster`
  - **Postgres**: `--user-name` → `--user`
  - **ServiceBus**: `--queue-name` → `--queue`, `--topic-name` → `--topic`
  - **Storage**: `--account-name` → `--account`, `--container-name` → `--container`, `--table-name` → `--table`, `--file-system-name` → `--file-system`, `--tier-name` → `--tier`
  - **Monitor**: `--table-name` → `--table`, `--model` → `--health-model`, `--resource-name` → `--resource`
  - **Foundry**: `--deployment-name` → `--deployment`, `--publisher-name` → `--publisher`, `--license-name` → `--license`, `--sku-name` → `--sku`, `--azure-ai-services-name` → `--azure-ai-services`

### Bugs Fixed

- Fixed an issue where the `azmcp-storage-blob-batch-set-tier` command did not correctly handle the `--tier` parameter when setting the access tier for multiple blobs. [[#808](https://github.com/Azure/azure-mcp/pull/808)]

### Other Changes

- Implemented centralized HttpClient service with proxy support for better resource management and enterprise compatibility. [[#857](https://github.com/Azure/azure-mcp/pull/857)]
- Added caching for Cosmos DB databases and containers. [[#813](https://github.com/Azure/azure-mcp/pull/813)]
- Refactored PostgreSQL commands to follow ObjectVerb naming pattern, fix command hierarchy, and ensure all commands end with verbs. This improves consistency and discoverability across all postgres commands. [[#865](https://github.com/Azure/azure-mcp/issues/865)] [[#866](https://github.com/Azure/azure-mcp/pull/866)]

#### Dependency Updates

- Updated the following dependencies to improve .NET Ahead-of-Time (AOT) compilation support. AOT will enable shipping Azure MCP Server as self-contained native executable.
  - Azure.Core: `1.46.2` → `1.47.1`
  - Azure.ResourceManager: `1.13.1` → `1.13.2`
  - Azure.ResourceManager.ApplicationInsights: `1.0.1` → `1.1.0-beta.1`
  - Azure.ResourceManager.AppConfiguration: `1.4.0` → `1.4.1`
  - Azure.ResourceManager.Authorization: `1.1.4` → `1.1.5`
  - Azure.ResourceManager.ContainerService: `1.2.3` → `1.2.5`
  - Azure.ResourceManager.Kusto: `1.6.0` → `1.6.1`
  - Azure.ResourceManager.CognitiveServices: `1.4.0` → `1.5.1`
  - Azure.ResourceManager.Redis: `1.5.0` → `1.5.1`
  - Azure.ResourceManager.RedisEnterprise: `1.1.0` → `1.2.1`
  - Azure.ResourceManager.LoadTesting: `1.1.1` → `1.1.2`
  - Azure.ResourceManager.Sql: `1.3.0` → `1.4.0-beta.3`
  - Azure.ResourceManager.Datadog: `1.0.0-beta.5` → `1.0.0-beta.6`
  - Azure.ResourceManager.CosmosDB: `1.3.2` → `1.4.0-beta.13`
  - Azure.ResourceManager.OperationalInsights: `1.3.0` → `1.3.1`
  - Azure.ResourceManager.Search: `1.2.3` → `1.3.0`
  - Azure.ResourceManager.Storage: `1.4.2` → `1.4.4`
  - Azure.ResourceManager.Grafana: `1.1.1` → `1.2.0-beta.2`
  - Azure.ResourceManager.ResourceGraph: `1.1.0-beta.3` → `1.1.0-beta.4`

## 0.5.2 (2025-07-31)

### Features Added

- Added support for batch setting access tier for multiple Azure Storage blobs via the `azmcp-storage-blob-batch-set-tier` command. This command efficiently changes the storage tier (Hot, Cool, Archive, etc) for multiple blobs simultaneously in a single operation. [[#735](https://github.com/Azure/azure-mcp/issues/735)]
- Added descriptions to all Azure MCP command groups to improve discoverability and usability when running the server with `--mode single` or `--mode namespace`. [[#791](https://github.com/Azure/azure-mcp/pull/791)]

### Breaking Changes

- Removed `--partner-tenant-id` option from `azmcp-marketplace-product-get` command. [[#656](https://github.com/Azure/azure-mcp/pull/656)]

## 0.5.1 (2025-07-29)

### Features Added

- Added support for listing SQL databases via the command: `azmcp-sql-db-list`. [[#746](https://github.com/Azure/azure-mcp/pull/746)]
- Added support for reading `AZURE_SUBSCRIPTION_ID` from the environment variables if a subscription is not provided. [[#533](https://github.com/Azure/azure-mcp/pull/533)]

### Breaking Changes

- Removed the following Key Vault operations: [[#768](https://github.com/Azure/azure-mcp/pull/768)]
  - `azmcp-keyvault-secret-get`
  - `azmcp-keyvault-key-get`

### Other Changes

- Improved the MAC address search logic for telemetry by making it more robust in finding a valid network interface. [[#759](https://github.com/Azure/azure-mcp/pull/759)]
- Major repository structure change:
  - Service areas moved from `/src/areas/{Area}` and `/tests/areas/{Area}` into `/areas/{area}/src` and `/areas/{area}/tests`
  - Common code moved into `/core/src` and `/core/tests`

## 0.5.0 (2025-07-24)

### Features Added

- Added a new VS Code extension (VSIX installer) for the VS Code Marketplace. [[#661](https://github.com/Azure/azure-mcp/pull/661)]
- Added `--mode all` startup option to expose all Azure MCP tools individually. [[#689](https://github.com/Azure/azure-mcp/issues/689)]
- Added more tools for Azure Key Vault: [[#517](https://github.com/Azure/azure-mcp/pull/517)]
  - `azmcp-keyvault-certificate-list` - List certificates in a key vault
  - `azmcp-keyvault-certificate-get` - Get details of a specific certificate
  - `azmcp-keyvault-certificate-create` - Create a new certificate
  - `azmcp-keyvault-secret-list` - List secrets in a key vault
  - `azmcp-keyvault-secret-create` - Create a new secret
- Added support for Azure Workbooks management operations: [[#629](https://github.com/Azure/azure-mcp/pull/629)]
  - `azmcp-workbooks-list` - List workbooks in a resource group with optional filtering
  - `azmcp-workbooks-show` - Get detailed information about a specific workbook
  - `azmcp-workbooks-create` - Create new workbooks with custom visualizations and content
  - `azmcp-workbooks-update` - Update existing workbook configurations and metadata
  - `azmcp-workbooks-delete` - Delete workbooks when no longer needed
- Added support for creating a directory in Azure Storage DataLake via the `azmcp-storage-datalake-directory-create` command. [[#647](https://github.com/Azure/azure-mcp/pull/647)]
- Added support for getting the details of an Azure Kubernetes Service (AKS) cluster via the `azmcp-aks-cluster-get` command. [[#700](https://github.com/Azure/azure-mcp/pull/700)]

### Breaking Changes

- Changed the default startup mode to list tools at the namespace level instead of at an individual level, reducing total tool count from around 128 tools to 25. Use `--mode all` to restore the previous behavior of exposing all tools individually. [[#689](https://github.com/Azure/azure-mcp/issues/689)]
- Consolidated Azure best practices commands into the command `azmcp-bestpractices-get` with `--resource` and `--action` parameters: [[#677](https://github.com/Azure/azure-mcp/pull/677)]
  - Removed `azmcp-bestpractices-general-get`, `azmcp-bestpractices-azurefunctions-get-code-generation` and `azmcp-bestpractices-azurefunctions-get-deployment`
  - Use `--resource general --action code-generation` for general Azure code generation best practices
  - Use `--resource general --action deployment` for general Azure deployment best practices
  - Use `--resource azurefunctions --action code-generation` instead of the old azurefunctions code-generation command
  - Use `--resource azurefunctions --action deployment` instead of the old azurefunctions deployment command
  - Use `--resource static-web-app --action all` to get Static Web Apps development and deployment best practices

### Bugs Fixed

- Fixes tool discovery race condition causing "tool not found" errors in MCP clients that use different processes to start and use the server, like LangGraph. [[#556](https://github.com/Azure/azure-mcp/issues/556)]

## 0.4.1 (2025-07-17)

### Features Added

- Added support for the following Azure Load Testing operations: [[#315](https://github.com/Azure/azure-mcp/pull/315)]
  - `azmcp-loadtesting-testresource-list` - List Azure Load testing resources.
  - `azmcp-loadtesting-testresource-create` - Create a new Azure Load testing resource.
  - `azmcp-loadtesting-test-get` - Get details of a specific load test configuration.
  - `azmcp-loadtesting-test-create` - Create a new load test configuration.
  - `azmcp-loadtesting-testrun-get` - Get details of a specific load test run.
  - `azmcp-loadtesting-testrun-list` - List all load test runs for a specific test.
  - `azmcp-loadtesting-testrun-create` - Create a new load test run.
  - `azmcp-loadtesting-testrun-delete` - Delete a specific load test run.
- Added support for scanning Azure resources for compliance recommendations using the Azure Quick Review CLI via the command: `azmcp-extension-azqr`. [[#510](https://github.com/Azure/azure-mcp/pull/510)]
- Added support for listing paths in Data Lake file systems via the command: `azmcp-storage-datalake-file-system-list-paths`. [[#608](https://github.com/Azure/azure-mcp/pull/608)]
- Added support for listing SQL elastic pools via the command: `azmcp-sql-elastic-pool-list`. [[#606](https://github.com/Azure/azure-mcp/pull/606)]
- Added support for listing SQL server firewall rules via the command: `azmcp-sql-firewall-rule-list`. [[#610](https://github.com/Azure/azure-mcp/pull/610)]
- Added new commands for obtaining Azure Functions best practices via the following commands: [[#630](https://github.com/Azure/azure-mcp/pull/630)]
  - `azmcp-bestpractices-azurefunctions-get-code-generation` - Get code generation best practices for Azure Functions.
  - `azmcp-bestpractices-azurefunctions-get-deployment` - Get deployment best practices for Azure Functions.
- Added support for get details about a product in the Azure Marketplace via the command: `azmcp-marketplace-product-get`. [[#442](https://github.com/Azure/azure-mcp/pull/442)]

### Breaking Changes

- Renamed the command `azmcp-bestpractices-get` to `azmcp-bestpractices-general-get`. [[#630](https://github.com/Azure/azure-mcp/pull/630)]

### Bugs Fixed

- Fixed an issue with Azure CLI executable path resolution on Windows. [[#611](https://github.com/Azure/azure-mcp/issues/611)]
- Fixed a tool discovery timing issue when calling tools on fresh server instances. [[#604](https://github.com/Azure/azure-mcp/issues/604)]
- Fixed issue where unrecognizable json would be sent to MCP clients in STDIO mode at startup. [[#644](https://github.com/Azure/azure-mcp/issues/644)]

### Other Changes

- Changed `engines.node` in `package.json` to require Node.js version `>=20.0.0`. [[#628](https://github.com/Azure/azure-mcp/pull/628)]

## 0.4.0 (2025-07-15)

### Features Added

- Added support for listing Azure Kubernetes Service (AKS) clusters via the command `azmcp-aks-cluster-list`. [[#560](https://github.com/Azure/azure-mcp/pull/560)]
- Made the following Ahead of Time (AOT) compilation improvements saving `6.96 MB` in size total:
  - Switched to the trimmer-friendly `CreateSlimBuilder` API from `CreateBuilder`, saving `0.63 MB` in size for the native executable. [[#564](https://github.com/Azure/azure-mcp/pull/564)]
  - Switched to the trimmer-friendly `npgsql` API, saving `2.69 MB` in size for the native executable. [[#592](https://github.com/Azure/azure-mcp/pull/592)]
  - Enabled `IlcFoldIdenticalMethodBodies` to fold identical method bodies, saving `3.64 MB` in size for the native executable. [[#598](https://github.com/Azure/azure-mcp/pull/598)]
- Added support for using the hyphen/dash ("-") character in command names. [[#531](https://github.com/Azure/azure-mcp/pull/531)]
- Added support for authenticating with the Azure account used to log into VS Code. Authentication now prioritizes the VS Code broker credential when in the context of VS Code. [[#452](https://github.com/Azure/azure-mcp/pull/452)]

### Breaking Changes

- Removed SSE (Server-Sent Events) transport support. Now, only stdio transport is supported as SSE is no longer part of the MCP specification. [[#593](https://github.com/Azure/azure-mcp/issues/593)]
- Renamed `azmcp-sql-server-entraadmin-list` to `azmcp-sql-server-entra-admin-list` for better readability. [[#602](https://github.com/Azure/azure-mcp/pull/602)]

### Bugs Fixed

- Added a post-install script to ensure platform-specific versions like `@azure/mcp-${platform}-${arch}` can be resolved. Otherwise, fail install to prevent npx caching of `@azure/mcp`. [[#597](https://github.com/Azure/azure-mcp/pull/597)]
- Improved install reliability and error handling when missing platform packages on Ubuntu. [[#394](https://github.com/Azure/azure-mcp/pull/394)]

### Other Changes
- Updated `engines.node` in `package.json` to require Node.js version `>=22.0.0`.

#### Dependency Updates

- Updated the `ModelContextProtocol.AspNetCore` version from `0.3.0-preview.1` to `0.3.0-preview.2`. [[#519](https://github.com/Azure/azure-mcp/pull/519)]

## 0.3.2 (2025-07-10)

### Features Added

- Added support for listing Azure Managed Grafana details via the command: `azmcp-grafana-list`. [[#532](https://github.com/Azure/azure-mcp/pull/532)]
- Added agent best practices for Azure Terraform commands. [[#420](https://github.com/Azure/azure-mcp/pull/420)]

### Bugs Fixed

- Fixed issue where trace logs could be collected as telemetry. [[#540](https://github.com/Azure/azure-mcp/pull/540/)]
- Fixed an issue that prevented the Azure MCP from finding the Azure CLI if it was installed on a path other than the default global one. [[#351](https://github.com/Azure/azure-mcp/issues/351)]

## 0.3.1 (2025-07-08)

### Features Added

- Added support for the following SQL operations:
  - `azmcp-sql-db-show` - Show details of a SQL Database [[#516](https://github.com/Azure/azure-mcp/pull/516)]
  - `azmcp-sql-server-entra-admin-list` - List Microsoft Entra ID administrators for a SQL server [[#529](https://github.com/Azure/azure-mcp/pull/529)]
- Updates Azure MCP tool loading configurations at launch time. [[#513](https://github.com/Azure/azure-mcp/pull/513)]

### Breaking Changes

- Deprecated the `--service` flag. Use `--namespace` and `--mode` options to specify the service and mode the server will run in. [[#513](https://github.com/Azure/azure-mcp/pull/513)]

## 0.3.0 (2025-07-03)

### Features Added

- Added support for Azure AI Foundry [[#274](https://github.com/Azure/azure-mcp/pull/274)]. The following tools are now available:
  - `azmcp-foundry-models-list`
  - `azmcp-foundry-models-deploy`
  - `azmcp-foundry-models-deployments-list`
- Added support for telemetry [[#386](https://github.com/Azure/azure-mcp/pull/386)]. Telemetry is enabled by default but can be disabled by setting `AZURE_MCP_COLLECT_TELEMETRY` to `false`.

### Bugs Fixed

- Fixed a bug where `CallToolResult` was always successful. [[#511](https://github.com/Azure/azure-mcp/pull/511)]

## 0.2.6 (2025-07-01)

### Other Changes

- Updated the descriptions of the following tools to improve their usage by Agents: [[#492](https://github.com/Azure/azure-mcp/pull/492)]
  - `azmcp-datadog-monitoredresources-list`
  - `azmcp-kusto-cluster-list`
  - `azmcp-kusto-database-list`
  - `azmcp-kusto-sample`
  - `azmcp-kusto-table-list`
  - `azmcp-kusto-table-schema`

## 0.2.5 (2025-06-26)

### Bugs Fixed

- Fixed issue where tool listing incorrectly returned resources instead of text. [#465](https://github.com/Azure/azure-mcp/issues/465)
- Fixed invalid modification to HttpClient in KustoClient. [#433](https://github.com/Azure/azure-mcp/issues/433)

## 0.2.4 (2025-06-24)

### Features Added

- Added new command for resource-centric logs query in Azure Monitor with command path `azmcp-monitor-resource-logs-query` - https://github.com/Azure/azure-mcp/pull/413
- Added support for starting the server with a subset of services using the `--service` flag - https://github.com/Azure/azure-mcp/pull/424
- Improved index schema handling in Azure AI Search (index descriptions, facetable fields, etc.) - https://github.com/Azure/azure-mcp/pull/440
- Added new commands for querying metrics with Azure Monitor with command paths `azmcp-monitor-metrics-query` and `azmcp-monitor-metrics-definitions`. - https://github.com/Azure/azure-mcp/pull/428

### Breaking Changes

- Changed the command for workspace-based logs query in Azure Monitor from `azmcp-monitor-log-query` to `azmcp-monitor-workspace-logs-query`

### Bugs Fixed

- Fixed handling of non-retrievable fields in Azure AI Search. [#416](https://github.com/Azure/azure-mcp/issues/416)

### Other Changes

- Repository structure changed to organize all of an Azure service's code into a single "area" folder. ([426](https://github.com/Azure/azure-mcp/pull/426))
- Upgraded Azure.Messaging.ServiceBus to 7.20.1 and Azure.Core to 1.46.2. ([441](https://github.com/Azure/azure-mcp/pull/441/))
- Updated to ModelContextProtocol 0.3.0-preview1, which brings support for the 06-18-2025 MCP specification. ([431](https://github.com/Azure/azure-mcp/pull/431))

## 0.2.3 (2025-06-19)

### Features Added

- Adds support to launch MCP server in readonly mode - https://github.com/Azure/azure-mcp/pull/410

### Bugs Fixed

- MCP tools now expose annotations to clients https://github.com/Azure/azure-mcp/pull/388

## 0.2.2 (2025-06-17)

### Features Added

- Support for Azure ISV Services https://github.com/Azure/azure-mcp/pull/199/
- Support for Azure RBAC https://github.com/Azure/azure-mcp/pull/266
- Support for Key Vault Secrets https://github.com/Azure/azure-mcp/pull/173


## 0.2.1 (2025-06-12)

### Bugs Fixed

- Fixed the issue where queries containing double quotes failed to execute. https://github.com/Azure/azure-mcp/pull/338
- Enables dynamic proxy mode within single "azure" tool. https://github.com/Azure/azure-mcp/pull/325

## 0.2.0 (2025-06-09)

### Features Added

- Support for launching smaller service level MCP servers. https://github.com/Azure/azure-mcp/pull/324

### Bugs Fixed

- Fixed failure starting Docker image. https://github.com/Azure/azure-mcp/pull/301

## 0.1.2 (2025-06-03)

### Bugs Fixed

- Monitor Query Logs Failing.  Fixed with https://github.com/Azure/azure-mcp/pull/280

## 0.1.1 (2025-05-30)

### Bugs Fixed

- Fixed return value of `tools/list` to use JSON object names. https://github.com/Azure/azure-mcp/pull/275

### Other Changes

- Update .NET SDK version to 9.0.300 https://github.com/Azure/azure-mcp/pull/278

## 0.1.0 (2025-05-28)

### Breaking Changes

- `azmcp tool list` "args" changes to "options"

### Other Changes

- Removed "Arguments" from code base in favor of "Options" to align with System. CommandLine semantics. https://github.com/Azure/azure-mcp/pull/232

## 0.0.21 (2025-05-22)

### Features Added

- Support for Azure Redis Caches and Clusters https://github.com/Azure/azure-mcp/pull/198
- Support for Azure Monitor Health Models https://github.com/Azure/azure-mcp/pull/208

### Bugs Fixed

- Updates the usage patterns of Azure Developer CLI (azd) when invoked from MCP. https://github.com/Azure/azure-mcp/pull/203
- Fixes server binding issue when using SSE transport in Docker by replacing `ListenLocalhost` with `ListenAnyIP`, allowing external access via port mapping. https://github.com/Azure/azure-mcp/pull/233

### Other Changes

- Updated to the latest ModelContextProtocol library. https://github.com/Azure/azure-mcp/pull/220

## 0.0.20 (2025-05-17)

### Bugs Fixed

- Improve the formatting in the ParseJsonOutput method and refactor it to utilize a ParseError record. https://github.com/Azure/azure-mcp/pull/218
- Added dummy argument for best practices tool, so the schema is properly generated for Python Open API use cases. https://github.com/Azure/azure-mcp/pull/219

## 0.0.19 (2025-05-15)

### Bugs Fixed

- Fixes Service Bus host name parameter description. https://github.com/Azure/azure-mcp/pull/209/

## 0.0.18 (2025-05-14)

### Bugs Fixed

- Include option to exclude managed keys. https://github.com/Azure/azure-mcp/pull/202

## 0.0.17 (2025-05-13)

### Bugs Fixed

- Added an opt-in timeout for browser-based authentication to handle cases where the process waits indefinitely if the user closes the browser. https://github.com/Azure/azure-mcp/pull/189

## 0.0.16 (2025-05-13)

### Bugs Fixed

- Fixed being able to pass args containing spaces through an npx call to the cli

### Other Changes

- Updated to the latest ModelContextProtocol library. https://github.com/Azure/azure-mcp/pull/161

## 0.0.15 (2025-05-09)

### Features Added

- Support for getting properties and runtime information for Azure Service Bus queues, topics, and subscriptions. https://github.com/Azure/azure-mcp/pull/150/
- Support for peeking at Azure Service Bus messages from queues or subscriptions. https://github.com/Azure/azure-mcp/pull/144
- Adds Best Practices tool that provides guidance to LLMs for effective code generation. https://github.com/Azure/azure-mcp/pull/153 https://github.com/Azure/azure-mcp/pull/156

### Other Changes

- Disabled Parallel testing in the ADO pipeline for Live Tests https://github.com/Azure/azure-mcp/pull/151

## 0.0.14 (2025-05-07)

### Features Added

- Support for Azure Key Vault keys https://github.com/Azure/azure-mcp/pull/119
- Support for Azure Data Explorer  https://github.com/Azure/azure-mcp/pull/21

## 0.0.13 (2025-05-06)

### Features Added

- Support for Azure PostgreSQL. https://github.com/Azure/azure-mcp/pull/81

## 0.0.12 (2025-05-05)

### Features Added

- Azure Search Tools https://github.com/Azure/azure-mcp/pull/83

### Other Changes

- Arguments no longer echoed in response: https://github.com/Azure/azure-mcp/pull/79
- Editorconfig and gitattributes updated: https://github.com/Azure/azure-mcp/pull/91

## 0.0.11 (2025-04-29)

### Features Added

### Breaking Changes

### Bugs Fixed
- Bug fixes to existing MCP commands
- See https://github.com/Azure/azure-mcp/releases/tag/0.0.11

### Other Changes

## 0.0.10 (2025-04-17)

### Features Added
- Support for Azure Cosmos DB (NoSQL databases).
- Support for Azure Storage.
- Support for Azure Monitor (Log Analytics).
- Support for Azure App Configuration.
- Support for Azure Resource Groups.
- Support for Azure CLI.
- Support for Azure Developer CLI (azd).

### Breaking Changes

### Bugs Fixed
- See https://github.com/Azure/azure-mcp/releases/tag/0.0.10

### Other Changes
- See blog post for details https://devblogs.microsoft.com/azure-sdk/introducing-the-azure-mcp-server/
