using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : BasePanel {
    private Text localPlayerUsername;
    private Text localPlayerTotalCount;
    private Text localPlayerWinCount;

    private Text enemyPlayerUsername;
    private Text enemyPlayerTotalCount;
    private Text enemyPlayerWinCount;

    private Transform bluePanel;
    private Transform redPanel;
    private Transform startButton;
    private Transform exitButton;

    CreateRoomRequest crRequest ;

    private UserData ud = null;

    private void Start()
    {
        localPlayerUsername = transform.Find("BluePanel/Username").GetComponent<Text>();
        localPlayerTotalCount = transform.Find("BluePanel/TotalCount").GetComponent<Text>();
        localPlayerWinCount = transform.Find("BluePanel/WinCount").GetComponent<Text>();
        enemyPlayerUsername = transform.Find("RedPanel/Username").GetComponent<Text>();
        enemyPlayerTotalCount = transform.Find("RedPanel/TotalCount").GetComponent<Text>();
        enemyPlayerWinCount = transform.Find("RedPanel/WinCount").GetComponent<Text>();
        bluePanel = transform.Find("BluePanel");
        redPanel = transform.Find("RedPanel");
        startButton = transform.Find("StartButton");
        exitButton = transform.Find("ExitButton");
        crRequest = GetComponent<CreateRoomRequest>();
        startButton.GetComponent<Button>().onClick.AddListener(OnStartClick);
        exitButton.GetComponent<Button>().onClick.AddListener(OnExitClick);
        EnterAnim();
        crRequest.SendRequest();
    }
    public override void OnEnter()
    {
        if (bluePanel != null)
        {
            EnterAnim();
        }
        if(crRequest==null)
            crRequest = GetComponent<CreateRoomRequest>();
        crRequest.SendRequest();
    }
    public override void OnExit()
    {
        ExitAnim();
    }
    public override void OnPause()
    {
        ExitAnim();
    }
    public override void OnResume()
    {
        EnterAnim();
    }
    void Update()
    {
        if (ud != null)
        {
            SetLocalPlayerRes(ud.Username,ud.TotalCount.ToString(),ud.WinCount.ToString());
            ClearEnemyPlayerRes();
            ud = null;
        }
    }
    private void OnExitClick()
    {
        throw new NotImplementedException();
    }

    private void OnStartClick()
    {
        throw new NotImplementedException();
    }
    public void SetLocalPlayerResSync()
    {
        ud = facade.GetUserData();
    }

    public void SetLocalPlayerRes(string username, string totalCount, string winCount)
    {
        localPlayerUsername.text = username;
        localPlayerTotalCount.text = "总场数\n" + totalCount;
        localPlayerWinCount.text = "胜场\n" + winCount;
    }

    public void SetEnemyPlayerRes(string username, string totalCount, string winCount)
    {
        enemyPlayerUsername.text = username;
        enemyPlayerTotalCount.text = "总场数\n" + totalCount;
        enemyPlayerWinCount.text = "胜场\n" + winCount;
    }
    public void ClearEnemyPlayerRes()
    {
        enemyPlayerUsername.text = "";
        enemyPlayerTotalCount.text = "等待玩家加入";
        enemyPlayerWinCount.text = "";
    }
    private void EnterAnim()
    {
        gameObject.SetActive(true);
        bluePanel.localPosition = new Vector3(-1000,20,0);
        bluePanel.DOLocalMoveX(-190,0.4f);
        redPanel.localPosition = new Vector3(1000, 20, 0);
        redPanel.DOLocalMoveX(190, 0.4f);
        startButton.localScale = Vector3.zero;
        startButton.DOScale(1,0.4f);
        exitButton.localScale = Vector3.zero;
        exitButton.DOScale(1,0.4f);
    }
    private void ExitAnim()
    {
        bluePanel.DOLocalMoveX(-1000, 0.4f);
        redPanel.DOLocalMoveX(1000, 0.4f);
        startButton.DOScale(0, 0.4f);
        exitButton.DOScale(0, 0.4f).OnComplete(()=>gameObject.SetActive(false));
    }
}
