using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/4/11 9:56:33
public partial class PlayListItem
{

	private Button m_Btn_Play;
	private Text m_Txt_SongsName;
	private Text m_Txt_ArtistsName;
	private Button m_Btn_Clear;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Btn_Play = autoBindTool.GetBindComponent<Button>(0);
		m_Txt_SongsName = autoBindTool.GetBindComponent<Text>(1);
		m_Txt_ArtistsName = autoBindTool.GetBindComponent<Text>(2);
		m_Btn_Clear = autoBindTool.GetBindComponent<Button>(3);
	}
}
