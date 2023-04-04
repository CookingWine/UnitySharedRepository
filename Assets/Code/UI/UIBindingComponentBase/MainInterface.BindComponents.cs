using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/4/4 17:06:13
public partial class MainInterface
{

	private Image m_Img_CloudTitleBG;
	private Image m_Img_CloudMusicTitleBg;
	private TextMeshProUGUI m_TTxt_CloudMusicTitle;
	private TMP_InputField m_TInput_SearchSongs;
	private Button m_Btn_Search;
	private Image m_Img_Search;
	private Button m_Btn_UserInfo;
	private Image m_Img_UserInfo;
	private Button m_Btn_Lessen;
	private Button m_Btn_Magnify;
	private Button m_Btn_Closee;
	private RectTransform m_Trans_CentreObjectBG;
	private Button m_Btn_PlayerMusic;
	private TextMeshProUGUI m_TTxt_SongsDataSongsName;
	private TextMeshProUGUI m_TTxt_SongsDataLonghair;
	private AudioSource m_Source_DownLyric;
	private RectTransform m_Trans_LyricBG;
	private Image m_Img_SongsIcon;
	private TextMeshProUGUI m_TTxt_SongsName;
	private TextMeshProUGUI m_TTxt_Longhair;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Img_CloudTitleBG = autoBindTool.GetBindComponent<Image>(0);
		m_Img_CloudMusicTitleBg = autoBindTool.GetBindComponent<Image>(1);
		m_TTxt_CloudMusicTitle = autoBindTool.GetBindComponent<TextMeshProUGUI>(2);
		m_TInput_SearchSongs = autoBindTool.GetBindComponent<TMP_InputField>(3);
		m_Btn_Search = autoBindTool.GetBindComponent<Button>(4);
		m_Img_Search = autoBindTool.GetBindComponent<Image>(5);
		m_Btn_UserInfo = autoBindTool.GetBindComponent<Button>(6);
		m_Img_UserInfo = autoBindTool.GetBindComponent<Image>(7);
		m_Btn_Lessen = autoBindTool.GetBindComponent<Button>(8);
		m_Btn_Magnify = autoBindTool.GetBindComponent<Button>(9);
		m_Btn_Closee = autoBindTool.GetBindComponent<Button>(10);
		m_Trans_CentreObjectBG = autoBindTool.GetBindComponent<RectTransform>(11);
		m_Btn_PlayerMusic = autoBindTool.GetBindComponent<Button>(12);
		m_TTxt_SongsDataSongsName = autoBindTool.GetBindComponent<TextMeshProUGUI>(13);
		m_TTxt_SongsDataLonghair = autoBindTool.GetBindComponent<TextMeshProUGUI>(14);
		m_Source_DownLyric = autoBindTool.GetBindComponent<AudioSource>(15);
		m_Trans_LyricBG = autoBindTool.GetBindComponent<RectTransform>(16);
		m_Img_SongsIcon = autoBindTool.GetBindComponent<Image>(17);
		m_TTxt_SongsName = autoBindTool.GetBindComponent<TextMeshProUGUI>(18);
		m_TTxt_Longhair = autoBindTool.GetBindComponent<TextMeshProUGUI>(19);
	}
}
