using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellScriptTrigger : MonoBehaviour
{
    //
    /// <summary>
    ///The hitbox is infact not on the object with a parent script, 
    ///this is why I use this script
    /// </summary>


    [SerializeField]
    GameObject objectRef;


    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        objectRef.SendMessage("onLowerTriggerActive", collision);
        


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        objectRef.SendMessage("onLowerColliderActive", collision);


    }



}
