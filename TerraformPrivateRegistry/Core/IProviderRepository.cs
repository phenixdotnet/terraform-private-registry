using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TerraformPrivateRegistry.Models;

namespace TerraformPrivateRegistry.Core
{
    public interface IProviderRepository
    {
        Task<IEnumerable<TerraformVersion>> GetVersionsAsync(string ns, string type, CancellationToken cancellationToken);

        Task<TerraformDownloadDetail?> GetDownloadDetailsAsync(string ns, string type, string version, string os, string arch, CancellationToken cancellationToken);
    }
}
