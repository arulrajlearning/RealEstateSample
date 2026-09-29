using System.Text.Json;

namespace ZipListing;

public sealed class ListingJsonLoader
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);
    private readonly string _filePath;

    public ListingJsonLoader(IHostEnvironment hostEnvironment)
    {
        _filePath = Path.Combine(hostEnvironment.ContentRootPath, "sample_listings.json");
    }

    public async Task<IReadOnlyList<Listing>> LoadAsync(CancellationToken cancellationToken = default)
    {
        await using var stream = File.OpenRead(_filePath);
        var listings = await JsonSerializer.DeserializeAsync<List<Listing>>(stream, SerializerOptions, cancellationToken);
        return listings ?? [];
    }
}