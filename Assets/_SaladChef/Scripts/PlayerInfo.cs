using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Player
{
    Player1,
    Player2
}

public class PlayerInfo : MonoBehaviour
{

    public string playerName = "";

    public Player playerNum;

    public int playerScore = 0;

    public PlayerInfoScriptableObject playerInfoObj;

    public GameObject characterBody;
    public GameObject characterJoints;

    // Start is called before the first frame update
    void Start()
    {
        // Setup Player Appearance
        switch (playerNum)
        {
            case Player.Player1:
                characterBody.GetComponent<SkinnedMeshRenderer>().material = playerInfoObj.player1Mats[0];
                characterJoints.GetComponent<SkinnedMeshRenderer>().material = playerInfoObj.player1Mats[1];
                break;

            case Player.Player2:
                characterBody.GetComponent<SkinnedMeshRenderer>().material = playerInfoObj.player2Mats[0];
                characterJoints.GetComponent<SkinnedMeshRenderer>().material = playerInfoObj.player2Mats[1];
                break;
        }
        playerScore = playerInfoObj.startingScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
