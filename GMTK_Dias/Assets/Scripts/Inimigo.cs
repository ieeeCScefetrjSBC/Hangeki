using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour {

    public float maxHealth = 100f;
    public float fireDamagePerSec = 5f;
    public float stunTime = 3f;
    public float onFireTime = 2f;
    public float forceStun;
    public float damageCaused = 20f;

    private float health;
    private float timeWasStunned;
    private bool right;
    private Vector3 facingOld;
    private Vector3 facingNew;

    protected bool isStunned = false;
    public bool isOnFire = false;

    //public bool IsStunned { get; set; }
    //public bool IsOnFire { get; set; }

    protected float doubleStun;

    protected bool possibleHit;
    protected bool hitting;

    //public SpriteRenderer sp;
    protected Rigidbody2D rb;

    protected Animator anim;
    //public Sprite Ataque;

    public AudioSource Die;
    public AudioSource AttackSound;
    public AudioSource Hit;

    public CircleCollider2D coli;

    ParticleSystem ps;
    //ParticleSystem pf;

    public void StartCode()
    {
        health = maxHealth;
        facingOld = transform.localScale;
        facingNew = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        anim = GetComponentInChildren<Animator>();

        ps = GetComponentInChildren<ParticleSystem>();
       // pf = GetComponent<ParticleSystem>();
    }

    public void UpdateCode()
    {
        if (rb.velocity.x != 0) right = rb.velocity.x > 0;

        if (right) transform.localScale = facingOld;
        else transform.localScale = facingNew;

        if (isOnFire)
        {
            if (isOnFire)
                health -= fireDamagePerSec * Time.deltaTime;
        }

        if (health <= 0f)
            Kill();
    }

    public void Damage(float damage)
    {
        Hit.Play();
        health -= damage;
    }

    public void Stun(float stunTime)
    {
        if (!isStunned)
        {
            ps.gameObject.SetActive(true);
            ps.Play();
            isStunned = true;
            Invoke("UnStun", stunTime);
        }
        else
        {
            doubleStun = stunTime;
        }
    }

    public void UnStun()
    {
        if (doubleStun > 0)
        {
            Invoke("UnStun", doubleStun);
            doubleStun = 0;
        }
        else
        {
            ps.Stop();
            ps.gameObject.SetActive(false);
            isStunned = false;
        }
    }

    //public void SetOnFire()
    //{
        //isOnFire = true;
        //pf.gameObject.SetActive(true);
        //pf.Play();
        //Invoke("UnSetOnFire", onFireTime);
    //}

  //  public void UnSetOnFire()
   // {
     //   pf.Stop();
       // pf.gameObject.SetActive(false);
        //isOnFire = false;
    //}

    public void Stop(Vector2 dir) {
        CancelInvoke();
        if (anim.name == "Gatino")
        {
            anim.SetBool("Gatino Attack", false);
        }
        else if (anim.name == "Raphael")
        {
            anim.SetBool("Raphael Attack", false);
        }
        else {
            anim.SetBool("Bonsai Attack", false);
        }
            hitting = false;
        
        Player.Instance.Atacker.Remove(this);
        Player.Instance.defenceWindow = Player.Instance.Atacker.Count > 0;
        Stun(stunTime);
        rb.AddForce(forceStun* dir.normalized);
    }

    public void Kill()
    {
        Die.Play();
        enabled = false;
        coli.enabled = false;
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(false); 
        
        Invoke("Kill2", 2f);
    }

    public void Kill2()
    {
        Destroy(this.gameObject);
    }



}
