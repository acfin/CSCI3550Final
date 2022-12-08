using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public GameObject SlimeIdle;
    public GameObject SlimeJump;
    public GameObject SlimeHurt;
    public GameObject SlimeDeath;
    private float speed = 6;
    private AudioSource audio;
    bool isDead = false;

    private Transform playerT;
    bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        Idle();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            if (playerT == null)
                playerT = GameObject.Find("PlayerObject(Clone)").GetComponent<Transform>();
            JumpToPlayer();
            CheckAlive();
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ammo")
        {
            Hurt();
        }
    }*/

    void CheckAlive()
    {
        if (hitPoints < 0 && !isDead)
            Death();
    }

    void JumpToPlayer()
    {
        float distanceToPlayer = Vector3.Distance(playerT.position, gameObject.transform.position);
        if (distanceToPlayer < 7)
        {
            if (!isAttacking)
            {
                Attack(playerT.position);
                isAttacking = true;
            }

        }
        else
            Idle();
    }

    void Attack(Vector3 player)
    {
        Jump();
        StartCoroutine(TravelArc(player, 0.8f));
    }

    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        // Grab the current gameobject's position
        Vector3 startPosition = transform.position;

        float percentComplete = 0.0f;

        // check that percentComplete is less than 100%
        while (percentComplete < 1.0f)
        {
            // Time elapsed since the last frame, divided by the total desired duration = a percentage of the duration
            percentComplete += Time.deltaTime / duration;

            // Effectively, travel PI distance down the sine curve every second
            float currentHeight = Mathf.Sin(Mathf.PI * percentComplete);
            currentHeight *= 2;
            transform.position = Vector3.MoveTowards(startPosition, destination, percentComplete * speed) + Vector3.up * currentHeight;
            CheckAlive();

            yield return null;
        }

        // Deactivate the object when it reaches its destination
        Idle();
        Invoke("AttackCooldown", 2);
    }

    void AttackCooldown()
    {
        isAttacking = false;
    }

    void Idle()
    {
        SlimeIdle.SetActive(true);
        SlimeJump.SetActive(false);
        SlimeHurt.SetActive(false);
        SlimeDeath.SetActive(false);
    }

    void Jump()
    {
        SlimeIdle.SetActive(false);
        SlimeJump.SetActive(true);
        SlimeHurt.SetActive(false);
        SlimeDeath.SetActive(false);
    }
    
    void Hurt()
    {
        SlimeIdle.SetActive(false);
        SlimeJump.SetActive(false);
        SlimeHurt.SetActive(true);
        SlimeDeath.SetActive(false);
        Invoke("Reset", 1);
    }

    private void Reset()
    {
        Idle();
    }

    void Death()
    {
        isDead = true;
        SlimeIdle.SetActive(false);
        SlimeJump.SetActive(false);
        SlimeHurt.SetActive(false);
        SlimeDeath.SetActive(true);
    }

    private void OnDestroy()
    {
        audio.Play();
    }


}
