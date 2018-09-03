using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Voltar")) SceneManager.LoadScene("Menu");
	}
}
