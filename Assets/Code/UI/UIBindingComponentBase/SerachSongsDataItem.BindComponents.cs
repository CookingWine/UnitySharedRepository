using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/4/10 17:02:38
public partial class SerachSongsDataItem
{

	private Button m_Btn_PlayMusic;
	private Text m_Txt_MusicName;
	private Text m_Txt_ArtistsName;
	private Text m_Txt_AlbumInfo;
	private Image m_Img_Heat;
	private Text m_Txt_PlayTotalTime;
	private Button m_Btn_AddMusicToList;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Btn_PlayMusic = autoBindTool.GetBindComponent<Button>(0);
		m_Txt_MusicName = autoBindTool.GetBindComponent<Text>(1);
		m_Txt_ArtistsName = autoBindTool.GetBindComponent<Text>(2);
		m_Txt_AlbumInfo = autoBindTool.GetBindComponent<Text>(3);
		m_Img_Heat = autoBindTool.GetBindComponent<Image>(4);
		m_Txt_PlayTotalTime = autoBindTool.GetBindComponent<Text>(5);
		m_Btn_AddMusicToList = autoBindTool.GetBindComponent<Button>(6);
	}
}
