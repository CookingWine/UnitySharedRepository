using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/4/7 15:19:59
public partial class CloundMusicTop
{

	private Text m_Txt_MusicTitle;
	private InputField m_Input_SearchSongs;
	private Button m_Btn_SearchSongs;
	private Image m_Img_UserIcon;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Txt_MusicTitle = autoBindTool.GetBindComponent<Text>(0);
		m_Input_SearchSongs = autoBindTool.GetBindComponent<InputField>(1);
		m_Btn_SearchSongs = autoBindTool.GetBindComponent<Button>(2);
		m_Img_UserIcon = autoBindTool.GetBindComponent<Image>(3);
	}
}
