using Common;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RegisterPanel : BasePanel {
    private InputField usernameIF;
    private InputField passwordIF;
    private InputField rePasswordIF;
    private Button closeBtn;
    private Button registerBtn;
    private RegisterRequest registerRequest;
    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.5f);

        usernameIF = transform.Find("UsernameLable/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLable/PasswordInput").GetComponent<InputField>();
        rePasswordIF = transform.Find("RePasswordLable/RePasswordInput").GetComponent<InputField>();
        registerBtn = transform.Find("RegisterBtn").GetComponent<Button>();
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(OnCloseClick);
        registerBtn.onClick.AddListener(OnRegisterClick);
        registerRequest = GetComponent<RegisterRequest>();
    }

    private void OnRegisterClick()
    {
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "\n密码不能为空";
        }
        if (passwordIF.text != rePasswordIF.text)
        {
            msg += "\n密码不一致";
        }
        if (msg != "")
        {
            uiMng.ShowMessage(msg); return;
        }
        else
        {
            registerRequest.SendRequest(usernameIF.text,passwordIF.text);
        }
    }

    private void OnCloseClick()
    {
        transform.DOScale(0, 0.4f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f);
        tweener.OnComplete(() => uiMng.PopPanel());
    }
    public override void OnExit()
    {
        base.OnExit();
        closeBtn.onClick.RemoveListener(OnCloseClick);
        registerBtn.onClick.RemoveListener(OnRegisterClick);
        gameObject.SetActive(false);
    }
    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            uiMng.ShowMessageSync("注册成功");
        }
        else
        {
            uiMng.ShowMessageSync("用户名已存在，请修改用户名");
        }
    }
}
