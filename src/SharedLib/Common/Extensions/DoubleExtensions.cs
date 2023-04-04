namespace Ark.SharedLib.Common.Extensions;

public static class DoubleExtensions
{
    public static double ToMegabytes(this long bytes) => bytes / 1024f / 1024f;

    public static double ToMegabytes(this int bytes) => bytes / 1024f / 1024f;
}