using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour {
    public Text username;
    public Text totalCount;
    public Text winCount;
    public Button joinButton;
	// Use this for initialization
	void Start () {
        if (joinButton != null)
        {
            joinButton.onClick.AddListener(OnJoinClick);
        }
	}
    public void SetRoomInfo(string username,int totalCount,int winCount)
    {
        this.username.text = username;
        this.totalCount.text = "总场数\n" + totalCount;
        this.winCount.text = "胜场\n" + winCount;
    }
    public void SetRoomInfo(string username, string totalCount, string winCount)
    {
        this.username.text = username;
        this.totalCount.text = "总场数\n" + totalCount;
        this.winCount.text = "胜场\n" + winCount;
    }
    private void OnJoinClick()
    {
        throw new NotImplementedException();
    }
}
