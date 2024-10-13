using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mudScript : MonoBehaviour
{

    public float muddy;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Goblin"))
        {
            collision.gameObject.GetComponentInParent<Rigidbody2D>().drag = muddy;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goblin"))
        {
            collision.gameObject.GetComponentInParent<Rigidbody2D>().drag = 2;
        }
    }


}
