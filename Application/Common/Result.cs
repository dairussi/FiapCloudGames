namespace FiapCloudGames.Application.Common;
//TODO separara as classes e criar as abstrações para usar o ResultData em applications e controllers   
public abstract class Result
{

    protected Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
    public class ResultData<T> : Result
    {
        public ResultData(T? data, bool isSuccess = true, string message = "") : base(isSuccess, message)
        {
            Data = data;
        }
        public T? Data { get; set; }
        public static ResultData<T> Success(T data) => new(data);
        public static ResultData<T> Error(string message) => new(default, false, message);
    }
}
