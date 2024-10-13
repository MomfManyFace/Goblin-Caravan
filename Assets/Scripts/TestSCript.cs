using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSCript : MonoBehaviour
{
    //I think this used to be the old monkey script


    [SerializeField]
    Transform Parent;

    [SerializeField]
    Transform fist;

    //45 and 135 above
    //negative otherwise
    //225 and 315 below

    [SerializeField]
    Transform Shoulder;

    [SerializeField]
    Transform CheckerPoint;

    bool handOnFloor;

    [SerializeField]
    float rotateSpeed;

    [SerializeField]
    float angleCheck;
    [SerializeField]
    float CheckerRadius;
    [SerializeField]
    Vector2 feetChecker;
    public LayerMask whatIsGround;

    bool tillRotated = true;
    GameObject[] hitColliders; //All objects detected

    public float lineOfSight;
    public LayerMask whatIsKill;

    bool flipped = false;

    GameObject killThisOne;//skill this one is the object with the shortest distance to the character

    float moveDir = 1;

    float smald; //Smald is the distance of the object with the shortest distance that is also within range. 

    int leftOrRight;

    [SerializeField]
    float TimeBeforeCheck = 1;

    float checkAgainTimer;


    // Start is called before the first frame update
    void Awake()
    {
        checkAgainTimer = TimeBeforeCheck;
    }



    private void Update()
    {
        //print(flipped);
        hitColliders = GameObject.FindGameObjectsWithTag("Goblin");

        if (hitColliders.Length >= 1)
        {
            FindShortestEnemy();

            if (checkAgainTimer > 0)
                checkAgainTimer -= Time.deltaTime;
            else
                checkAgainTimer = TimeBeforeCheck;


           
                if(!flipped)
                {
                
                if (killThisOne.transform.position.x < Shoulder.position.x && checkAgainTimer < 0)
                    flipped = true;

                    leftOrRight = -((int)fist.position.x - (int)Shoulder.position.x);
                    moveDir = -rotateSpeed;


                }


                else
                {




                if (killThisOne.transform.position.x > Shoulder.position.x && checkAgainTimer < 0)
                    flipped = false;

                    leftOrRight = (int)fist.position.x - (int)Shoulder.position.x;
                    moveDir = rotateSpeed;


                }









        }


            

    }





    // Update is called once per frame
    void FixedUpdate()
    {

        if (killThisOne)
        {
            if (handOnFloor)
                rotateAroundFist();
            else
                rotateAroundBody();
        }
        else
        {
            ReturnToSlumber();
        }
        

       

       // print(Shoulder.eulerAngles);

    }

    void rotateAroundBody()
    {
        Shoulder.Rotate(Vector3.forward, moveDir);
        if (leftOrRight < 0)
            tillRotated = true;
        

        if (Physics2D.OverlapCircle(fist.position, CheckerRadius, whatIsGround) && tillRotated)
        {
            handOnFloor = true;
            tillRotated = false;
        }


    }

    void rotateAroundFist()
    {

        Shoulder.RotateAround(fist.position, Vector3.forward, moveDir);

        if (leftOrRight > 0)
            tillRotated = true;

        if (Physics2D.OverlapCapsule(CheckerPoint.position, feetChecker, CapsuleDirection2D.Horizontal, 0, whatIsGround) && tillRotated)
        {
            handOnFloor = false;
            tillRotated = false;
        }


    }


    void FindShortestEnemy()//sifts through enemies in the array until it finds the closest one
    {

        smald = Vector3.Distance(hitColliders[0].transform.position, Shoulder.position);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            float d = Vector3.Distance(hitColliders[i].transform.position, Shoulder.position);

            if (d < smald)
                smald = d;




            if (Vector3.Distance(hitColliders[i].transform.position, Shoulder.position) <= smald)
            {
                killThisOne = hitColliders[i].gameObject;
            }
        }



    }



    void ReturnToSlumber()
    {

        if(!Physics2D.OverlapCapsule(CheckerPoint.position, feetChecker, CapsuleDirection2D.Horizontal, 0, whatIsGround))
        {
            Shoulder.RotateAround(fist.position, Vector3.forward, moveDir);
        }


    }


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(fist.position, CheckerRadius);
        //Gizmos.DrawWireSphere(transform.position, lineOfSight);

    }






}
