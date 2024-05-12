using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Controller_Enemy
{
    private GameObject player;

    private Rigidbody rb;

    private Vector3 direction;

    public float stopPoint;

    public bool stoped = false;
    public bool reduced = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Controller_Player._Player != null)
        {
            player = Controller_Player._Player.gameObject;
        }
        else
        {
            player = GameObject.Find("Player");
        }
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (player != null)
        {
            direction = -(this.transform.localPosition - player.transform.localPosition).normalized;
        }
        base.Update();
        Stop();
    }

    void FixedUpdate()
    {
        if (stoped) 
            direction.Scale(Vector3.up);
        
        if (player != null)
            rb.AddForce(direction * enemySpeed);

    }

    private void Stop()
    {
        Debug.Log(direction);

        if (this.transform.position.x < stopPoint && !stoped)
        {
            stoped = true;
            rb.velocity = Vector3.zero;
        }
    }

    public override void ShootPlayer()
    {
        if (Controller_Player._Player != null)
        {
            if (shootingCooldown <= 0)
            {
                Instantiate(enemyProjectile, transform.position, Quaternion.identity);
                shootingCooldown = UnityEngine.Random.Range(1, 6);
            }
        }
    }
}