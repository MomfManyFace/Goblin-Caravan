using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollSpawner : MonoBehaviour
{
    
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    GameObject ToSpawn;
    [SerializeField]
    GameObject Spawned;
    [SerializeField]
    DeleteBoxScript del;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Goblin") && Spawned == null)
        {
            
            Spawned  = Instantiate(ToSpawn,spawnPoint.position,Quaternion.identity);
            del.ToDelete = Spawned;

        }

    }

}
