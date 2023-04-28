using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Music :MonoBehaviour
{

    public string URL;
    public AudioType type;
    private AudioSource musicSource;

    private void Start( )
    {
        if( musicSource == null )
        {
            musicSource = gameObject.AddComponent<AudioSource>( );
        }
    }
    private void Update( )
    {
        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            StartCoroutine( DownLoadMusic( URL , type ) );
        }

        if(Input.GetKeyDown( KeyCode.A ) )
        {
            StartCoroutine(DownLoad( URL ));
        }
    }

    private IEnumerator DownLoad(string url )
    {
        UnityWebRequest request=UnityWebRequest.Get( url ); 
        yield return request.SendWebRequest();
        if( request.result == UnityWebRequest.Result.Success )
        {
            FileExtend.CreationFileWirteData( "D:\\testQQMusic.m4a" , request.downloadHandler.text );
            Debug.LogError( "下载完成" );
        }
        else
        {
            Debug.LogError(request.error);
        }
    }


    private IEnumerator DownLoadMusic( string url , AudioType type = AudioType.MPEG )
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip( url , type );
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent( request );
            musicSource.clip = clip;
            musicSource.Play( );
        }
        else
        {
            Debug.LogError( request.error );
        }
    }

}
