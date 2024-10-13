using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyStomper : MonoBehaviour
{
    [SerializeField]
    Transform boxPos;


    [SerializeField]
    Animator anim;

    [SerializeField]
    Vector3 boxSize;


    public LayerMask gobbos;


    // Start is called before the first frame update
    void Awake()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //Lets be real this is easier than what I tried doing before
        anim.SetBool("GobboInRange", Physics2D.OverlapBox(boxPos.position, boxSize, 0, gobbos));

        
        /*
        //Player enters radius then it drops
        //freeze x
        //translate upwards


        //Arise if felled

        if(felledTimer > 0)
        {

            if(SillyStomp.localPosition.y < originalPos.y)
            {
                sillyRB.velocity = Vector2.up * 5;
                print("Going Up");

            }

            else
            {
                SillyStomp.localPosition = originalPos;

                sillyRB.constraints = RigidbodyConstraints2D.FreezeAll;
                print("Setting Pos");

            }




        }

        else if ()
        {
            print("Ope Falling");
            sillyRB.constraints = RigidbodyConstraints2D.FreezePositionX;





        }

        if (SillyStomp.localPosition.y <= -6)
        {
            felledTimer = maxTime;


        }


        */

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(boxPos.position, boxSize);


    }
        
}
