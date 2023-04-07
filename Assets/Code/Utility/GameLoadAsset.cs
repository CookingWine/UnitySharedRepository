#if UNITY_EDITOR
using System.Collections.Generic;
using System;
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 资源加载
/// </summary>
public class GameLoadAsset
{
    /// <summary>
    /// UI预制体的json文件
    /// </summary>
    private readonly string m_UIPrefabPath = Application.dataPath + "/Res/Confing/UIPrefabData.json";
    private readonly Dictionary<string , string> m_UIPrefab = new Dictionary<string , string>( );
    public GameLoadAsset( )
    {
        UIPrefabData data = JsonExtend.AnalysisJson<UIPrefabData>( m_UIPrefabPath );
        for( int i = 0 ; i < data.UIPrefabDatas.Count ; i++ )
        {
            m_UIPrefab.Add( data.UIPrefabDatas[i].Key , data.UIPrefabDatas[i].UIPrefabPath );
        }
    }

    private AssetBundle m_AssetBundle;

    /// <summary>
    /// 是否启动bundle模式
    /// </summary>
    public bool EnableBundle { get; private set; } = false;

    public void EditorEnableBunle( bool value )
    {
        EnableBundle = value;
    }
    public void InitGameAssetBundle( string bundleFile )
    {
        m_AssetBundle = AssetBundle.LoadFromFile( bundleFile );
    }
    public GameObject LoadPrefabAsset( string objtName )
    {
        GameObject temp = null;
#if UNITY_EDITOR
        if( EnableBundle )
        {
            temp = Object.Instantiate( m_AssetBundle.LoadAsset<GameObject>( objtName ) );
        }
        else
        {
            temp = Object.Instantiate( (GameObject)AssetDatabase.LoadAssetAtPath( $"Assets/Res/UI/UIPrefab/{objtName}.prefab" , typeof( GameObject ) ) );
        }
#else
            temp = Object.Instantiate( m_AssetBundle.LoadAsset<GameObject>( objtName ) );
#endif
        temp.name = objtName;
        return temp;
    }

    public GameObject LoadPrefabAsset( string objtName , Transform parent )
    {
        Debug.Log( $"开始加载资源{objtName}" );
        GameObject temp = null;
#if UNITY_EDITOR
        if( EnableBundle )
        {
            temp = Object.Instantiate( m_AssetBundle.LoadAsset<GameObject>( objtName ) );
        }
        else
        {
            if( m_UIPrefab.TryGetValue( objtName , out string value ) )
            {
                temp = Object.Instantiate( (GameObject)AssetDatabase.LoadAssetAtPath( value , typeof( GameObject ) ) );
            }
            else
            {
                Debug.LogError( $"当前资源列表内没有该资源{objtName}" );
            }
        }
#endif
        temp.name = objtName;
        temp.transform.SetParent( parent , false );
        return temp;
    }

}



[Serializable]
public class UIPrefabData
{
    public List<UpdateUIPrefabData> UIPrefabDatas;
    public UIPrefabData( )
    {
        UIPrefabDatas = new List<UpdateUIPrefabData>( );
    }
}

[Serializable]
public class UpdateUIPrefabData
{
    public string Key;

    public string UIPrefabPath;

    public string PrefabResPath;

    public UpdateUIPrefabData( string key , string prefab , string res )
    {
        Key = key;
        UIPrefabPath = prefab;
        PrefabResPath = res;
    }
}