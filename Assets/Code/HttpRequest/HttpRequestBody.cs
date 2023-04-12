using System;

[AttributeUsage( AttributeTargets.All )]
public class HttpRequestBody :Attribute
{
    public string HttpBodyInfo { get; set; }
    public HttpRequestBody( string data )
    {
        HttpBodyInfo = data;
    }
    /// <summary>
    /// 获取包体名字
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string TGetBodyName<T>( T data ) where T : HttpRequestProvider
    {
        Type type = data.GetType( );
        object[] obj = type.GetCustomAttributes( true );
        for( int i = 0 ; i < obj.Length ; i++ )
        {
            if( obj[i] is HttpRequestBody info )
            {
                return info.HttpBodyInfo;
            }
        }
        return "Unknown";
    }
}

/// <summary>
/// http请求数据
/// </summary>
public class HttpRequestProvider
{
    public virtual string GetUrl( )
    {
        return string.Empty;
    }
}