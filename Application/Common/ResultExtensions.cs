namespace FiapCloudGames.Application.Common;

public static class ResultExtensions
{
    public static IResult ToOkResult<T>(this T data)
    {
        return Results.Ok(ResultData<T>.Success(data));
    }

    public static IResult ToCreatedResult<T>(this T data, string path)
    {
        return Results.Created(path, ResultData<T>.Success(data));
    }

    public static IResult ToBadRequest<T>(this T data, string errorMessage)
    {
        return Results.NotFound(ResultData<T>.Error(errorMessage));
    }
}
