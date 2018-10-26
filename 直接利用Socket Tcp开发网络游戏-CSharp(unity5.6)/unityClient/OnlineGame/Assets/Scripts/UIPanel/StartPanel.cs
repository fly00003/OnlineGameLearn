using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartPanel : BasePanel {
    public override void OnEnter()
    {
        base.OnEnter();
        Button startbtn = transform.Find("LoginBtn").GetComponent<Button>();
        startbtn.onClick.AddListener(ShowLoginPanel);
    }

    private void ShowLoginPanel()
    {
        uiMng.PushPanel(UIPanelType.Login);
    }
}
