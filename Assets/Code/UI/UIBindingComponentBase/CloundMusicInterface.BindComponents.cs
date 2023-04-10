using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/4/10 22:26:25
public partial class CloundMusicInterface
{

	private RectTransform m_Trans_TopInterface;
	private RectTransform m_Trans_LeftInterface;
	private RectTransform m_Trans_RigthInterface;
	private RectTransform m_Trans_DownInterface;
	private RectTransform m_Trans_FullScreen;
	private RectTransform m_Trans_PoPInterface;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Trans_TopInterface = autoBindTool.GetBindComponent<RectTransform>(0);
		m_Trans_LeftInterface = autoBindTool.GetBindComponent<RectTransform>(1);
		m_Trans_RigthInterface = autoBindTool.GetBindComponent<RectTransform>(2);
		m_Trans_DownInterface = autoBindTool.GetBindComponent<RectTransform>(3);
		m_Trans_FullScreen = autoBindTool.GetBindComponent<RectTransform>(4);
		m_Trans_PoPInterface = autoBindTool.GetBindComponent<RectTransform>(5);
	}
}
