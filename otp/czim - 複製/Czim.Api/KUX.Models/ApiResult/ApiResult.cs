namespace KUX.Models.ApiResultManage;

/// <summary>
/// Api 消息返回类
/// </summary>
public class ApiResult
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="code">返回编码</param>
    /// <param name="message">返回消息</param>
    /// <param name="data">返回数据</param>
    public ApiResult(int code, string message, object data)
    {
        Code = code;
        Message = message;
        Data = data;
    }

    /// <summary>
    /// 接口编码
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 数据类型
    /// </summary>
    public object Data { get; set; }

    #region result

    /// <summary>
    /// 返回消息
    /// </summary>
    /// <param name="apiResultCodeEnum"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ApiResult ResultMessage(ApiResultCodeEnum apiResultCodeEnum, string message)
        => new ApiResult((int)apiResultCodeEnum, message, null);

    /// <summary>
    /// 返回数据
    /// </summary>
    /// <param name="apiResultCodeEnum"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult ResultData(ApiResultCodeEnum apiResultCodeEnum, object data)
        => new ApiResult((int)apiResultCodeEnum, null, data);

    /// <summary>
    /// 可返回消息和数据
    /// </summary>
    /// <param name="apiResultCodeEnum"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult Result(ApiResultCodeEnum apiResultCodeEnum, string message, object data)
        => new ApiResult((int)apiResultCodeEnum, message, data);

    #endregion result

    #region result code 可传入 int

    /// <summary>
    /// 返回消息
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ApiResult ResultMessage(int code, string message)
        => new ApiResult(code, message, null);

    /// <summary>
    /// 返回数据
    /// </summary>
    /// <param name="code"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult ResultData(int code, object data)
        => new ApiResult(code, null, data);

    /// <summary>
    /// 可返回消息和数据
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult Result(int code, string message, object data)
        => new ApiResult(code, message, data);

    #endregion result code 可传入 int

    #region Ok

    /// <summary>
    /// 成功 可返回消息
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ApiResult OkMessage(string message)
        => ResultMessage(ApiResultCodeEnum.Ok, message);

    /// <summary>
    /// 成功 可返回数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult OkData(object data)
        => ResultData(ApiResultCodeEnum.Ok, data);

    /// <summary>
    /// 成功 可返回 消息和数据
    /// </summary>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult Ok(string message, object data)
        => Result(ApiResultCodeEnum.Ok, message, data);

    #endregion Ok

    #region warn

    /// <summary>
    /// 警告 可返回消息
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ApiResult WarnMessage(string message)
        => ResultMessage(ApiResultCodeEnum.Warn, message);

    /// <summary>
    /// 警告 可返回数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult WarnData(object data)
        => ResultData(ApiResultCodeEnum.Warn, data);

    /// <summary>
    /// 警告 可返回 消息和数据
    /// </summary>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static ApiResult Warn(string message, object data)
        => Result(ApiResultCodeEnum.Warn, message, data);

    #endregion warn
}

