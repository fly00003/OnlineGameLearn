using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartPanel : BasePanel {

    private Button loginButton;
    private Animator animator;
    public override void OnEnter()
    {
        base.OnEnter();
        loginButton = transform.Find("LoginBtn").GetComponent<Button>();
        animator = loginButton.transform.GetComponent<Animator>();
        loginButton.onClick.AddListener(OnLoginClick);
    }

    private void OnLoginClick()
    {
        PlayClickSound();
        uiMng.PushPanel(UIPanelType.Login);
    }
    public override void OnPause()
    {
        base.OnPause();
        animator.enabled = false;
        loginButton.transform.DOScale(0, 0.4f).OnComplete(() => loginButton.gameObject.SetActive(false));
    }
    public override void OnResume()
    {
        base.OnResume();
        loginButton.gameObject.SetActive(true);
        loginButton.transform.DOScale(1,0.4f).OnComplete(()=>animator.enabled=true);
    }
}
