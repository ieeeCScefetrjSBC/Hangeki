using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    static GameUI Instance;
    static bool canComeBack;

    public Sprite[] lifes;
    public Image bar;

    public GameObject gameWin;
    public GameObject gameOver;
    public GameObject pressAny;
    public GameObject go;

    void Start () {
        Instance = this;
	}
	
	
	void Update () {

        if (canComeBack && Input.anyKey) Menu();

        int index = (int)(Player.Instance.health / 20);

        if(index >= 0 && index <= 5)
            bar.sprite = lifes[index];
        else
            bar.sprite = lifes[0];

    }

    public static void GameOver()
    {
        Instance.gameOver.SetActive(true);
        Instance.Invoke("canCome", 5f);
    }

    public static void Go()
    {
        Instance.go.SetActive(true);
    }

    public static void UnGo()
    {
        Instance.go.SetActive(false);
    }

    public  static void GameWin()
    {
        Instance.gameWin.SetActive(true);
        Instance.Invoke("canCome", 5f);
    }

    void canCome()
    {
        canComeBack = true;
        pressAny.SetActive(true);
    }

    static void Menu() {
        canComeBack = false;
        SceneManager.LoadScene("Menu");
    }
}
