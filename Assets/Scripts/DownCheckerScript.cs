using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownCheckerScript : MonoBehaviour
{

    /// <summary>
    /// What is this script? Its quite simple actually (No it isnt)
    /// it spawns something that checks the area and finds a valid position that 
    /// a teleporting character can spawn to
    /// Why is this one of the only things with a summary?
    /// Because there's another script really similar to it
    /// I keep getting them confused
    /// </summary>

    [SerializeField]
    GameObject TEST;
   
    [SerializeField]
    GameObject DownChecker;
    [SerializeField]
    GameObject TooMuchChecker;
    [SerializeField]
    Vector2 checkerHeight;

    [SerializeField]
    float raycastSize = 0;

    public bool itsValid = false;
    bool itsFullyValid = false;

    public GameObject prev = null;
    public GameObject next = null;

    public LayerMask floor;







    // Start is called before the first frame update
    void Awake()
    {

    }


    public void CheckMeSelf()
    {

        if (Physics2D.Raycast(DownChecker.transform.position, Vector2.down, raycastSize, floor)
            &&
            !Physics2D.OverlapCapsule(TooMuchChecker.transform.position, checkerHeight, CapsuleDirection2D.Vertical, 0, floor))
            itsValid = true;

        else
            itsValid = false;

    }


    public void CheckMeMateys()
    {
        

        //This checks if the raycast is pointing down touching the floor but also if the collide finds any weirdos
        //Makes sure if theres enough space above to spawn and theres a spot below to spawn on

        if (next && prev)
        {
            //checks if the left and right stuff also feel the same way
            if (itsValid && next.GetComponent<DownCheckerScript>().itsValid && prev.GetComponent<DownCheckerScript>().itsValid)
            {
                GetComponentInParent<DownSiftScript>().validPos.Add(transform.position);

            }
            
        }

    }


    // Update is called once per frame
    void FixedUpdate()
    {



    }


    private void Update()
    {

    }

}
