using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
   
    public int maxJumps = 2;
    int jumpCount = 0;

    public int maxHearts = 3;
    private int currentHearts;
    public List<GameObject> UIHearts = new List<GameObject>();

    public Vector3 resetPoint;

    public LayerMask boxLayer;

    public float bumpForceUp = 50;
    public float bumpForceHorizontal = 100;
    public float InvincibilityTime = 3;
    protected override void Start()
    {
        base.Start();
        resetPoint = transform.position;
        currentHearts = maxHearts;
    }

    // Update is called once per frame
    void Update ()
    {
        Move();
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            Jump();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
        anim.SetBool("isGrounded", isGrounded);
    }



    public override void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        base.Move();
        anim.SetBool("isMoving", Mathf.Abs(horizontal) > 0);
    }

    public override void Jump()
    {
        base.Jump();
        jumpCount++;       
    }

    public override void OnGroundHit()
    {
        base.OnGroundHit();
        jumpCount = 0;
    }

    public override void Death()
    {
        base.Death();
        currentHearts--;
        UIHearts[currentHearts].SetActive(false);
        if(currentHearts <=0)
        {
            transform.position = resetPoint;
            ResetHearts();
        }
       
    }
    void ResetHearts()
    {
        currentHearts = maxHearts;
        foreach(GameObject heart in UIHearts)
        {
            heart.SetActive(true);
        }
    }

    public bool IsMaxHearts()
    {
        return currentHearts == maxHearts;
    }

    public void AddHeart()
    {
        if(currentHearts < maxHearts)
        {
            UIHearts[currentHearts].SetActive(true);
            currentHearts++;
        }
    }
    public override void OnEnemyHit(GameObject enemy)
    {
        base.OnEnemyHit(enemy);
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();
        //BoxCollider2D enemyCollider = enemy.GetComponent<BoxCollider2D>();
        //float heightDifference = transform.position.y - enemy.transform.position.y;
        float direction = enemy.transform.position.x - transform.position.x;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, myCollider.size * 0.9f, 0, new Vector2(direction, 0), 1, boxLayer);
        if(transform.position.y < enemy.GetComponent<Entity>().anim.transform.position.y || (hit && hit.collider.gameObject == enemy))
        {
            Vector3 force = new Vector3();
            force.x = Mathf.Sign(transform.position.x - enemy.transform.position.y) * bumpForceHorizontal;
            force.y = bumpForceUp;
            rb.AddForce(force);

       
            StopAllCoroutines();
            StartCoroutine("MakeInvinCible");


           Death();
        }
        else
        {
            enemy.GetComponent<Entity>().Death();
        }

  
    }
    IEnumerator MakeInvinCible()
    {
        gameObject.layer = 13;
        StartCoroutine(PlayInvinCibilityEffect());

        yield return new WaitForSeconds(InvincibilityTime);
        gameObject.layer = 9;
        StopCoroutine("PlayInvinCibilityEffect");
        anim.gameObject.GetComponent<SpriteRenderer>().enabled = true;

        }

    IEnumerator PlayInvinCibilityEffect()
    {
        bool toggle = false;
        while(true)
        {
            //anim.gameObject.SetActive(toggle);
            anim.gameObject.GetComponent<SpriteRenderer>().enabled = toggle;
             yield return new WaitForSeconds(0.5f);
            toggle = !toggle;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Kill Plane")
        {
            gameObject.SetActive(false);

            //levelManager.Respawn();
            Death();
            //transform.position = resetPoint;
            Debug.Log("Kill Plane");
            //transform.position = respawnPosition;


            StopCoroutine("PlayInvinCibilityEffect");
            anim.gameObject.GetComponent<SpriteRenderer>().enabled = true;

            gameObject.transform.position = resetPoint;

            gameObject.SetActive(true);
            anim.gameObject.SetActive(true);
        }


    }
}
