using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/4/6 18:29:15
public partial class SongsData
{

	private Button m_Btn_PlayerMusic;
	private Text m_Txt_SongsDataSongsName;
	private Text m_Txt_SongsDataLonghair;
	private Text m_Txt_Album;
	private Button m_Btn_AddPlayList;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Btn_PlayerMusic = autoBindTool.GetBindComponent<Button>(0);
		m_Txt_SongsDataSongsName = autoBindTool.GetBindComponent<Text>(1);
		m_Txt_SongsDataLonghair = autoBindTool.GetBindComponent<Text>(2);
		m_Txt_Album = autoBindTool.GetBindComponent<Text>(3);
		m_Btn_AddPlayList = autoBindTool.GetBindComponent<Button>(4);
	}
}
