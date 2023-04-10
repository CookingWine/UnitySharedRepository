using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/4/10 22:52:09
public partial class CloundMusicDown
{

	private Button m_Btn_SongsIcon;
	private Image m_Img_SongsIcon;
	private Text m_Txt_SongsName;
	private Text m_Txt_ArtistsName;
	private AudioSource m_Source_CloundMusic;
	private Image m_Img_ProgressBar;
	private Text m_Txt_CurrentTimer;
	private Text m_Txt_TotalTimer;
	private Button m_Btn_LastSongs;
	private Image m_Img_PlayOrPauser;
	private Button m_Btn_PlayOrPauser;
	private Button m_Btn_NextSongs;
	private Image m_Img_Volume;
	private Button m_Btn_Volume;
	private Button m_Btn_PlayList;
	private Slider m_Slider_Volume;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Btn_SongsIcon = autoBindTool.GetBindComponent<Button>(0);
		m_Img_SongsIcon = autoBindTool.GetBindComponent<Image>(1);
		m_Txt_SongsName = autoBindTool.GetBindComponent<Text>(2);
		m_Txt_ArtistsName = autoBindTool.GetBindComponent<Text>(3);
		m_Source_CloundMusic = autoBindTool.GetBindComponent<AudioSource>(4);
		m_Img_ProgressBar = autoBindTool.GetBindComponent<Image>(5);
		m_Txt_CurrentTimer = autoBindTool.GetBindComponent<Text>(6);
		m_Txt_TotalTimer = autoBindTool.GetBindComponent<Text>(7);
		m_Btn_LastSongs = autoBindTool.GetBindComponent<Button>(8);
		m_Img_PlayOrPauser = autoBindTool.GetBindComponent<Image>(9);
		m_Btn_PlayOrPauser = autoBindTool.GetBindComponent<Button>(10);
		m_Btn_NextSongs = autoBindTool.GetBindComponent<Button>(11);
		m_Img_Volume = autoBindTool.GetBindComponent<Image>(12);
		m_Btn_Volume = autoBindTool.GetBindComponent<Button>(13);
		m_Btn_PlayList = autoBindTool.GetBindComponent<Button>(14);
		m_Slider_Volume = autoBindTool.GetBindComponent<Slider>(15);
	}
}
