using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Animator Buttons;
    public GameObject pressAny;
    public AudioSource clickMenu;

    Canvas canvas;
    bool on;

    void Start () {
        canvas = GetComponent<Canvas>();
	}
	
	void Update () {
        if (Input.anyKey)
        {
            if (on == false) { clickMenu.Play(); }
           
            pressAny.SetActive(false);
            Buttons.SetBool("menuOn", true);
            on = true;
        }
        
	}

    public void Play()
    {
        //Debug.Log("Jogo");
        SceneManager.LoadScene("Principal2");
    }

    public void Credits()
    {
        //Debug.Log("Creditos");
        SceneManager.LoadScene("Credits");
    }

    public void Exit()
    {
        //Debug.Log("Sair");
        Application.Quit();
    }
}
