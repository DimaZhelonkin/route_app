using System.Net.Http.Json;
using System.Text;

using Newtonsoft.Json;
using Ark.Infrastructure;
using Ark.Infrastructure.Extensions;
using Ark.Infrastructure.Helpers;
using Ark.Routing.Converters;
using Ark.Routing.HttpClients.VkClient.Models;
using Ark.SharedLib.Common.Results;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;

namespace Ark.Routing.HttpClients.VkClient;

public class VkRoutingClient : IVkRoutingClient, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<VkRoutingClient> _logger;
    private readonly IOptions<VkRoutingApiOptions> _options;
    private readonly string _apiKey;

    public VkRoutingClient(
        HttpClient httpClient,
        IOptions<VkRoutingApiOptions> options,
        ILogger<VkRoutingClient> logger
    )
    {
        _httpClient = httpClient;
        _options = options;
        _logger = logger;
        _apiKey = options.Value.ApiKey;
    }

    #region IVkRoutingClient Members

    public async Task<Result<DirectionsResponse>> DirectionsAsync(DirectionsRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            // var uri = new UriBuilder(_options.Value.Uri)
            // {
            //     Path = "directions",
            //     Query = $"api_key={_apiKey}"
            // };
            //todo закинуть в httpClient  - baseUrl,QueryParameters
            var baseDirectionUri = new StringBuilder(_options.Value.Uri).Append("directions").ToString();
            var body = new { };
            var query = new {api_key = _apiKey};
            var uriWithQuery = HttpHelper.CreateUri(query, baseDirectionUri).ToString();
            // var uriBuilder = new UriBuilder();
            // var content = JsonSerializer.Serialize(request, new JsonSerializerOptions()
            // {
            //     DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            //     Converters = {
            //         new JsonStringEnumConverter()
            //     }
            // });
            // var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            // var jsonContent = new ByteArrayContent(buffer);
            // // /directions
            var options = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Include
            };
            // options.Converters.Add(new StringEnumConverter());
            var json = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uriWithQuery, stringContent, cancellationToken);

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
            var data = JsonConvert.DeserializeObject<DirectionsResponse>(responseJson,new DirectionConverter());

            return Result.Success(data);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error getting something fun to say: {Error}", ex);
            throw;
        }
    }

    // public async Task<LineString> GetRoute(Point startPoint, Point finishPoint)
    // {
    //     var coordinates = new Coordinate[]
    //     {
    //         new(0, 0),
    //         new(0, 1)
    //     };
    //     var currentRoute = new LineString(coordinates);
    //     return currentRoute;
    // }

    #endregion

    public void Dispose() => _httpClient?.Dispose();
}