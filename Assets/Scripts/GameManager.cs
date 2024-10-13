using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //There are no comments in this code yet
    //Have fun reading it

    [SerializeField]
    GameObject MouseCursor;
    [SerializeField]
    Camera cam;
    [SerializeField]
    GameObject Gobbo;
    [SerializeField]
    Transform spawner;
    [SerializeField]
    Text gobboTextCount;
    [SerializeField]
    Image closerScreen;

    bool pressedRestart = true;

    string goblinsAmount = "Goblins: ";
    int goblinCount;

    [SerializeField]
    bool sandboxMode = false;

    [SerializeField]
    int state = 0;

    [SerializeField]
    float blackScreenSpeed = 1f;

    float alpha = 1;

    float timer = .2f;
    int spawnCount = 0;



    Animator anim;


    Queue<AudioSource> deadAudio = new Queue<AudioSource>();

    private void Awake()
    {
        anim = GetComponent<Animator>(); 

    }

    // Start is called before the first frame update
    void Start()
    {
        gobboTextCount.text = (goblinsAmount + goblinCount);

        StartCoroutine("OpenScreen");



    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();


        }

        
        Vector2 mouse = Input.mousePosition;

        MouseCursor.transform.position = (Vector2)cam.ScreenToWorldPoint(mouse);
        

        if(deadAudio.Count > 0)
        { 
            if(deadAudio.Peek() == null)
            {
                deadAudio.Dequeue();
            }
        }

        if(pressedRestart == false)
        {
            if (sandboxMode)
            {

                if (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.G))
                {
                    Instantiate(Gobbo, spawner.position, Quaternion.identity);
                    goblinCount++;

                    gobboTextCount.text = (goblinsAmount + goblinCount);


                }



            }


            else if (state == 0)
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    goblinCount = 20;
                    gobboTextCount.text = (goblinsAmount + goblinCount);
                    state = 1;

                }

            }




            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartLevel();


            }

        }


    }


    private void FixedUpdate()
    {

        if(state == 1)
        spawnGoblins();



    }


    public void GoblinDied(AudioSource aud)
    {
        goblinCount--;
        gobboTextCount.text = (goblinsAmount + goblinCount);

        deadAudio.Enqueue(aud);
        if(deadAudio.Count > 6)
        {
            
           deadAudio.Peek().mute  = true;
            deadAudio.Dequeue();


        }

        if(!sandboxMode && goblinCount <= 0)
        {
            anim.SetTrigger("JustDied");

        }




    }


    void spawnGoblins()
    {
        
        

        if(spawnCount < goblinCount)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;

            }
            else { 
            Instantiate(Gobbo, spawner.position, Quaternion.identity);
            spawnCount++;
            timer = .1f;
            }

        }
        else
        state = 2;
        

    }

    public void RestartLevel()
    {
        

        if(pressedRestart == false) { 
        StartCoroutine("RestartLevelRoutine");
            anim.SetTrigger("JustRestarted");
            pressedRestart = true;
        }

        

    }

    public IEnumerator RestartLevelRoutine()
    {

        while (closerScreen.color.a < 1)
        {
            alpha += Time.deltaTime * blackScreenSpeed;


            closerScreen.color = new Color(0, 0, 0, alpha);



           
            yield return null;
        
        }


        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);




    }

    IEnumerator OpenScreen()
    {

        while (closerScreen.color.a > 0)
        {
            alpha -= Time.deltaTime* blackScreenSpeed;


            closerScreen.color = new Color(0, 0, 0, alpha);

            yield return null;

        }

        pressedRestart = false;

    }




}
