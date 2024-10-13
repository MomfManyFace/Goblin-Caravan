using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterScript : MonoBehaviour
{
    Collider2D[] hitColliders; //All objects detected
    float smald;
    public float lineOfSight;
    public LayerMask whatIsKill;
    GameObject killThisOne; //skill this one is the object with the shortest distance to the character
    
    [SerializeField]
    Transform spittingPoint;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float timeBeforeShoot;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        hitColliders = Physics2D.OverlapCircleAll(transform.position, lineOfSight, whatIsKill);
        

        if (hitColliders.Length >= 1)
        {
            FindShortestEnemy();
           
        }
        else
        {
            
            killThisOne = null;

        }


    }
    private void FixedUpdate()
    {
        if (killThisOne != null)
        {
            
            
            HuntingAi();
        }
        else
        {
            
            timer = timeBeforeShoot;
            transform.rotation = new Quaternion(0, 0, 0, 0);

        }

    }
    void FindShortestEnemy()//sifts through enemies in the array until it finds the closest one
    {

        smald = Vector3.Distance(hitColliders[0].transform.position, transform.position);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            float d = Vector3.Distance(hitColliders[i].transform.position, transform.position);

            if (d < smald)
                smald = d;




            if (Vector3.Distance(hitColliders[i].transform.position, transform.position) <= smald)
            {
                killThisOne = hitColliders[i].gameObject;
            }
        }
    }




    void HuntingAi()
    {
        Vector2 direction = killThisOne.transform.position - transform.position;

        transform.up = -direction;

        timer -= Time.deltaTime;

        if(timer < 0)
        {
            Instantiate(bullet, spittingPoint.position, transform.rotation);
            timer = timeBeforeShoot;

        }

    }


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
       

    }

}
