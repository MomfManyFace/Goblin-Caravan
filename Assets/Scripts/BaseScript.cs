using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    //YOU DONT EVEN USE THIS BECAUSE THEY DIE IMMEDIATELY ANYWAYS RAAAAAAAAAAAAAAAGH

    public int[] values = new int[5];
    //1 health, 2 knockbackd ,3 knockbackr , 4 dam,  5 critchance
    Rigidbody2D rb;
    Collider2D[] hitEnemies; //All objects detected

    int health;
    int maxHealth;
    int knockbackD;
    int knockbackR;
    int damage;
    int critChance;

    public string enemy;
    public string you;
    public float iFrames = 1;
    public LayerMask hitbox;
    public Vector2 hitboxSize;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        maxHealth = values[0];
        knockbackD = values[1];
        knockbackR = values[2];
        damage = values[3];
        critChance = values[4];
        health = maxHealth;

    }



    void Update()
    {
        
        hitEnemies = Physics2D.OverlapBoxAll(transform.position, hitboxSize, 0, hitbox);
        foreach(Collider2D hitted in hitEnemies)
        {
            BaseScript bse2 = hitted.gameObject.GetComponentInParent<BaseScript>();

            if(bse2.you == enemy)
            {
                dealDamage(hitted.gameObject);



            }

        }


    }

    void dealDamage(GameObject fuckedUp)
    {
        BaseScript bse2 = fuckedUp.GetComponent<BaseScript>();

        if(bse2.iFrames > 0)
        {

        

        int cocko = knockbackD;
        int damageLow = (int)(damage * .75f);
        int damageHigh= (int)(damage * 1.25f);

        int damDealt = Random.Range(damageLow, damageHigh+1);

        if (rollCrit() == true)
            damDealt += damDealt;


        if (fuckedUp.transform.position.x < transform.position.x)
            cocko = cocko * -1;


        bse2.takeDamage(damDealt,cocko);
        }



    }

    public void takeDamage(int damTake,int knockedBack)
    {
        if(knockbackR < knockedBack)
        {

            rb.AddForce(new Vector2(knockedBack-knockbackR, 0));
            

        }

        health -= damTake;



    }


    bool rollCrit()
    {
        int rand = Random.Range(1,100);

        if (rand < critChance)
            return true;

        else
            return false;


    }

    void heal()
    {



    }

    public void beHealed()
    {





    }














}
