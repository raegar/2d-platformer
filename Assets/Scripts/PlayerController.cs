using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public delegate void OnHitSpikeAction();
    public delegate void OnHitEnemyAction();
    public delegate void OnHitOrbAction();

    public OnHitEnemyAction OnHitEnemy;
    public OnHitSpikeAction OnHitSpike;
    public OnHitOrbAction OnHitOrb;

    float speed = 1000;
    float jumpSpeed = 400;

    Vector3 leftBound;
    Vector3 rightBound;
    bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput() {
        if(Input.GetKey("left") || Input.GetKey("a")) 
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed * Time.deltaTime);
        }

        if(Input.GetKey("right") || Input.GetKey("d")) 
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * Time.deltaTime);
        }

        if(Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up")) 
        {
            Jump();
        }
    }

    void Jump(bool force = false)
    {
        if(canJump || force)
        {
            canJump = false;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bound") 
        {
            canJump = true;
        }

        if(collision.gameObject.GetComponent<SpikeController>() != null)
        {
            if(OnHitSpike != null) 
            {
                OnHitSpike();
            }
        }

        else if(collision.gameObject.GetComponent<EnemyController>() != null)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

            if(this.transform.position.y > enemy.transform.position.y + enemy.GetComponent<BoxCollider2D>().size.y / 2)
            {
                GameObject.Destroy(collision.gameObject);
                Jump(true);

                if (OnHitEnemy != null)
                {
                    OnHitEnemy();
                }
            }
            else { //run the event that will kill the player
                if(OnHitSpike != null) 
                {
                    OnHitSpike();
                }
            }
        } else if (collision.gameObject.GetComponent<OrbController>() != null) {
            if(OnHitOrb != null) 
            {
                OnHitOrb();
            }
        }
    }
}
