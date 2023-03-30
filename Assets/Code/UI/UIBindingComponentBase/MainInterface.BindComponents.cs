using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/3/30 15:55:21
public partial class MainInterface
{

	private Image m_Img_CloudTitleBG;
	private Image m_Img_CloudMusicTitleBg;
	private TextMeshProUGUI m_TTxt_CloudMusicTitle;
	private TMP_InputField m_TInput_SearchSongs;
	private Button m_Btn_UserInfo;
	private Image m_Img_UserInfo;
	private Button m_Btn_Lessen;
	private Button m_Btn_Magnify;
	private Button m_Btn_Closee;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Img_CloudTitleBG = autoBindTool.GetBindComponent<Image>(0);
		m_Img_CloudMusicTitleBg = autoBindTool.GetBindComponent<Image>(1);
		m_TTxt_CloudMusicTitle = autoBindTool.GetBindComponent<TextMeshProUGUI>(2);
		m_TInput_SearchSongs = autoBindTool.GetBindComponent<TMP_InputField>(3);
		m_Btn_UserInfo = autoBindTool.GetBindComponent<Button>(4);
		m_Img_UserInfo = autoBindTool.GetBindComponent<Image>(5);
		m_Btn_Lessen = autoBindTool.GetBindComponent<Button>(6);
		m_Btn_Magnify = autoBindTool.GetBindComponent<Button>(7);
		m_Btn_Closee = autoBindTool.GetBindComponent<Button>(8);
	}
}
