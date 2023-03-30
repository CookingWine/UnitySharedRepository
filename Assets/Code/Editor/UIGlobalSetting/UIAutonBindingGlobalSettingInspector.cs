using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIAutonBindingGlobalSetting))]
public class UIAutonBindingGlobalSettingInspector :Editor
{
    private SerializedProperty m_Namespace;
    private SerializedProperty m_CodePath;
    private SerializedProperty m_MainCodePath;
    private void OnEnable( )
    {
        m_Namespace = serializedObject.FindProperty( "m_Namespace" );
        m_CodePath = serializedObject.FindProperty( "m_CodePath" );
        m_MainCodePath = serializedObject.FindProperty( "m_MainCodePath" );
    }

    public override void OnInspectorGUI( )
    {

        m_Namespace.stringValue = EditorGUILayout.TextField( new GUIContent( "默认命名空间" ) , m_Namespace.stringValue );

        EditorGUILayout.LabelField( "构建组件保存路径：" );
        EditorGUILayout.LabelField( m_CodePath.stringValue );
        if( GUILayout.Button( "选择路径" , GUILayout.Width( 140f ) ) )
        {
            m_CodePath.stringValue = EditorUtility.OpenFolderPanel( "选择代码保存路径" , Application.dataPath , "" );
        }
        EditorGUILayout.LabelField( "构建ui保存路径:" );
        EditorGUILayout.LabelField( m_MainCodePath.stringValue );
        if(GUILayout.Button("选择路径",GUILayout.Width(100f)))
        {
            m_MainCodePath.stringValue = EditorUtility.OpenFolderPanel( "选择代码保存路径" , Application.dataPath , "" );
        }
        serializedObject.ApplyModifiedProperties( );

    }
}