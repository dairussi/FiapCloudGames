namespace FiapCloudGames.Application.Common;

public abstract class Result
{

    protected Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
}
