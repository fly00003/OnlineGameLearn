﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel {
    private Text text;
    private string message = null;
    private float showTime = 1f;
    private void Update()
    {
        if(message!=null)
        {
            ShowMessage(message);
            message = null;
        }
       
    }
    public override void OnEnter()
    {
        base.OnEnter();
        uiMng.InjectUiMng(this);
        text = GetComponent<Text>();
        text.enabled = false;
    }
    public void ShowMessageSync(string msg)
    {
        message = msg;
    }
    public void ShowMessage(string msg)
    {
        text.CrossFadeAlpha(1, 0.2f, false);
        text.text = msg;
        text.enabled = true;
        Invoke("Hide",showTime);
    }
    private void Hide()
    {
        text.CrossFadeAlpha(0,1,false);
    }
}
