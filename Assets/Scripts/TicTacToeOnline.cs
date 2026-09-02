using System;
using TMPro;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Collections.Generic;

public class TicTacToeOnline : NetworkBehaviour
{
    string turn = "X";
    string startingTurn = "X";

    TextMeshProUGUI[] cells;

    bool Finished = false;
    int plusone;

    public TextMeshProUGUI btnText1 = null;
    public TextMeshProUGUI btnText2 = null;
    public TextMeshProUGUI btnText3 = null;
    public TextMeshProUGUI btnText4 = null;
    public TextMeshProUGUI btnText5 = null;
    public TextMeshProUGUI btnText6 = null;
    public TextMeshProUGUI btnText7 = null;
    public TextMeshProUGUI btnText8 = null;
    public TextMeshProUGUI btnText9 = null;

    public TextMeshProUGUI txtPlayerX;
    public TextMeshProUGUI txtPlayerO;
    public TextMeshProUGUI PlayerTurn;

    public GameObject Line1;
    public GameObject Line2;
    public GameObject Line3;
    public GameObject Line4;
    public GameObject Line5;
    public GameObject Line6;
    public GameObject Line7;
    public GameObject Line8;


    public GameObject VectoryPanel;

    public Image VectoryImage;
    public Sprite winSpriteX;
    public Sprite winSpriteO;
    public Sprite DrawSprite;

    HashSet<ulong> playAgainVotes = new HashSet<ulong>();
    Color defaultColor;
    public Button playAgainButton;
    public TextMeshProUGUI waitingText;

    bool waitingForServer;

    public AudioClip PaperScrawling;
    public AudioSource audioSource;

    void Awake()
    {
        cells = new TextMeshProUGUI[]
        {
            btnText1, btnText2, btnText3,
            btnText4, btnText5, btnText6,
            btnText7, btnText8, btnText9
        };
        defaultColor = btnText1.color;
    }

    public void Start()
    {
        VectoryPanel.SetActive(false);
    }

    public void CheckDraw()
    {
        int counter = 0;
        TextMeshProUGUI[] Texts = { btnText1, btnText2, btnText3, btnText4, btnText5, btnText6, btnText7, btnText8 , btnText9 };
        foreach(var Text in Texts)
        {
            if(Text.text != "")
            {
                counter++;
            }
        }
        if(counter == Texts.Length)
        {
            Finished = true;
            VectoryPanel.SetActive(true);
            VectoryImage.sprite = DrawSprite;
        }
    }

    public void CheckWinner()
    {
        if (btnText1.text == btnText2.text && btnText2.text == btnText3.text)
        {
            if(btnText1.text == "O")
            {
                btnText1.color = Color.red;
                btnText2.color = Color.red;
                btnText3.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line1.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteO;
            }
            if(btnText1.text == "X")
            {
                btnText1.color = Color.blue;
                btnText2.color = Color.blue;
                btnText3.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line1.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText4.text == btnText5.text && btnText5.text == btnText6.text)
        {
            if(btnText4.text == "O")
            {
                btnText4.color = Color.red;
                btnText5.color = Color.red;
                btnText6.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line2.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteO;
            }
            if(btnText4.text == "X")
            {
                btnText4.color = Color.blue;
                btnText5.color = Color.blue;
                btnText6.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line2.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText7.text == btnText8.text && btnText8.text == btnText9.text)
        {
            if(btnText7.text == "O")
            {
                btnText7.color = Color.red;
                btnText8.color = Color.red;
                btnText9.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line3.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteO;
            }
            if(btnText7.text == "X")
            {
                btnText7.color = Color.blue;
                btnText8.color = Color.blue;
                btnText9.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line3.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText1.text == btnText4.text && btnText4.text == btnText7.text)
        {
            if(btnText1.text == "O")
            {
                btnText1.color = Color.red;
                btnText4.color = Color.red;
                btnText7.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line4.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteO;
            }
            if(btnText1.text == "X")
            {
                btnText1.color = Color.blue;
                btnText4.color = Color.blue;
                btnText7.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line4.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText2.text == btnText5.text && btnText5.text == btnText8.text)
        {
            if(btnText2.text == "O")
            {
                btnText2.color = Color.red;
                btnText5.color = Color.red;
                btnText8.color = Color.red;

                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line5.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteO;
            }
            if(btnText2.text == "X")
            {
                btnText2.color = Color.blue;
                btnText5.color = Color.blue;
                btnText8.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line5.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText3.text == btnText6.text && btnText6.text == btnText9.text)
        {
            if(btnText3.text == "O")
            {
                btnText3.color = Color.red;
                btnText6.color = Color.red;
                btnText9.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line6.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteO;
            }
            if(btnText3.text == "X")
            {
                btnText3.color = Color.blue;
                btnText6.color = Color.blue;
                btnText9.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line6.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText1.text == btnText5.text && btnText5.text == btnText9.text)
        {
            if(btnText1.text == "O")
            {
                btnText1.color = Color.red;
                btnText5.color = Color.red;
                btnText9.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line7.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteO;
            }
            if(btnText1.text == "X")
            {
                btnText1.color = Color.blue;
                btnText5.color = Color.blue;
                btnText9.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line7.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText3.text == btnText5.text && btnText5.text == btnText7.text)
        {
            if(btnText3.text == "O")
            {
                btnText3.color = Color.red;
                btnText5.color = Color.red;
                btnText7.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line8.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteO;
            }
            if(btnText3.text == "X")
            {
                btnText3.color = Color.blue;
                btnText5.color = Color.blue;
                btnText7.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line8.SetActive(true);
                VectoryPanel.SetActive(true);
                VectoryImage.sprite = winSpriteX;
            }
        }
    }

    public void ChangeValue(TextMeshProUGUI clickedText)
    {
        if (!IsSpawned || Finished || waitingForServer)
            return;
        if (clickedText.text != "")
            return;

        string myMark = IsHost ? "X" : "O";
        if (myMark != turn)
            return;

        int index = Array.IndexOf(cells, clickedText);
        if (index < 0)
            return;

        waitingForServer = true;
        RequestMoveServerRpc(index);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestMoveServerRpc(int index, ServerRpcParams rpcParams = default)
    {
        ulong sender = rpcParams.Receive.SenderClientId;
        string mark = sender == NetworkManager.ServerClientId ? "X" : "O";

        bool valid = false;
        if(!Finished && index >= 0 && index < cells.Length && cells[index].text == "" && mark == turn)
        {
            valid = true;
        }
        if (!valid) {
            RejectMoveClientRpc(index, mark, new ClientRpcParams {
                Send = new ClientRpcSendParams { TargetClientIds = new[] { sender } }
            });
            return;
        }

        ApplyMoveClientRpc(index, mark);
    }

    [ClientRpc]
    private void RejectMoveClientRpc(int index, string mark, ClientRpcParams rpcParams = default)
    {
        waitingForServer = false;
        if (index >= 0 && index < cells.Length && cells[index].text == mark)
            cells[index].text = "";
    }

    [ClientRpc]
    private void ApplyMoveClientRpc(int index, string mark)
    {
        waitingForServer = false;
        cells[index].text = mark;
        if (mark == "X")
            turn = "O";
        else
            turn = "X";
        UpdateTurnText();
        audioSource.PlayOneShot(PaperScrawling);
        CheckWinner();
        if (!Finished)
        {
            CheckDraw();
        }
    }

    void UpdateTurnText()
    {
        if (PlayerTurn == null)
            return;

        PlayerTurn.text = "Player : " + turn;
    }

    public void PlayAgain()
    {
        if (!IsSpawned || !Finished) 
            return;
        playAgainButton.interactable = false;
        if (waitingText != null)
        {
            waitingText.gameObject.SetActive(true);
            waitingText.text = "Waiting for the other player...";
        }
        PlayAgainServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void PlayAgainServerRpc(ServerRpcParams rpcParams = default)
    {
        playAgainVotes.Add(rpcParams.Receive.SenderClientId);
        if (playAgainVotes.Count >= NetworkManager.ConnectedClientsIds.Count)
        {
            playAgainVotes.Clear();
            ResetBoardClientRpc();
        }
    }

    [ClientRpc]
    private void ResetBoardClientRpc()
    {
        waitingForServer = false;
        foreach (var cell in cells)
        {
            cell.text = "";
            cell.color = defaultColor;
        }
        GameObject[] lines = { Line1, Line2, Line3, Line4, Line5, Line6, Line7, Line8 };
        foreach (var line in lines)
        {
            line.SetActive(false);
        }

        Finished = false;
        if (startingTurn == "X") {
            startingTurn = "O";
        }

        else {
            startingTurn = "X";
        }
        turn = startingTurn;
        VectoryPanel.SetActive(false);
        playAgainButton.interactable = true;
        if (waitingText != null)
        {
            waitingText.gameObject.SetActive(false);
        }
    }
}
