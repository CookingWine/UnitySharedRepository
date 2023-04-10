using UnityEngine;
using UnityEngine.UI;

//自动生成于:2023/4/10 22:42:26
public partial class LyricsPortray :MonoBehaviour
{
    public float Angle;

    private float m_CurrentAngle = 0f;


    private void Awake( )
    {
        InitBindComponent( gameObject );
    }

    public LyricsPortray UpdateLyricsInfo( Sprite sprite )
    {
        m_Img_Picture.sprite = sprite;
        return this;
    }

    private void Update( )
    {
        if( CloundMusicInterface.Instance.CloundMusicDownData.IsPlay )
        {
            m_CurrentAngle -= ( Time.deltaTime * Angle );
            if( m_CurrentAngle < -360.0f )
            {
                m_CurrentAngle = 0f;
            }
            m_Img_Picture.transform.localEulerAngles = new Vector3( 0 , 0 , m_CurrentAngle );
        }
    }
}
