using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HagCode : MonoBehaviour
{
    //I actually tried learning how to use the debug log on this one
    //Aint that cool?


    [SerializeField]
    GameObject DownSifter;

    [SerializeField]
    GameObject KillOrb;

    GameObject currentSifter = null;
    [SerializeField]
    int offSet = 1;

    GameObject KillThisOne = null;

    public GameObject Testo;

    bool timerRunning;

    [SerializeField]
    float sitOffset;

    [SerializeField]
    float spawnAboveGoblin;

    public float timerStart = 1;
    float timer;
    int interval=0;


    Animator anim;

    //Shoots orbs at 1 interval
    //Do count down twice before shooting, 1 time to shoot, 3 times to tp again

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (KillThisOne != null)
        {
            if (timerRunning != true)
            {

                Debug.Log(gameObject + ": Starting Timer Coroutine");
                StartCoroutine("Timer");

            }
        }
        else
        {
            KillThisOne = GameObject.FindGameObjectWithTag("Goblin");

            if (KillThisOne == null) 
            {
                anim.SetBool("TP",false);
                
                timerRunning = false;
                StopCoroutine("Timer");
            }
            interval = 0;
            


        }




    }

    private IEnumerator Timer()
    {
        timer = timerStart;
        timerRunning = true;
        while (true)
        {
            Testo.transform.position = KillThisOne.transform.position;

            timer -= Time.deltaTime;

            if (currentSifter == null)
            {
                if (timer <= 0)
                {
                    Debug.Log(gameObject + ": Interval is" + interval);
                    // timer

                    if (interval < 4) 
                    { 
                    interval++;
                    timer = timerStart;
                    
                    }

                    else if (interval == 4)
                    {
                        
                        Debug.Log(gameObject + ": Shot projectile");
                        anim.SetTrigger("Shoot");

                        Vector2 direction = KillThisOne.transform.position - transform.position;

                        


                        GameObject orbInstance = Instantiate(KillOrb,transform.position, Quaternion.identity);

                        orbInstance.transform.up = -direction;

                        interval++;
                    
                        timer = timerStart;
                    
                    }
                    else if (interval < 8)
                    {

                        
                        interval++;

                        timer = timerStart;
                        if(interval == 7)
                            anim.SetBool("TP", true);


                    }



                    else
                    {
                        
                        Vector3 newSpawn = KillThisOne.transform.position;
                        newSpawn.y = Mathf.FloorToInt(newSpawn.y);
                        newSpawn.x = Mathf.CeilToInt((newSpawn.x));
                        
                        newSpawn.y = newSpawn.y + .5f + spawnAboveGoblin;

                        Debug.Log(gameObject + ": Teleporting Around " + newSpawn);

                        currentSifter = Instantiate(DownSifter, newSpawn, Quaternion.identity,transform);
                        

                        interval = 0;
                    
                        timer = timerStart;
                        StopCoroutine("Timer");
                        timerRunning = false;
                        



                    }
                    

                }
                

            }


            yield return null;
            

        }
    }


    public void MoveHag(Vector3 newPos)
    {
        if(newPos != new Vector3(256, 256, 256)) 
        { 
        newPos.y = newPos.y + sitOffset;

            transform.position = newPos;
            Debug.Log(gameObject + ": Moved to " + newPos);
            
        }


        else
        {
            newPos = transform.position;
            newPos.y = newPos.y + sitOffset;
            transform.position = newPos;
            
            Debug.Log(gameObject + ": Just Stayed Still");
            
        }

        anim.SetBool("TP", false);


    }


}
