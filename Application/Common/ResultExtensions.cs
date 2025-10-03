using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Application.Common;

public static class ResultExtensions
{
    public static IActionResult ToCreatedActionResult<T>(this T data, string path = "")
    {
        return new CreatedResult(path, ResultData<T>.Success(data));
    }

    public static IActionResult ToOkActionResult<T>(this T data)
    {
        return new OkObjectResult(ResultData<T>.Success(data));
    }

    public static IActionResult ToBadRequestActionResult<T>(this T data, string errorMessage)
    {
        return new BadRequestObjectResult(ResultData<T>.Error(errorMessage));
    }
}
