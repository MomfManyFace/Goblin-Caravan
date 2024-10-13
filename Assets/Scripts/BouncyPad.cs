using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPad : MonoBehaviour
{
    [SerializeField]
    private float bouncePower=1f;
    [SerializeField]
    private float timer;
    public float timeBeforeJump = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        
        if (timer > 0)
            timer -= Time.deltaTime;


        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("Goblin"))
        {
            if (timer <= 0)
            {

                print("Brokorka");
                collision.gameObject.GetComponentInParent<GoblinAI>().airTimer = -1;
                collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity = (Vector2.up * bouncePower);

                timer = timeBeforeJump;
            }
           
        
        
        }

    }


}
