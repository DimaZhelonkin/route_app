using Microsoft.Extensions.Options;
using NetBox.Extensions;
using Storage.Net;
using Storage.Net.Blobs;

namespace Ark.Infrastructure.Storage;

public class FileStorageService : IFileStorageService
{
    private readonly string _basePath;
    private readonly IBlobStorage _storage;

    public FileStorageService(IOptions<FileStorageOptions> options)
    {
        _basePath = options.Value.BasePath;
        _storage = StorageFactory.Blobs.DirectoryFiles(_basePath);
    }

    #region IFileStorageService Members

    public async Task DeleteAsync(string filePath, CancellationToken cancellationToken = default) =>
        await _storage.DeleteAsync(filePath.AsEnumerable(), cancellationToken);

    public async Task<bool> ExistsAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var result = await _storage.ExistsAsync(filePath.AsEnumerable(), cancellationToken);
        return result.First();
    }

    public Task<Stream> OpenReadAsync(string filePath, CancellationToken cancellationToken = default) =>
        Task.FromResult<Stream>(new FileStream(filePath, FileMode.Open));

    public async Task WriteAsync(string filePath, Stream dataStream, bool overwrite = false,
        CancellationToken cancellationToken = default)
    {
        await using FileStream fs = new(filePath, FileMode.Create);
        await dataStream.CopyToAsync(fs, cancellationToken);
        dataStream.Close();
    }

    #endregion
}