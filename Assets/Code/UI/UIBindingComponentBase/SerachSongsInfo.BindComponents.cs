using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/4/10 18:06:40
public partial class SerachSongsInfo
{

	private Button m_Btn_CloseSerach;
	private RectTransform m_Trans_MiddleSongData;
	private Button m_Btn_Last;
	private Text m_Txt_Pagination;
	private Button m_Btn_Next;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Btn_CloseSerach = autoBindTool.GetBindComponent<Button>(0);
		m_Trans_MiddleSongData = autoBindTool.GetBindComponent<RectTransform>(1);
		m_Btn_Last = autoBindTool.GetBindComponent<Button>(2);
		m_Txt_Pagination = autoBindTool.GetBindComponent<Text>(3);
		m_Btn_Next = autoBindTool.GetBindComponent<Button>(4);
	}
}
