using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TerraformPrivateRegistry.Models
{
    public record TerraformPlatform(string OS, string Arch);
    public record TerraformVersion(string Version, IEnumerable<string> Protocols, IEnumerable<TerraformPlatform> Platforms);

    public record TerraformGpgSigningKey
    {
        public TerraformGpgSigningKey(string keyId, string asciiArmor, string trustSignature, string source, string sourceUrl)
            => (KeyId, AsciiArmor, TrustSignature, Source, SourceUrl) = (keyId, asciiArmor, trustSignature, source, sourceUrl);

        [JsonPropertyName("key_id")]
        public string KeyId { get; }
        [JsonPropertyName("ascii_armor")]
        public string AsciiArmor { get; }
        [JsonPropertyName("trust_signature")]
        public string TrustSignature { get; }
        public string Source { get; }
        [JsonPropertyName("source_url")]
        public string SourceUrl { get; }
    }
    public record TerraformSigningKeys
    {
        public TerraformSigningKeys(IEnumerable<TerraformGpgSigningKey> gpgPublicKeys)
            => (GpgPublicKeys) = (gpgPublicKeys);

        [JsonPropertyName("gpg_public_keys")]
        public IEnumerable<TerraformGpgSigningKey> GpgPublicKeys { get; }
    }

    public record TerraformDownloadDetail
    {
        public TerraformDownloadDetail(IEnumerable<string> protocols, string os, string arch, string filename, string downloadUrl, string shasumsUrl, string shasumsSignatureUrl, string shasum, TerraformSigningKeys signingKeys)
         => (Protocols, OS, Arch, Filename, DownloadUrl, ShasumsUrl, ShasumsSignatureUrl, Shasum, SigningKeys) = (protocols, os, arch, filename, downloadUrl, shasumsUrl, shasumsSignatureUrl, shasum, signingKeys);

        public IEnumerable<string> Protocols { get; }
        public string OS { get; }
        public string Arch { get; }
        public string Filename { get; }

        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; }
        [JsonPropertyName("shasums_url")]
        public string ShasumsUrl { get; }
        [JsonPropertyName("shasums_signature_url")]
        public string ShasumsSignatureUrl { get; }
        [JsonPropertyName("shasum")]
        public string Shasum { get; }
        [JsonPropertyName("signing_keys")]
        public TerraformSigningKeys SigningKeys { get; }
    }
}
