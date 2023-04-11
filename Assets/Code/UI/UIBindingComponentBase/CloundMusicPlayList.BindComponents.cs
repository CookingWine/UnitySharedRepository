using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/4/11 10:50:04
public partial class CloundMusicPlayList
{

	private Text m_Txt_CurrentNumer;
	private Button m_Btn_ClearAll;
	private RectTransform m_Trans_PlayList;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Txt_CurrentNumer = autoBindTool.GetBindComponent<Text>(0);
		m_Btn_ClearAll = autoBindTool.GetBindComponent<Button>(1);
		m_Trans_PlayList = autoBindTool.GetBindComponent<RectTransform>(2);
	}
}
