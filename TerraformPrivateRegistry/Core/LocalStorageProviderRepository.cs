using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TerraformPrivateRegistry.Models;

namespace TerraformPrivateRegistry.Core
{
    public class LocalStorageProviderRepository : IProviderRepository
    {
        private readonly ILogger<LocalStorageProviderRepository> logger;
        private readonly string basePath;

        public LocalStorageProviderRepository(string basePath, ILogger<LocalStorageProviderRepository> logger)
        {
            this.logger = logger;
            this.basePath = basePath;
        }

        public async Task<IEnumerable<TerraformVersion>> GetVersionsAsync(string ns, string type, CancellationToken cancellationToken)
        {
            string providerDirectory = Path.Combine(this.basePath, ns, type);
            string providerVersionsFile = Path.Combine(providerDirectory, "versions.json");
            this.logger.LogDebug("Looking in {0} for provider versions.json file");

            if (!Directory.Exists(providerDirectory))
            {
                this.logger.LogDebug("Directory {0} doesn't exists", providerDirectory);
                return new TerraformVersion[0];
            }
                

            if (cancellationToken.IsCancellationRequested)
                return new TerraformVersion[0];

            if (!File.Exists(providerVersionsFile))
            {
                this.logger.LogDebug("Unable to find the provider versions file {0}", providerVersionsFile);
                return new TerraformVersion[0];
            }

            var metadataContent = await File.ReadAllTextAsync(providerVersionsFile, cancellationToken).ConfigureAwait(false);
            var versions = JsonSerializer.Deserialize<IEnumerable<TerraformVersion>>(metadataContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (versions == null)
                return new TerraformVersion[0];
            else
                return versions;
        }

        public async Task<TerraformDownloadDetail?> GetDownloadDetailsAsync(string ns, string type, string version, string os, string arch, CancellationToken cancellationToken)
        {
            string providerVersionDirectory = Path.Combine(this.basePath, ns, type, version);
            this.logger.LogDebug("Looking in {0} for provider metadata.json file", providerVersionDirectory);

            if (!Directory.Exists(providerVersionDirectory))
            {
                this.logger.LogDebug("Directory {0} doesn't exists", providerVersionDirectory);
                return null;
            }

            string providerMetadataFile = Path.Combine(providerVersionDirectory, "metadata.json");

            if (cancellationToken.IsCancellationRequested)
                return null;

            if (!File.Exists(providerMetadataFile))
            {
                this.logger.LogDebug("Unable to find the provider metadata file {0}", providerMetadataFile);
                return null;
            }

            var metadataContent = await File.ReadAllTextAsync(providerMetadataFile, cancellationToken).ConfigureAwait(false);
            var versions = JsonSerializer.Deserialize<TerraformDownloadDetail>(metadataContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return versions;
        }
    }
}
