using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JonnyBoySprite : MonoBehaviour
{

    //If I wanna make it so that he moves at mach speed then I gotta make it so that he builds up until he reaches the point he wants to reach
    //Nevermind he just moves like skeletron from terraria now



    Collider2D[] hitColliders; //All objects detected
    float smald; //Smald is the distance of the object with the shortest distance that is also within range. 

    [SerializeField]
    Transform Jonny;
    
    [SerializeField]
    Transform CharactersPoint;
    GameObject killThisOne;//skill this one is the object with the shortest distance to the character
    public LayerMask whatIsKill; //What the character attacks and moves towards
    bool stopThatJank = false;

    public float lineOfSight; //The aggroed see value

    bool rotateJonny;

    public float rotateSpeed = 5;



    [SerializeField]
    float timeBeforeAttack = 15;
    [SerializeField]
    float timer;
    Rigidbody2D JRB;
    // Start is called before the first frame update
    void Awake()
    {
        timer = -timeBeforeAttack;

        JRB = Jonny.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        hitColliders = Physics2D.OverlapCircleAll(transform.position, lineOfSight, whatIsKill);


        if (hitColliders.Length >= 1)
        {
            stopThatJank = false;
            FindShortestEnemy();
            if (killThisOne != null)
            {
                HuntingAi();
            }

            else
            {

                print("Null???");

            }

        }
        else
        {
            rotateJonny = false;

            if (stopThatJank == false) { 
            stopThatJank = true;
            JRB.velocity = Vector2.zero;
                timer = -timeBeforeAttack;
                

            }
            Jonny.position = Vector2.Lerp(Jonny.position, transform.position, Time.deltaTime * 1);

        }





    }


    private void FixedUpdate()
    {
        if(rotateJonny)
        {
            Jonny.Rotate(Vector3.forward, rotateSpeed);


        }


    }


    void FindShortestEnemy()//sifts through enemies in the array until it finds the closest one
    {

        smald = Vector3.Distance(hitColliders[0].transform.position, Jonny.position);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            float d = Vector3.Distance(hitColliders[i].transform.position, Jonny.position);

            if (d < smald)
                smald = d;




            if (Vector3.Distance(hitColliders[i].transform.position, Jonny.position) <= smald)
            {
                killThisOne = hitColliders[i].gameObject;
            }
        }



    }


    void HuntingAi()
    {

        timer += Time.deltaTime;
        //before 0 it hovers

        //after 0 halfway through runThrough it will slowly grow

        //it will cap at a certain amount and move without rotating anymore

        //If timer > 0 then
        //Jonny.direction to player
        // Johnny moves to point
        //Else if jonny < time/2 then move towards without rotation
        //else then move and rotate

        
        CharactersPoint.position = killThisOne.transform.position;

        if (timer < 0)
        {
            Vector2 direction = killThisOne.transform.position - Jonny.position;

            Jonny.up = -direction;

            Jonny.position = Vector2.Lerp(Jonny.position, CharactersPoint.GetChild(0).position, Time.deltaTime * 1);

            rotateJonny = false;


        }


        else
        {
            rotateJonny = true;
            



           Jonny.position = Vector2.MoveTowards(Jonny.position,killThisOne.transform.position,Time.deltaTime*5);



            if (timer > timeBeforeAttack*1.5f)
            {
                timer = -timeBeforeAttack * 1.5f;
                JRB.velocity = Vector2.zero;
            }




        }





    }

    /*
    
    This is the old hunting AI which will be a surprise tool that'll help us for later
    void JonnyOrgHuntingAI()
    {
        

        timer += Time.deltaTime;
        //before 0 it hovers
        
        //after 0 halfway through runThrough it will slowly grow

        //it will cap at a certain amount and move without rotating anymore

        //If timer > 0 then
        //Jonny.direction to player
        // Johnny moves to point
        //Else if jonny < time/2 then move towards without rotation
        //else then move and rotate

        CharactersPoint.position = killThisOne.transform.position;

        if(timer<0)
        { 
        Vector2 direction = killThisOne.transform.position - Jonny.position;

        Jonny.up = -direction;

        Jonny.position = Vector2.Lerp(Jonny.position, CharactersPoint.GetChild(0).position, Time.deltaTime * 1);
        }


        else
        {


            if (Vector3.Magnitude(JRB.velocity) < 10)
                JRB.AddForce(-Jonny.up * 10f);

 

            if (timer > timeBeforeAttack)
            {
                timer = -timeBeforeAttack*2;
                JRB.velocity = Vector2.zero;
            }

            


        }







    }
      

    */





    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);

        
    }

 

}
