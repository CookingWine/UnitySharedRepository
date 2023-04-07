using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

//自动生成于:2023/4/7 10:44:07
public partial class LyricsInterface :MonoBehaviour
{
    public float Angle;
    private float m_CurrentAngle = 0f;
    private void Awake( )
    {
        InitBindComponent( gameObject );
        InitGameObjectInfo( );
    }

    private void InitGameObjectInfo( )
    {
        m_Btn_Switch.onClick.AddListener( ( ) =>
        {
            Debug.Log( "请求歌曲评论" );
            string url = CloudMusicAPI.GetRequestSongsCommentData( MainInterface.Instance.SongsID );
            Debug.Log( url );
            StartCoroutine( RequestComment( url) );
        } );
    }

    private void OnEnable( )
    {
        Debug.Log( "更新数据" );
        m_Img_SongsIcon.sprite = MainInterface.Instance.SongsIcon;
    }

    private void Update( )
    {
        if( MainInterface.Instance.IsPlay )
        {
            m_CurrentAngle -= ( Time.deltaTime * Angle );
            if( m_CurrentAngle < -360 )
            {
                m_CurrentAngle = 0f;
            }
            m_Img_SongsIcon.transform.localEulerAngles = new Vector3( 0 , 0 , m_CurrentAngle );
        }
    }

    private IEnumerator RequestComment( string url )
    {
        using UnityWebRequest request = new UnityWebRequest( url );
        request.timeout = 10;
        DownloadHandlerBuffer data = new DownloadHandlerBuffer( );
        request.downloadHandler = data;
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            Debug.Log( request.downloadHandler.text );
        }
        else
        {
            Debug.LogError( "请求错误" );
        }
    }
}
