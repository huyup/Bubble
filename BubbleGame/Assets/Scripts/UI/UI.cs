using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GUIStyle fontStyle;
    float baseWidth = 854f;
    float baseHeight = 480f;
    public Texture2D heartImg;

    GameObject[] players;
    int player1HP;
    int player2HP;
    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");



    }
    private void Update()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerStatus>().playerNum == PlayerStatus.PlayerSelection.Player1)
                player1HP = players[i].GetComponent<PlayerStatus>().nowHp;
            if (players[i].GetComponent<PlayerStatus>().playerNum == PlayerStatus.PlayerSelection.Player2)
                player2HP = players[i].GetComponent<PlayerStatus>().nowHp;
        }
    }
    private void OnGUI()
    {
        // 解像度対応.
        GUI.matrix = Matrix4x4.TRS(
            Vector3.zero,
            Quaternion.identity,
            new Vector3(Screen.width / baseWidth, Screen.height / baseHeight, 1f));

        GUI.Label(new Rect(10, 450, 100, 100), "Player1:", fontStyle);
        
        DrawPlayer1Life(player1HP);

        GUI.Label(new Rect(690, 450, 100, 100), "Player2:", fontStyle);
        DrawPlayer2Life(player2HP);


    }

    void DrawPlayer1Life(int _player1hp)
    {
        float tmp = 27;
        float tmp2 = 28;

        switch (_player1hp)
        {
            case 0:
                break;
            case 1:
                GUI.Label(new Rect(90, 450, tmp2, tmp2), heartImg);
                break;
            case 2:
                GUI.Label(new Rect(90, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(90 + tmp, 450, tmp2, tmp2), heartImg);
                break;
            case 3:
                GUI.Label(new Rect(90, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(90 + tmp, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(90 + tmp * 2, 450, tmp2, tmp2), heartImg);
                break;
        }
    }
    void DrawPlayer2Life(int _player2hp)
    {
        float tmp = 27;
        float tmp2 = 28;

        switch (_player2hp)
        {
            case 0:
                break;
            case 1:
                GUI.Label(new Rect(770, 450, tmp2, tmp2), heartImg);
                break;
            case 2:
                GUI.Label(new Rect(770, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(770 + tmp, 450, tmp2, tmp2), heartImg);
                break;
            case 3:
                GUI.Label(new Rect(770, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(770 + tmp, 450, tmp2, tmp2), heartImg);
                GUI.Label(new Rect(770 + tmp * 2, 450, tmp2, tmp2), heartImg);
                break;
        }
    }
}
