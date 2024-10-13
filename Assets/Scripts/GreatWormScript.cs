using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatWormScript : MonoBehaviour
{





    //If I wanna make it so that he moves at mach speed then I gotta make it so that he builds up until he reaches the point he wants to reach
    //You might notice this is called great worm.
    //It was supposed to be a worm
    //it is now a bird/harpy

        Vector3 startPos;

        Collider2D[] hitColliders; //All objects detected
        float smald; //Smald is the distance of the object with the shortest distance that is also within range. 

        [SerializeField]
        Transform Worm;

        [SerializeField]
        Transform CharactersPoint;
        GameObject killThisOne;//skill this one is the object with the shortest distance to the character
        public LayerMask whatIsKill; //What the character attacks and moves towards
        bool stopThatJank = false;



        [SerializeField]
        float maxSpeed;


       // public float inWallsView; (saving this incase I make a worm again)
        public float lineOfSight; //The aggroed see value



        public float rotateSpeed = 5;

        public LayerMask whatIsGround;

        [SerializeField]
        float timeBeforeAttack = 15;
        [SerializeField]
        float timer;
        Rigidbody2D JRB;
        Animator anim;

        public float birdieOffset = 1;
        // Start is called before the first frame update
        void Awake()
        {
            timer = -timeBeforeAttack;
            startPos = Worm.position;
            JRB = GetComponentInChildren<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {

        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        float offSet = Random.Range(1f, birdieOffset);


        JRB.AddForce(new Vector2(randX, randY) * birdieOffset, ForceMode2D.Impulse);
        //JRB.AddForce(new Vector2(randX, randY) * birdieOffset, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
        {

        //Animations
        if (Worm.eulerAngles.z > 0 && Worm.eulerAngles.z < 180)
            anim.SetBool("FacingUp", false);
        else
            anim.SetBool("FacingUp", true);

        hitColliders = Physics2D.OverlapCircleAll(transform.position, lineOfSight, whatIsKill);
        //bool inTheWalls = Physics2D.OverlapCircle(Worm.position, inWallsView, whatIsGround);


                JRB.gravityScale = 0;
                



            if (hitColliders.Length >= 1)
            {
                
                FindShortestEnemy();

            }
        else
        {
            Vector2 direction = startPos - Worm.position;
            Worm.right = -direction;


            JRB.AddForce(direction,ForceMode2D.Force);
            if (JRB.velocity.magnitude > maxSpeed)
            {
                JRB.velocity = Vector3.ClampMagnitude(JRB.velocity, maxSpeed);
            }

        }







        }


        private void FixedUpdate()
        {
            if (killThisOne != null)
            {
                HuntingAi();
            }


        }


        void FindShortestEnemy()//sifts through enemies in the array until it finds the closest one
        {
            smald = Vector3.Distance(hitColliders[0].transform.position, Worm.position);

        for (int i = 0; i < hitColliders.Length; i++)
            {
                float d = Vector3.Distance(hitColliders[i].transform.position, Worm.position);

                if (d < smald)
                    smald = d;




                if (Vector3.Distance(hitColliders[i].transform.position, Worm.position) <= smald)
                {
                    killThisOne = hitColliders[i].gameObject;
                }
            }



        }


        void HuntingAi()
        {

                Vector2 direction = killThisOne.transform.position - Worm.position;

                Worm.right = -direction;
                

                JRB.AddForce(direction);

        
        
                if(JRB.velocity.magnitude > maxSpeed)
                {
                    JRB.velocity = Vector3.ClampMagnitude(JRB.velocity,maxSpeed);
                }



        }

    public void onLowerColliderActive(Collision2D col)
    {
        anim.SetTrigger("Bump");


    }



    public void onLowerTriggerActive(Collision2D col)
    {
    }




    private void OnDrawGizmos()
        {

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, lineOfSight);

    }



    }