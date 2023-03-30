using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MainInterface : MonoBehaviour
{
    private void Awake( )
    {
        InitBindComponent( gameObject );
    }

    private void Start( )
    {
        m_TTxt_CloudMusicTitle.text = "网易云音乐";
    }
}
