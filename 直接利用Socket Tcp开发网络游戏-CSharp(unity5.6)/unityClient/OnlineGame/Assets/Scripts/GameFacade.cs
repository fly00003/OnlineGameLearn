using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class GameFacade : MonoBehaviour {
    private static GameFacade _instance;
    public static GameFacade Instance { get { return _instance; } }

    private AudioManager audioMng;
    private CameraManager cameraMng;
    private PlayerManager playerMng;
    private RequestManager requestMng;
    private ClientManager clientMng;
    private UIManager uiMng;
    // Use this for initialization
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);return;
        }
        _instance = this;
    }
	void Start () {
        InitManager();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void InitManager()
    {
        audioMng = new AudioManager(this);
        cameraMng = new CameraManager(this);
        playerMng = new PlayerManager(this);
        requestMng = new RequestManager(this);
        clientMng = new ClientManager(this);
        uiMng = new UIManager(this);
        audioMng.OnInit();
        cameraMng.OnInit();
        playerMng.OnInit();
        requestMng.OnInit();
        clientMng.OnInit();
        uiMng.OnInit();
    }
    private void DestroyManager()
    {
        audioMng.OnDestroy();
        cameraMng.OnDestroy();
        playerMng.OnDestroy();
        requestMng.OnDestroy();
        clientMng.OnDestroy();
        uiMng.OnDestroy();
    }
    private void OnDestroy()
    {
        DestroyManager();
    }
    public void AddRequest(RequestCode requestCode, BaseRequest request)
    {
        requestMng.AddRequest(requestCode,request);
    }
    public void RemoveRequest(RequestCode requestCode)
    {
        requestMng.RemoveRequest(requestCode);
    }
    public void HandleReponse(RequestCode requestCode, string data)
    {
        requestMng.HnadleReponse(requestCode,data);
    }
    public void ShowMessage(string msg)
    {
        uiMng.ShowMessage(msg);
    }
}
