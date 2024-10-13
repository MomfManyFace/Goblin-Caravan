using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAesthetics : MonoBehaviour
{
    Animator anim;
    AudioSource aud;
    GoblinAI gob;

    [SerializeField]
    AudioClip[] GobboSounds = new AudioClip[20];
    
    
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        gob = GetComponent<GoblinAI>();
    }

    private void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        anim.speed = Mathf.Abs(gob.moveDi);
    }
}
