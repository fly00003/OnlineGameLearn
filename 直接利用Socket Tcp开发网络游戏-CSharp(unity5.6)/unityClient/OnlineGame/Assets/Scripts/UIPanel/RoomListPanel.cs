using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomListPanel : BasePanel {
    private RectTransform battleRes;
    private RectTransform roomList;
    private VerticalLayoutGroup roomLayout;
    private GameObject roomItemPrefab;
    void Start()
    {
        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        roomLayout = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;
        transform.Find("RoomList/CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
        transform.Find("RoomList/CreateRoomButton").GetComponent<Button>().onClick.AddListener(OnCreateRoomClick);
    }

    private void OnCreateRoomClick()
    {
        uiMng.PushPanel(UIPanelType.Room);
    }
    public override void OnPause()
    {
        HideAnim();
    }
    public override void OnResume()
    {
        EnterAnim();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        SetBattleRes();
        if (battleRes!=null)
        EnterAnim();
    }

    private void OnCloseClick()
    {
        uiMng.PopPanel();
    }

    private void EnterAnim()
    {
        gameObject.SetActive(true);
        battleRes.localPosition = new Vector3(-1000,0);
        battleRes.DOLocalMoveX(-181,0.5f);
        roomList.localPosition = new Vector3(1000,0);
        roomList.DOLocalMoveX(156,0.5f);
    }
    private void HideAnim()
    {
        battleRes.DOLocalMoveX(-1000, 0.5f);

        roomList.DOLocalMoveX(1000, 0.5f).OnComplete(()=>gameObject.SetActive(false));
    }
    public override void OnExit()
    {
        HideAnim();
    }
    private void SetBattleRes()
    {
        UserData ud = facade.GetUserData();
        transform.Find("BattleRes/Username").GetComponent<Text>().text = ud.Username;
        transform.Find("BattleRes/TotalCount").GetComponent<Text>().text ="总场数："+ ud.TotalCount.ToString();
        transform.Find("BattleRes/WinCount").GetComponent<Text>().text = "胜利："+ud.WinCount.ToString();
    }
    private void LoadRoomItem(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(roomLayout.transform);
        }
        Vector2 size = roomLayout.GetComponent<RectTransform>().sizeDelta;
        int roomCount = GetComponentsInChildren<RoomItem>().Length;
        roomLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x,
            roomCount*(roomItemPrefab.GetComponent<RectTransform>().sizeDelta.y+roomLayout.spacing));
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadRoomItem(1);
        }
    }
}


