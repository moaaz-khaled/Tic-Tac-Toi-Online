using System;
using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeLocal : MonoBehaviour
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


    Color defaultColor;
    public AudioClip PaperScrawling;
    public AudioSource audioSource;

    void Awake()
    {
        cells = new TextMeshProUGUI[]
        {
            btnText1, btnText2, btnText3,
            btnText4, btnText5, btnText6,
            btnText7, btnText8, btnText9,
        };
        defaultColor = btnText1.color;
    }

    public void Start()
    {
        VectoryPanel.SetActive(false);
        UpdateTurnText();
    }

    void UpdateTurnText()
    {
        if (PlayerTurn == null)
            return;

        PlayerTurn.text = "Player : " + turn;
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
            VectoryPanel.SetActive(true);
            VectoryImage.sprite = DrawSprite;
        }
    }

    public void CheckWinner()
    {
        if (btnText1.text == btnText2.text && btnText2.text == btnText3.text)
        {
            if (btnText1.text == "O")
            {
                btnText1.color = Color.red;
                btnText2.color = Color.red;
                btnText3.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line1.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteO;
            }
            if (btnText1.text == "X")
            {
                btnText1.color = Color.blue;
                btnText2.color = Color.blue;
                btnText3.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line1.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText4.text == btnText5.text && btnText5.text == btnText6.text)
        {
            if (btnText4.text == "O")
            {
                btnText4.color = Color.red;
                btnText5.color = Color.red;
                btnText6.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line2.SetActive(true);
                StartCoroutine(ShowVectoryPanel());

                VectoryImage.sprite = winSpriteO;
            }
            if (btnText4.text == "X")
            {
                btnText4.color = Color.blue;
                btnText5.color = Color.blue;
                btnText6.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line2.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText7.text == btnText8.text && btnText8.text == btnText9.text)
        {
            if (btnText7.text == "O")
            {
                btnText7.color = Color.red;
                btnText8.color = Color.red;
                btnText9.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line3.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteO;
            }
            if (btnText7.text == "X")
            {
                btnText7.color = Color.blue;
                btnText8.color = Color.blue;
                btnText9.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line3.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText1.text == btnText4.text && btnText4.text == btnText7.text)
        {
            if (btnText1.text == "O")
            {
                btnText1.color = Color.red;
                btnText4.color = Color.red;
                btnText7.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line4.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteO;
            }
            if (btnText1.text == "X")
            {
                btnText1.color = Color.blue;
                btnText4.color = Color.blue;
                btnText7.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line4.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText2.text == btnText5.text && btnText5.text == btnText8.text)
        {
            if (btnText2.text == "O")
            {
                btnText2.color = Color.red;
                btnText5.color = Color.red;
                btnText8.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line5.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteO;
            }
            if (btnText2.text == "X")
            {
                btnText2.color = Color.blue;
                btnText5.color = Color.blue;
                btnText8.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line5.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText3.text == btnText6.text && btnText6.text == btnText9.text)
        {
            if (btnText3.text == "O")
            {
                btnText3.color = Color.red;
                btnText6.color = Color.red;
                btnText9.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line6.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteO;
            }
            if (btnText3.text == "X")
            {
                btnText3.color = Color.blue;
                btnText6.color = Color.blue;
                btnText9.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line6.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText1.text == btnText5.text && btnText5.text == btnText9.text)
        {
            if (btnText1.text == "O")
            {
                btnText1.color = Color.red;
                btnText5.color = Color.red;
                btnText9.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line7.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteO;
            }
            if (btnText1.text == "X")
            {
                btnText1.color = Color.blue;
                btnText5.color = Color.blue;
                btnText9.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line7.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteX;
            }
        }
        if (btnText3.text == btnText5.text && btnText5.text == btnText7.text)
        {
            if (btnText3.text == "O")
            {
                btnText3.color = Color.red;
                btnText5.color = Color.red;
                btnText7.color = Color.red;
                plusone = int.Parse(txtPlayerO.text) + 1;
                txtPlayerO.text = Convert.ToString(plusone);
                Finished = true;
                Line8.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteO;
            }
            if (btnText3.text == "X")
            {
                btnText3.color = Color.blue;
                btnText5.color = Color.blue;
                btnText7.color = Color.blue;
                plusone = int.Parse(txtPlayerX.text) + 1;
                txtPlayerX.text = Convert.ToString(plusone);
                Finished = true;
                Line8.SetActive(true);
                StartCoroutine(ShowVectoryPanel());
                VectoryImage.sprite = winSpriteX;
            }
        }
    }

    public void ChangeValue(TextMeshProUGUI clickedText)
    {
        if (Finished)
            return;
        if (clickedText.text != "")
            return;

        clickedText.text = turn;
        turn = (turn == "X") ? "O" : "X";
        UpdateTurnText();
        audioSource.PlayOneShot(PaperScrawling);

        CheckWinner();
        if (!Finished)
            CheckDraw();
    }

    IEnumerator ShowVectoryPanel()
    {
        yield return new WaitForSeconds(0.25f);
        VectoryPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        StopAllCoroutines();

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

        startingTurn = (startingTurn == "X") ? "O" : "X";
        turn = startingTurn;
        UpdateTurnText();
        
        VectoryPanel.SetActive(false);
    }
}
