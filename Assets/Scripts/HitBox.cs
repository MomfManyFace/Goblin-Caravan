using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    AudioClip[] GoblinKill;
    [SerializeField]
    GameObject instance;
    [SerializeField]
    GameManager gameMan;
    [SerializeField]
    AudioClip[] KillBoxSounds;

    [SerializeField]
    bool allBox = false;

    AudioSource src;

    private void Awake()
    {
        gameMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        src = this.GetComponent<AudioSource>();


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!allBox)
        if(collision.CompareTag("Goblin"))
        { 
            GameObject newInst = Instantiate(instance, collision.transform.position,this.transform.rotation);

              
            Destroy(collision.transform.parent.gameObject);
            int x = 0;

            gameMan.GoblinDied(newInst.GetComponent<AudioSource>());


            if(GoblinKill.Length > 0) 
            { 
                x = Random.Range(0, GoblinKill.Length);

                newInst.GetComponent<AudioSource>().clip = GoblinKill[x];
            }

            if (KillBoxSounds.Length > 0)
            {
                x = Random.Range(0, KillBoxSounds.Length);
                src.clip = KillBoxSounds[x];
                src.Play();
            }

        }




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!allBox)
        if (collision.gameObject.CompareTag(("Goblin")))
        {
            GameObject newInst = Instantiate(instance, collision.transform.position, this.transform.rotation);

            
            Destroy(collision.transform.gameObject);

            int x = 0;

            gameMan.GoblinDied(newInst.GetComponent<AudioSource>());


            if (GoblinKill.Length > 0)
            {
                x = Random.Range(0, GoblinKill.Length);

                newInst.GetComponent<AudioSource>().clip = GoblinKill[x];
            }

            if (KillBoxSounds.Length > 0 && !src.isPlaying)
            {
                x = Random.Range(0, KillBoxSounds.Length);
                src.clip = KillBoxSounds[x];
                src.Play();
            }

        }


    }

}
