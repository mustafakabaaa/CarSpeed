using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    public GameObject way;

    bool yolYapildi = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainCar" && yolYapildi == false)
        {
            Vector3 spawn_loc = new Vector3(transform.position.x, transform.position.y + 8.3f, 0);
            //Spawn i�in Instatiate kullan�l�r. Quaternion.identity- Rotasyonda bir de�i�ikli yapma demek
            Instantiate(way, spawn_loc, Quaternion.identity);
            yolYapildi = true;
            Destroy(this.gameObject, 5f);
        }


    }

}