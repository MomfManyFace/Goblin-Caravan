using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownSiftScript : MonoBehaviour
{
    /// <summary>
    /// Here what we have is the script that moves downcheckers down
    /// or is it downsifter? I literally cannot remember
    /// which one is which
    /// Dude I really should've made the names more clear
    /// </summary>





    [SerializeField]
    GameObject DownChecker;

    public ArrayList validPos = new ArrayList();

    [SerializeField]
    int twoLenghts;//Creates objects in both directions
    //If you try to make this odd fuck you im adding one
    [SerializeField]
    int iterations;

    // Start is called before the first frame update
    void Awake()
    {

        if (twoLenghts % 2 != 0)
            twoLenghts++;
        
        //Hypothetical scenario
        //65 indexes
        //Ends at 64
        //which means 33 is median and 32 is center
        //I get confused with calculating medians in code

        GameObject[] DownCheckArray = new GameObject[twoLenghts*2+1];
        DownCheckArray[twoLenghts] = Instantiate(DownChecker, transform.position, Quaternion.identity, transform);

        int x = -twoLenghts; 

        //left side instantiation goes to 31 (at least should)
        for (int i=0; i < twoLenghts; i++)
        {
            
            DownCheckArray[i] = 
            Instantiate(DownChecker, 
                new Vector2(transform.position.x + x, transform.position.y), 
                Quaternion.identity, transform);
            x++;

        }

        x = 0;

        for (int i = 1; i <= twoLenghts; i++)
        {
            x++;

            DownCheckArray[i+twoLenghts] =
            Instantiate(DownChecker,
                new Vector2(transform.position.x + x, transform.position.y),
                Quaternion.identity, transform);


        }



        for (int i=0; i < DownCheckArray.Length; i++)
        {
            if(i!= 0)
            DownCheckArray[i].GetComponent<DownCheckerScript>().prev = DownCheckArray[i-1];

            if(i < DownCheckArray.Length - 1)
            DownCheckArray[i].GetComponent<DownCheckerScript>().next = DownCheckArray[i + 1];

        }

    }


    private void Start()
    {
        StartCoroutine("Fall");

    }


    IEnumerator Fall()
    {
        int i = 0;
        
        //Iterations is how many times the checker is going to go down to check stuff
        while(i <= iterations)
        { 

                BroadcastMessage("CheckMeSelf"); //This sends a message to the base object tells it to see if its in a valid spot
                
                BroadcastMessage("CheckMeMateys");//This one checks if the ones next to it are also valid

                //The reason why we do them seperately is cuz unity update function doesnt like it otherwise so I need to make them distinct
                //That way they run in order

                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                i++;

            yield return null;
        }
        if(validPos.Count != 0)
        {
            
            int rand = Random.Range(0, validPos.Count);
            Debug.Log(gameObject + ": Found valid Pos at" + validPos[rand]);

            SendMessageUpwards("MoveHag",validPos[rand]);
            

        }
        else { 
            SendMessageUpwards("MoveHag", new Vector3(256,256,256));
            Debug.Log(gameObject + ": No valid pos, L...");
        }
        Debug.Log(gameObject + ": Destroying...");
        Destroy(gameObject);

    }


}
