using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour {
    private AudioManager audioMng;
    private CameraManager cameraMng;
    private PlayerManager playerMng;
    private RequestManager requestMng;
    private ClientManager clientMng;
    private UIManager uiMng;
	// Use this for initialization
	void Start () {
        InitManager();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void InitManager()
    {
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
}
