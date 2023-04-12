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
            failedCallback?.Invoke( request.error );
        }
    }
}
