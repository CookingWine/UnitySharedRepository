using TMPro;
using UnityEngine;
using UnityEngine.UI;

//自动生成于：2023/3/29 18:28:32
public partial class MusicLogin
{

	private RectTransform m_Trans_AuthCodeLogin;
	private TMP_InputField m_TInput_PhoneNumber;
	private TMP_InputField m_TInput_AuthCode;
	private Button m_Btn_SpeedCode;
	private Button m_Btn_Login;
	private TextMeshProUGUI m_TTxt_LoginBtnTxt;
	private RectTransform m_Trans_QRCodeLogin;
	private Image m_Img_QRCode;
	private Button m_Btn_SwitchoverLogin;

	private void InitBindComponent(GameObject go)
	{
		UIComponentBinding autoBindTool = go.GetComponent<UIComponentBinding>();
		m_Trans_AuthCodeLogin = autoBindTool.GetBindComponent<RectTransform>(0);
		m_TInput_PhoneNumber = autoBindTool.GetBindComponent<TMP_InputField>(1);
		m_TInput_AuthCode = autoBindTool.GetBindComponent<TMP_InputField>(2);
		m_Btn_SpeedCode = autoBindTool.GetBindComponent<Button>(3);
		m_Btn_Login = autoBindTool.GetBindComponent<Button>(4);
		m_TTxt_LoginBtnTxt = autoBindTool.GetBindComponent<TextMeshProUGUI>(5);
		m_Trans_QRCodeLogin = autoBindTool.GetBindComponent<RectTransform>(6);
		m_Img_QRCode = autoBindTool.GetBindComponent<Image>(7);
		m_Btn_SwitchoverLogin = autoBindTool.GetBindComponent<Button>(8);
	}
}
