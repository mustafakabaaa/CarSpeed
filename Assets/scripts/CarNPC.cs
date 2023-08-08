using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CarNPC : MonoBehaviour
{
    private float varsayilan_hiz;
    Rigidbody2D rb;
    private int gidilen_serit;
    float neKadarUzaktaSpawn = 7f;
    public Sprite car1, car2, car3, car4, car5, car6, car7, car8, car9, car10, car11, car12, car13, car14, car15, car16, car17, car18;
    public int carSprite;

    SpriteRenderer spr;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        carSprite = Random.Range(1, 19);
        gidilen_serit = Random.Range(1, 5);
        varsayilan_hiz = Random.Range(3.5f, 3.7f);
        

        if (gidilen_serit==1)
        {
            transform.position = new Vector3(-1.27f, transform.position.y + neKadarUzaktaSpawn, 0);
        }
        else if (gidilen_serit == 2)
        {
            transform.position = new Vector3(-0.433f, transform.position.y + neKadarUzaktaSpawn, 0);
        }
        else if (gidilen_serit ==3)
        {
            transform.position = new Vector3(0.432f, transform.position.y + neKadarUzaktaSpawn, 0);
        }
        else
        {
            transform.position = new Vector3(1.304f, transform.position.y + neKadarUzaktaSpawn, 0);
        }
        
        switch(carSprite)
        {
            case 1:
                spr.sprite = car1;
                break;
            case 2: spr.sprite = car2;
                break;
            case 3: spr.sprite = car3;
                break;
            case 4: spr.sprite = car4;
                break;
            case 5: spr.sprite = car5;
                break;
            case 6: spr.sprite = car6;
                break;
            case 7: spr.sprite = car7;
                break;
            case 8: spr.sprite = car8;
                break;
            case 9: spr.sprite = car9;
                break;
            case 10: spr.sprite = car10;
                break;
            case 11: spr.sprite = car11;
                break;
            case 12: spr.sprite = car12;
                break;
            case 13: spr.sprite = car13;
                break;
            case 14: spr.sprite = car14;
                break;
            case 15: spr.sprite = car15;
                break;
            case 16: spr.sprite = car16;
                break;
            case 17: spr.sprite = car17;
                break;
            case 18: spr.sprite = car18;
                break;
        }
        


    }
    
    void Update()
    {
        rb.velocity = new Vector3(rb.velocity.x,varsayilan_hiz*50*Time.deltaTime,0);
        
    }
   


}

