using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDeadGobbo : MonoBehaviour
{



    [SerializeField]
    GameObject[] deadPart;

    [SerializeField]
    Sprite[] sprites = new Sprite[7];

    [SerializeField]
    float boing;


    AudioSource src;
    float timer = 1;




    private void Awake()
    {
        src = this.GetComponent<AudioSource>();
    }



    // Start is called before the first frame update
    void Start()
    {
        src.Play();
        for (int i = 0; i <= 6; i++)
        {
            int rand = Random.Range(0,360);
           

            GameObject currentPart = deadPart[i];
            
         
            currentPart.transform.eulerAngles = new Vector3(0,0,rand);

    

            currentPart.GetComponent<SpriteRenderer>().sprite = sprites[i];

            float randX = Random.Range(-1f,1f);
            float randY = Random.Range(-1f, 1f);
         


            currentPart.GetComponent<Rigidbody2D>().AddForce(new Vector2(randX,randY) * boing,ForceMode2D.Impulse);


        }


    }

    // Update is called once per frame
    void Update()
    {
        
    
        timer -= Time.deltaTime / 5;


        foreach (GameObject i in deadPart)
        {
            i.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, timer);
            

        }

        if(timer < 0)
        {
            Destroy(gameObject);


        }


    }
}
