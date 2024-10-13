using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GoblinAI : MonoBehaviour
{





    //Instantiated scribts

    Rigidbody2D rb;
    

    [Header ("Detection variables")]
    GameObject killThisOne;//skill this one is the object with the shortest distance to the character
    float smald; //Smald is the distance of the object with the shortest distance that is also within range. 



    //This will not be implemented yet (ever)
    
    public LayerMask whatIsKill; //What the character attacks and moves towards
    Collider2D[] hitColliders; //All objects detected

    [Header("Movement variables")]
    public Vector2 velocity;
    public float moveDi; //The value that determines the direction of the character(Should never go above 1)
    public float moveSpeed; //The value which determines how fast the character can go.
    public float changeSpeed = 1; //This value allows for you to determine how fast the character can switch/stop in their tracks


    [Header("Jump Values")]
    public float jumpForce;
    public int jumpingMode;
    public float airTimer;
    public float orgAirTimer;

    [Header("Crankin Brainkin")]
    bool manGoTheOthaWay;
    public float bored;
    public float borCount;
    bool wasFacingRight;
    bool inDaTrees;


    [Header("Floor Collider Stuff")]
    public bool isGrounded;
    public bool isWalled;
    public Transform floorChecker;
    public Transform chasmChecker;
    public float checkRadius;
    public LayerMask whatIsGround;
    public Vector2 chasmHeight;
    bool theChasm;
    public float rayHeight;

    bool isDead = false;

    public float gobboAbove;



    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        killThisOne = GameObject.FindGameObjectWithTag("Cursor");
            

    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false) { 
        Checkers(); //All of the check variables to determine what the goblin ai does
        
        WhyYouShouldJump(); //checks if goblin jumps
        getThatMf();

/*
        //THis method looks for the enemy target 
        if(hitColliders.Length == 1)
        {
            killThisOne = hitColliders[0].gameObject;
            //print("I see yo ass " + killThisOne);
        }
        else if(hitColliders.Length >= 1)
        {

            FindShortestEnemy();

            //print("I see yo ass " + killThisOne);

        }
*/

  //      if (manGoTheOthaWay == false)
           

    //    else
      //      LEAVE();


        }

    }






    
    private void FixedUpdate()
    {
        if (isDead == false)
        { 
        if (jumpingMode == 2)
            rb.velocity = Vector2.up * jumpForce;

            //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); Dont remember why this is here

            rb.velocity = new Vector2(moveDi * moveSpeed, rb.velocity.y);
            //rb.MovePosition((Vector2)transform.position + velocity * moveSpeed); Dont remember this either


        }

        if (this.transform.position.y < -22) 
        {
            this.transform.position = new Vector2(-30, -21);


        }



    }
    
    void WhyYouShouldJump()
    {


        if (isWalled == true || theChasm == false || inDaTrees == true)
        {
            jump();
           
        }
        else if (airTimer <= 0)
        {

            jumpingMode = 3;
        }


    }




    void Checkers()
    {

        if (killThisOne == null)
            theChasm = Physics2D.OverlapCapsule(chasmChecker.transform.position, chasmHeight, 0, whatIsGround);

        else
        {
            
            if (killThisOne.transform.position.y > transform.position.y - 3)
            {
                theChasm = Physics2D.OverlapBox(chasmChecker.transform.position, chasmHeight, 0, whatIsGround);
                
            
            }
            else
                theChasm = true;
        }

        if(killThisOne != null)
            inDaTrees = jumpinHeight();

        isGrounded = Physics2D.OverlapCircle(floorChecker.transform.position, checkRadius, whatIsGround);
        RaycastHit2D ray = (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + rayHeight), transform.TransformDirection(new Vector2(moveDi,0)), 1, whatIsGround));

        if (ray.collider != null)
        {
            Debug.DrawLine(new Vector2(transform.position.x, transform.position.y + rayHeight), ray.point, Color.red);
            isWalled = true;
        }

        else
        {
            Debug.DrawLine(new Vector2(transform.position.x, transform.position.y + rayHeight), new Vector2(transform.position.x + (moveDi * .5f), transform.position.y + rayHeight), Color.red);
            isWalled = false;
        }
        

        if(isWalled == true && bored > 0)
        {
            bored -= Time.deltaTime;
        }
        else if (bored != borCount)
        {
            bored = borCount;
        }

        //print(inDaTrees);


    }


    bool jumpinHeight() //Each block in this game is about 1 unit big. The ai can jump 1 unit per 2.5 value going for 2.5x + 5 = y 
        //(Try to stay away from any value below 10 for jump height)
    {

        //this is named poorly this is the fucking height it checks you fucking moron
        //Maybe name it something like "JumpHeightCheck"? Idk what is wrong with you 2022 Nigeria
        int maxJumpHeight = (int)((jumpForce - 5)/ 2.5);//it used to be called "hereYouAre" WHY????


        //Xpos is left and right distance, it wont jump if its more than 1 block away
        float xPos = Math.Abs( transform.position.x - killThisOne.transform.position.x);


        if (killThisOne.transform.position.y <= transform.position.y + maxJumpHeight &&
            killThisOne.transform.position.y > transform.position.y + gobboAbove
            && xPos < 1)
            return true;

        else
            return false;




    }


    /*
    void FindShortestEnemy()//sifts through enemies in the array until it finds the closest one
    {

        smald = 30; //30 is max distance it can see iirc

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


    void LEAVE()
    {
        flipper();
        if (wasFacingRight == true)
        {

            if (moveDi == -1)
            { }

            else if (velocity.x > -1)
                moveDi -= Time.deltaTime * changeSpeed;

            else if (moveDi < -1)
            {
                moveDi = -1;

            }
        }





        else//This moves left
        {
            if (moveDi == 1)
            { }

            else if (moveDi < 1)
                moveDi += Time.deltaTime * changeSpeed;

            else if (moveDi > 1)
            {
                moveDi = 1;

            }
        }



        velocity.x = moveDi;


    }

    */


    void getThatMf()//Moves towards the enemy unless enemy is null
    {
        flipper();
        

        //All of these check if the character is moving towards the object

        if (!killThisOne)//This one just moves in an aimless direction 
        {
            if(moveDi >= 0)
            {

                if (moveDi == 1)
                { }

                else if (velocity.x < 1)
                    moveDi += Time.deltaTime * changeSpeed;

                else if (moveDi > 1)
                {
                    moveDi = 1;

                }
            }





            else//This moves left
            {
                if (moveDi == -1)
                { }

                else if (moveDi > -1)
                    moveDi -= Time.deltaTime * changeSpeed;

                else if (moveDi < -1)
                {
                    moveDi = -1;

                }
            }

        }



        else if(killThisOne.transform.position.x > transform.position.x)//This moves right if object is right
        {



            if (moveDi == 1 )
            { }

            else if( velocity.x < 1)
            moveDi += Time.deltaTime * changeSpeed;
        
            else if (moveDi > 1)
            {
                moveDi = 1;

            }

                
                
         }

        else if (killThisOne.transform.position.x < transform.position.x)//This moves left if object is left
        {
            if (moveDi == -1)
            { }

            else if (moveDi > -1)
                moveDi -= Time.deltaTime * changeSpeed;

            else if (moveDi < -1)
            {
                moveDi = -1;

            }
        }


        if (moveDi > 0)
            wasFacingRight = true;
        else if (moveDi < 0)
            wasFacingRight = false;

            velocity.x = moveDi;





    }


    //You can use this for climbing as well without is grounded
    
    void jump()//Guess what this does
    {
        //print(jumpingMode);

        //As soon as the jump button is pressed, it runs these codes and turnes into jump mode 2
        if (jumpingMode == 1)
        {

            jumpingMode = 2;
            airTimer = orgAirTimer;
            StartCoroutine("AirJumpTimer");



        }

        else if (isGrounded == true && jumpingMode == 3)
        {
            jumpingMode = 1;
        }


        //When the player lets go, or the airtimer is 0, the player will fall again. 
        else if (airTimer <= 0) //Or if player is below 
        {
            
            jumpingMode = 3;
        }







    }


    void flipper()
    {
        if (moveDi > 0)
            transform.localScale = new Vector3(1, 1, 1);

        else if (moveDi < 0)
            transform.localScale = new Vector3(-1, 1, 1);

    }


    IEnumerator AirJumpTimer()

    {
        while (airTimer > 0)
        {
            airTimer -= Time.deltaTime;
            yield return null;
        }

    }




    /*
    public void KillThisMotherFucka()
    {
        isDead = true;
        

        float time = 5;


        while (true || 1 == 1 || 69 != 2 ) { 

        time -= Time.deltaTime;
        print(time);

        if (time < 0)
        {
            Destroy(gameObject);
        }



        }

    }


    private IEnumerator DieTime()
    {


        yield return null;

    }

    */
    
     
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawWireSphere(floorChecker.position, checkRadius);
        Gizmos.DrawWireCube(chasmChecker.position, chasmHeight);
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + gobboAbove),.1f);

    }

 



}
