using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel {
    private Text text;
    private float showTime = 1f;
    public override void OnEnter()
    {
        base.OnEnter();
        uiMng.InjectUiMng(this);
        text = GetComponent<Text>();
        text.enabled = false;
    }
    public void ShowMessage(string msg)
    {
        text.color = Color.white;
        text.text = msg;
        text.enabled = true;
        Invoke("Hide",showTime);
    }
    private void Hide()
    {
        text.CrossFadeAlpha(0,1,false);
    }
}
