using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public GameObject EnemyType1;
    public GameObject EnemyType2;
    public GameObject EnemyType3;

    public GameObject nextArea;
    public GameObject previousArea;
    public Vector2 InstantiatePivot;

    public int areaIndex;
    public int totalEnemies;
    public float timeForStart;
    public float timePerWave;
    public Vector2 limitesCam;

    private int remainingEnemies;
    private int numEnemiesPerWave = 3;
    private float timeLastCheckpoint = 0;

    private bool playerEntered = false;
    private bool hasStarted = false;
    public bool hasEnded = false;
    private bool waveDeployed = false;

    List<GameObject> Ene;


    // Use this for initialization
    void Start()
    {
        Ene = new List<GameObject>();
        remainingEnemies = totalEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted && !hasEnded)
        {
           
            if (!waveDeployed )
            {
                DeployWave();
                waveDeployed = true;
                timeLastCheckpoint = Time.time;
            } 
            else if (((remainingEnemies > 0) && (Time.time - timeLastCheckpoint > timePerWave)) || (Ene.TrueForAll(g => g == null)))
            {
                waveDeployed = false;
            }

            if (remainingEnemies <= 0 && (Ene.TrueForAll(g => g == null)))
                hasEnded = true;
        }

        

        if (hasEnded && areaIndex >= 2) GameUI.GameWin();

    }

    void InstantiateEnemies()
    {
        Ene.Add(Instantiate<GameObject>(EnemyType1, InstantiatePivot, Quaternion.identity));
        Ene.Add(Instantiate<GameObject>(EnemyType2, InstantiatePivot + Vector2.down * 5f, Quaternion.identity));
        Ene.Add(Instantiate<GameObject>(EnemyType3, InstantiatePivot + Vector2.down * 10f, Quaternion.identity));
    }

    void DeployWave()
    {
        InstantiateEnemies();
        remainingEnemies -= numEnemiesPerWave;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if(!hasStarted) playerEntered = false;

            if (hasEnded) {
                BoxCollider2D b = GetComponent<BoxCollider2D>();
                if (b != null)
                {
                    b.isTrigger = false;
                    GameUI.UnGo();
                }

                if (areaIndex < 2)
                {
                    nextArea.GetComponent<Collider2D>().isTrigger = true;
                }

            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Player" && hasEnded) GameUI.Go();

        if (collider.gameObject.tag == "Player" && !hasStarted)
        {
            

            if (!playerEntered)
            {
                playerEntered = true;
                timeLastCheckpoint = Time.time;
            }
            else if (Time.time - timeLastCheckpoint > timeForStart)
            {
                hasStarted = true;
                timeLastCheckpoint = Time.time;

                if (areaIndex > 0)
                {
                    previousArea.GetComponent<Collider2D>().isTrigger = false;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && (areaIndex != 2)) Cam.SetLimits(limitesCam);
    }
}