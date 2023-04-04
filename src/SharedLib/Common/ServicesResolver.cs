using Microsoft.Extensions.DependencyInjection;

namespace Ark.SharedLib.Common;

public static class ServicesProvider
{
    private static IServiceProvider? _serviceProvider;

    public static object? GetService(Type type)
    {
        if (_serviceProvider is null)
            throw new Exception("ServicesProvider is not initialized");
        return _serviceProvider.GetService(type);
    }

    public static T GetService<T>()
    {
        if (_serviceProvider is null)
            throw new Exception("ServicesProvider is not initialized");
        return _serviceProvider.GetService<T>();
    }

    public static object GetRequiredService(Type type)
    {
        if (_serviceProvider is null)
            throw new Exception("ServicesProvider is not initialized");
        return _serviceProvider.GetRequiredService(type);
    }

    public static T GetRequiredService<T>()
    {
        if (_serviceProvider is null)
            throw new Exception("ServicesProvider is not initialized");
        return _serviceProvider.GetRequiredService<T>();
    }

    public static IEnumerable<T> GetServices<T>()
    {
        if (_serviceProvider is null)
            throw new Exception("ServicesProvider is not initialized");
        return _serviceProvider.GetServices<T>();
    }

    public static void Init(IServiceProvider serviceProvider) => _serviceProvider ??= serviceProvider;
}