#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

/// <summary>
/// 资源加载
/// </summary>
public class GameLoadAsset
{
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
#endif
        temp.name = objtName;
        temp.transform.SetParent( parent , false );
        return temp;
    }

}