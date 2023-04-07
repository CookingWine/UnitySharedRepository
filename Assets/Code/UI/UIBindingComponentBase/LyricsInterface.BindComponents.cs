using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/4/7 11:21:24
public partial class LyricsInterface
{

	private Image m_Img_SongsIcon;
	private Button m_Btn_Switch;
	private RectTransform m_Trans_Lyri;
	private RectTransform m_Trans_Comment;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Img_SongsIcon = autoBindTool.GetBindComponent<Image>(0);
		m_Btn_Switch = autoBindTool.GetBindComponent<Button>(1);
		m_Trans_Lyri = autoBindTool.GetBindComponent<RectTransform>(2);
		m_Trans_Comment = autoBindTool.GetBindComponent<RectTransform>(3);
	}
}
