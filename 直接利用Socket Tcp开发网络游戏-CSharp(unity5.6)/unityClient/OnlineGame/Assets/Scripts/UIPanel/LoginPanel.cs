using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
public class LoginPanel : BasePanel {

    private Button closeButton;
    private InputField usernameIF;
    private InputField passwordIF;
    private Button loginButton;
    private Button registerButton;
    private LoginRequest loginRequest;

    void Start()
    {
        loginRequest = GetComponent<LoginRequest>();
        usernameIF = transform.Find("UsernameLable/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLable/PasswordInput").GetComponent<InputField>();
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
        loginButton = transform.Find("LoginBtn").GetComponent<Button>();
        loginButton.onClick.AddListener(OnLoginClick);
        registerButton = transform.Find("RegisterBtn").GetComponent<Button>();
        registerButton.onClick.AddListener(OnRegisterClick);

    }
    public override void OnEnter()
    {
        base.OnEnter();

        OnEnterAnim();

    }

    private void OnLoginClick()
    {
        PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }
        if (msg != "")
        {
            uiMng.ShowMessage(msg);return;
        }
        //Debug.LogError("meg正常");
        loginRequest.SendRequest(usernameIF.text,passwordIF.text);
    }

    private void OnRegisterClick()
    {
        PlayClickSound();
        uiMng.PushPanel(UIPanelType.Register);
    }

    private void OnCloseClick()
    {
        PlayClickSound();
        transform.DOScale(0,0.4f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000,0,0),0.4f);
        tweener.OnComplete(()=>uiMng.PopPanel());
    }

    public void OnLoginResponse(ReturnCode returnCode )
    {
        if (returnCode == ReturnCode.Success)
        {
            uiMng.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            uiMng.ShowMessageSync("用户名或密码错误，无法登录，请重新输入");
        }
    }

    public override void OnPause()
    {
        OnExitAnim();
    }
    public override void OnResume()
    {
        OnEnterAnim();
    }

    public override void OnExit()
    {
        base.OnExit();
        OnExitAnim();
    }
    private void OnEnterAnim()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.5f);
    }
    private void OnExitAnim()
    {
        transform.DOScale(0, 0.4f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f);
        tweener.OnComplete(() => gameObject.SetActive(false));
    }
}
