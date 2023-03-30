using UnityEngine;

public partial class MusicLogin :MonoBehaviour
{
    private void Awake( )
    {
        InitBindComponent( gameObject );
    }

    private void Start( )
    {
        m_Btn_SwitchoverLogin.onClick.AddListener( ( ) =>
        {

        } );
    }

    private void Update( )
    {
        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            Debug.Log( m_TInput_PhoneNumber.text );
        }
    }
}
