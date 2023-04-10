using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/4/10 22:42:26
public partial class LyricsPortray
{

	private Image m_Img_Picture;
	private Text m_Txt_Lyrics;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Img_Picture = autoBindTool.GetBindComponent<Image>(0);
		m_Txt_Lyrics = autoBindTool.GetBindComponent<Text>(1);
	}
}
