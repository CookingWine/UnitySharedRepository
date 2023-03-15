using System;

/// <summary>
/// <see cref="SingletonException"/>表示在单例执行期间发生的错误
/// </summary>
public class SingletonException :Exception
{
    /// <summary>
    /// 初始化的一个新实例。<see cref="SingletonException"/>带有指定的错误消息
    /// </summary>
    /// <param name="message">描述错误的消息</param>
    public SingletonException( string message ) : base( message )
    {

    }
}