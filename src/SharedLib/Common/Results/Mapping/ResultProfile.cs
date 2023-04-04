using Ark.SharedLib.Common.Results.Extensions;
using AutoMapper;

namespace Ark.SharedLib.Common.Results.Mapping;

public class ResultProfile : Profile
{
    public ResultProfile()
    {
        CreateMap(typeof(Result<>), typeof(Result<>))
            .ConvertUsing(typeof(ResultToResultTypeConverter<,>));
        CreateMap(typeof(object), typeof(Result<>))
            .ConvertUsing(x => x);
        CreateMap(typeof(Result<>), typeof(object))
            .ConstructUsingServiceLocator()
            .ConvertUsing(typeof(ResultToObjectTypeConverter<>));
    }
}

public class ResultToObjectTypeConverter<TSource> : ITypeConverter<Result<TSource>, object>
{
    private readonly IMapper _mapper;

    public ResultToObjectTypeConverter(IMapper mapper)
    {
        _mapper = mapper;
    }

    #region ITypeConverter<Result<TSource>,object> Members

    public object Convert(Result<TSource> source, object destination, ResolutionContext context)
    {
        var dest = destination;
        return _mapper.Map(source.Value, destination);
    }

    #endregion
}

public class ObjectToResultTypeConverter<T> : ITypeConverter<T, Result<T>>
{
    private readonly IMapper _mapper;

    public ObjectToResultTypeConverter(IMapper mapper)
    {
        _mapper = mapper;
    }

    #region ITypeConverter<T,Result<T>> Members

    public Result<T> Convert(T source, Result<T> destination, ResolutionContext context)
    {
        if (source is null)
            throw new NullReferenceException(nameof(source));
        if (source.GetType() == typeof(Result<>))
            throw new NotImplementedException();
        return source;
    }

    #endregion
}

public class ResultToResultTypeConverter<TSource, TDestination>
    : ITypeConverter<Result<TSource>, Result<TDestination>>
{
    private readonly IMapper _mapper;

    public ResultToResultTypeConverter(IMapper mapper)
    {
        _mapper = mapper;
    }

    #region ITypeConverter<Result<TSource>,Result<TDestination>> Members

    public Result<TDestination> Convert(Result<TSource> source, Result<TDestination> destination,
        ResolutionContext context) =>
        // TODO recursion
        source.Map(destination);

    #endregion
}