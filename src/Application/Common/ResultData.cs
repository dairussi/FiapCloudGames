namespace FiapCloudGames.Application.Common;

public class ResultData<T> : Result
{
    public ResultData(T? data, bool isSuccess = true, string message = "") : base(isSuccess, message)
    {
        Data = data;
    }
    public T? Data { get; set; }
    public static ResultData<T> Success(T data) => new(data);
    public static ResultData<T> SuccessNoContent() => new(default);
    public static ResultData<T> Error(string message) => new(default, false, message);
}