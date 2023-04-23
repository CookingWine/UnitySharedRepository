using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequest :MonoBehaviour
{
    private static HttpRequest m_Instance;

    public static HttpRequest Instance
    {
        get
        {
            if( m_Instance == null )
            {
                Debug.LogError( "HttpRequest还未创建" );
                return null;
            }
            return m_Instance;
        }
    }

    public string RequestUrl
    {
        get { return CloudMusic.API.CloudMusicAPI.RequestUrl; }
    }

    private void Awake( )
    {
        m_Instance = this;
    }


    public void CreateCloudLoginRequet( HttpRequestProvider data , int awaitTime = 10 , Action<DownloadHandler> successCallback = null , Action<string> failedCallback = null )
    {
        string body = HttpRequestBody.TGetBodyName( data );
        string url = CloudMusic.API.CloudMusicAPI.RequestUrl + "/" + body;
        if( !data.GetUrl( ).IsNullOrEmpty( ) )
        {
            url += $"{data.GetUrl( )}";
        }
        StartCoroutine( HttpRequetData( url , awaitTime , successCallback , failedCallback ) );
    }

    public string GetBodyName( HttpRequestProvider data , out string getUrl )
    {
        string body = HttpRequestBody.TGetBodyName( data );
        getUrl = string.Empty;
        if( !data.GetUrl( ).IsNullOrEmpty( ) )
        {
            getUrl = data.GetUrl( );
        }
        return body;
    }

    /// <summary>
    /// 创建一个请求
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="awaitTime">等待时间</param>
    /// <param name="successCallback">请求成功事件</param>
    /// <param name="failedCallback">请求失败事件</param>
    public void CreateCloudRequet( string url , int awaitTime = 10 , Action<DownloadHandler> successCallback = null , Action<string> failedCallback = null )
    {
        StartCoroutine( HttpRequetData( url , awaitTime , successCallback , failedCallback ) );
    }

    public void CreateTextureRequet( string url , Action<Texture2D> successCallback = null , Action<string> failedCallback = null )
    {
        StartCoroutine( HttpRequetTexture( url , successCallback , failedCallback ) );
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Style" , "IDE0090:使用 \"new(...)\"" , Justification = "<挂起>" )]
    private IEnumerator HttpRequetData( string url , int awaitTime , Action<DownloadHandler> successCallback , Action<string> failedCallback )
    {
        using UnityWebRequest request = new UnityWebRequest( url );
        request.timeout = awaitTime;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            successCallback?.Invoke( request.downloadHandler );
        }
        else
        {
            Debug.LogError( $"请求得url为->{url}\n异常错误->\n{request.error}" );
            failedCallback?.Invoke( request.error );
        }
    }
    private IEnumerator HttpRequetTexture( string url , Action<Texture2D> successCallback , Action<string> failedCallback )
    {
        using UnityWebRequest request = UnityWebRequestTexture.GetTexture( url );
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            Texture2D texture = DownloadHandlerTexture.GetContent( request );
            successCallback?.Invoke( texture );
        }
        else
        {
            failedCallback.Invoke( request.error );
        }
    }
}
