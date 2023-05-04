using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using CloudMusic.API;
using SimpleJSON;
using System.IO;
using System.Net;

[System.Diagnostics.CodeAnalysis.SuppressMessage( "Style" , "IDE0090:使用 \"new(...)\"" , Justification = "<挂起>" )]
public class PortraitScreenCloudMusic :MonoBehaviour
{

    public static PortraitScreenCloudMusic Instance;
    #region public
    public InputField SearchInput;

    public Button SearchButton;

    public Transform CentreArea;

    public GameObject SearchSongPrefab;

    public Image SongCover;

    public Text Lyric;

    public Image PlaySchedule;

    public Button PlayOrPause;

    public Button Home;

    public Button FM;

    public Button NextFM;

    public AudioSource MusicPlay;

    public bool IsFMMode = false;

    [Header( "---------------------------" )]
    public Sprite PlaySprite;
    public Sprite PauseSprite;
    #endregion

    private const int CreationCount = 11;
    private Vector3 loactPost = new Vector3( 0 , 730 , 0 );
    private float m_TotalPlayMusicTimer;
    private int m_FMPlayIndex = 0;
    private float m_CurrentAngle = 0;
    #region 

    private List<RequestData> m_FMData = new List<RequestData>( );
    private List<SearchSongsObject> m_SearchObject = new List<SearchSongsObject>( );
    private string m_LyricData = string.Empty;
    #endregion


    private void Awake( )
    {
        Instance = this;
        MusicPlay = gameObject.AddComponent<AudioSource>( );
    }
    private void Start( )
    {
        InitButtonClick( );
        InitData( );
        NextFM.SetActive( IsFMMode );
    }
    private void Update( )
    {
        if( MusicPlay != null && MusicPlay.clip != null )
        {
            PlaySchedule.fillAmount = MusicPlay.time / m_TotalPlayMusicTimer;
            if( !MusicPlay.isPlaying && MusicPlay.time / m_TotalPlayMusicTimer == 0 )
            {

                MusicPlay.clip = null;
                if( IsFMMode )
                {
                    NextFMMusic( );
                }
                else
                {

                }
            }
            if( MusicPlay.isPlaying )
            {
                m_CurrentAngle -= Time.deltaTime * 20;
                if( m_CurrentAngle < -360 )
                {
                    m_CurrentAngle = 0;
                }
                else
                {
                    SongCover.transform.localEulerAngles = new Vector3( 0 , 0 , m_CurrentAngle );
                }
            }
        }
    }
    private void NextFMMusic( )
    {
        m_FMPlayIndex++;
        if( m_FMPlayIndex >= m_FMData.Count )
        {
            CreateCloudRequet( CloudMusicAPI.RequestUrl + "/" + CloudMusicAPI.CloudMusicWebRequest.PersonslFM( ) , 10 , FMCallback );
        }
        else
        {
            PlayMusic( m_FMData[m_FMPlayIndex].ID );
        }
    }
    private void InitButtonClick( )
    {
        SearchButton.onClick.AddListener( ( ) =>
        {
            CreateCloudRequet( CloudMusicAPI.RequestUrl + "/" + CloudMusicAPI.CloudMusicWebRequest.SearchSong( SearchInput.text ) , 10 , SearchInputCallback );
        } );
        Home.onClick.AddListener( ( ) =>
        {
            IsFMMode = false;
            NextFM.SetActive( IsFMMode );
        } );
        PlayOrPause.onClick.AddListener( ( ) =>
        {
            if( MusicPlay.clip != null )
            {
                if( MusicPlay.isPlaying )
                {
                    MusicPlay.Pause( );
                    PlayOrPause.image.sprite = PlaySprite;
                }
                else
                {
                    MusicPlay.Play( );
                    PlayOrPause.image.sprite = PauseSprite;
                }
            }
        } );
        FM.onClick.AddListener( ( ) =>
        {
            if( IsFMMode )
            {
                return;
            }
            IsFMMode = true;
            NextFM.SetActive( IsFMMode );
            m_FMData.Clear( );
            m_FMPlayIndex = 0;
            CreateCloudRequet( CloudMusicAPI.RequestUrl + "/" + CloudMusicAPI.CloudMusicWebRequest.PersonslFM( ) , 10 , FMCallback );
        } );
        NextFM.onClick.AddListener( ( ) =>
        {
            NextFMMusic( );
        } );
    }

    private void InitData( )
    {
        for( int i = 0 ; i < CreationCount ; i++ )
        {
            GameObject item = GameObject.Instantiate( SearchSongPrefab );
            item.transform.SetParent( CentreArea , false );
            item.transform.localPosition = loactPost;
            loactPost = new Vector3( 0 , loactPost.y - 130 , 0 );
            item.SetActive( false );
            m_SearchObject.Add( new SearchSongsObject( item ) );
        }
    }

    private void UpdateLyric( string json )
    {
        m_LyricData = json;
    }

    private void UpdateAlbum( Sprite sprite )
    {
        SongCover.sprite = sprite;
    }

    public void PlayMusic( int id )
    {
        CreateCloudRequet( $"https://api-unm.imsyy.top/match?id={id}&server=qq,pyncmd,kugou" , 10 , ( data ) =>
        {
            JSONNode json = JSON.Parse( data.text );
            string url = json["data"]["url"];
            if( url.Contains( ".m4a" ) )
            {
                NextFMMusic( );
                return;
            }
            SearchLyric( id );
            AlbumSprite( id );
            CreateMusicRequest( url , ( data ) =>
            {
                MusicPlay.clip = data;
                MusicPlay.Play( );
                m_TotalPlayMusicTimer = MusicPlay.clip.length;
                PlayOrPause.image.sprite = PauseSprite;
            } );
        } );


        //return;
        //CreateCloudRequet( $"{CloudMusicAPI.RequestUrl}/{CloudMusicAPI.CloudMusicWebRequest.GetRequestMP3URL( id , CloudMusicAPI.CloudMusicWebRequest.Level.hires )}" , 10 , ( data ) =>
        //{
        //    JSONNode json = JSON.Parse( data.text );
        //    string url = json["data"][0]["url"];
        //    CreateMusicRequest( url , ( data ) =>
        //    {
        //        MusicPlay.clip = data;
        //        MusicPlay.Play( );
        //    } );
        //} );
    }

    private void SearchLyric( int id )
    {
        CreateCloudRequet( $"http://music.163.com/api/song/media?id={id}" , 10 , ( data ) => { UpdateLyric( data.text ); } );
    }

    private void AlbumSprite( int id )
    {
        CreateCloudRequet( $"http://music.163.com/api/song/detail/?id={id}&ids=[{id}]" , 10 , ( data ) =>
        {
            JSONNode json = JSON.Parse( data.text );
            Instance.CreateTextureRequet( json["songs"][0]["album"]["blurPicUrl"] , ( data ) => UpdateAlbum( data.Texture2DToSprite( ) ) );
        } );
    }

    #region callback

    private void SearchInputCallback( DownloadHandler data )
    {
        List<RequestData> tempData = new List<RequestData>( );
        JSONNode json = JSON.Parse( data.text );
        for( int i = 0 ; i < json["result"]["songs"].Count ; i++ )
        {
            RequestData temp = new RequestData( )
            {
                ID = json["result"]["songs"][i]["id"] ,
                name = json["result"]["songs"][i]["name"] ,
                data = json["result"]["songs"][i]["artists"][0]["name"]
            };
            tempData.Add( temp );
        }
        int count = tempData.Count >= m_SearchObject.Count ? m_SearchObject.Count : tempData.Count;
        for( int i = 0 ; i < count ; i++ )
        {
            m_SearchObject[i].UpdateShowData( tempData[i].ID , tempData[i].name , tempData[i].data );
        }
    }

    private void FMCallback( DownloadHandler data )
    {
        JSONNode songData = JSON.Parse( data.text );
        for( int i = 0 ; i < songData["data"].Count ; i++ )
        {
            RequestData temp = new RequestData
            {
                name = songData["data"][i]["name"] ,
                ID = songData["data"][i]["id"] ,
                data = songData["data"][i]["album"]["blurPicUrl"]
            };
            m_FMData.Add( temp );
        }
        PlayMusic( m_FMData[m_FMPlayIndex].ID );
    }
    #endregion

    #region Http
    public void CreateCloudRequet( string url , int awaitTime = 10 , Action<DownloadHandler> successCallback = null , Action<string> failedCallback = null )
    {
        StartCoroutine( HttpRequetData( url , awaitTime , successCallback , failedCallback ) );
    }
    public void CreateTextureRequet( string url , Action<Texture2D> successCallback = null , Action<string> failedCallback = null )
    {
        StartCoroutine( HttpRequetTexture( url , successCallback , failedCallback ) );
    }

    public void CreateMusicRequest( string url , Action<AudioClip> successCallback = null , Action<string> failedCallback = null , AudioType type = AudioType.MPEG )
    {
        StartCoroutine( HttpRequetMusic( url , successCallback , failedCallback , type ) );
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

    private IEnumerator HttpRequetMusicBytes( string url )
    {
        UnityWebRequest webRequest = UnityWebRequest.Get( url );
        webRequest.timeout = 30;//设置超时，若webRequest.SendWebRequest()连接超时会返回，且isNetworkError为true
        yield return webRequest.SendWebRequest( );

        if( webRequest.result == UnityWebRequest.Result.ConnectionError )
        {
            Debug.Log( "Download Error:" + webRequest.error );
        }
        else
        {
            //获取二进制数据

            var File = webRequest.downloadHandler.data;
           
        }
    }

    private IEnumerator HttpRequetMusic( string url , Action<AudioClip> successCallback , Action<string> failedCallback , AudioType type = AudioType.MPEG )
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip( url , type );
        yield return request.SendWebRequest( );
        if( request.result == UnityWebRequest.Result.Success )
        {
            try
            {
                successCallback?.Invoke( DownloadHandlerAudioClip.GetContent( request ) );
            }
            catch( Exception e )
            {
                Debug.LogError( e.Message + "当前url-》" + url );
            }
        }
        else
        {
            failedCallback.Invoke( request.error );
        }
    }

    #endregion

    #region Request Data
    public class RequestData
    {
        public int ID;

        public string name;

        public string data;
    }
    #endregion

    ///<summary>搜索物体</summary>
    public class SearchSongsObject
    {
        public SearchSongsObject( GameObject data )
        {
            trans = data;
            Play = data.transform.FindTransform( "Play" ).GetComponent<Button>( );
            songName = data.transform.FindTransform( "SongName" ).GetComponent<Text>( );
            artistNaem = data.transform.FindTransform( "Name" ).GetComponent<Text>( );
            ID = -1;
            Play.onClick.AddListener( Click );
        }
        private readonly GameObject trans;
        public Button Play;

        public Text songName;

        public Text artistNaem;

        public int ID;

        public void UpdateShowData( int id , string name , string artist )
        {
            ID = id;
            songName.text = name;
            artistNaem.text = artist;
            trans.SetActive( true );
        }

        private void Click( )
        {
            Instance.PlayMusic( ID );
        }
    }
}
