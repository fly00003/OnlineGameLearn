﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class BaseRequest : MonoBehaviour {
    protected RequestCode requestCode=RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade facade;
    public virtual void Awake() {
        GameFacade.Instance.AddRequest(actionCode,this);
        facade = GameFacade.Instance;
    }
    public virtual void SendRequest() { }
    public virtual void OnResponse(string data) { }

    public virtual void OnDestroy()
    {
        GameFacade.Instance.RemoveRequest(actionCode);
    }
    protected void SendRequest(string data)
    {
        //Debug.LogError(data);
        facade.SendRequest(requestCode, actionCode, data);
        
    }
}
