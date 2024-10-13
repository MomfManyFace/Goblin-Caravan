using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public bool PassesThrough = false;
    public float bulletLife = -16;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(-transform.up * speed * 50, ForceMode2D.Impulse);

    }

    private void Update()
    {   if (bulletLife > 0)
            bulletLife -= Time.deltaTime;

        else if (bulletLife > -16)
        {
            Destroy(gameObject);

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (rb.velocity.magnitude > speed)
            Vector3.ClampMagnitude(rb.velocity, speed); 

    }

    public void onLowerTriggerActive(Collider2D collision)
    {
        if(PassesThrough != true)
        Destroy(gameObject);

    }


}
