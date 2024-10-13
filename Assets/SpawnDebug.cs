using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebug : MonoBehaviour
{
    [SerializeField]
    GameObject Debugger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y) && Debugger != null)
        {
            Debug.Log("So you debugging with da " +Debugger +" now?");
            Instantiate(Debugger,transform.position,Quaternion.identity);

        }
    }
}
