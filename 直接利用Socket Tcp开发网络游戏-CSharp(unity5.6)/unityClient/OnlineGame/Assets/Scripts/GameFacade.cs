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
        UpdateManagers();
    }
    private void UpdateManagers()
    {
        audioMng.Update();
        cameraMng.Update();
        playerMng.Update();
        requestMng.Update();
        clientMng.Update();
        uiMng.Update();
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
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestMng.AddRequest(actionCode,request);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        requestMng.RemoveRequest(actionCode);
    }
    public void HandleReponse(ActionCode actionCode, string data)
    {
        requestMng.HnadleReponse(actionCode,data);
    }
    public void ShowMessage(string msg)
    {
        uiMng.ShowMessage(msg);
    }
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientMng.SendRequest(requestCode,actionCode,data);
    }
    public void PlayBgSound(string soundName)
    {
        audioMng.PlayBgSound(soundName);
    }
    public void PlayNormalSound(string soundName)
    {
        audioMng.PlayNormalSound(soundName);
    }
    public void SetUserData(UserData ud)
    {
        playerMng.UserData = ud;
    }
    public UserData GetUserData()
    {
        return playerMng.UserData;
    }
}
