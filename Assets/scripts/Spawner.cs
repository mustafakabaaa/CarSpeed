using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject randomCarNPC;
    bool carSpawn=true;
    void Start()
    {
        StartCoroutine(bekle());
    }

    IEnumerator bekle()
    {
        
        while (carSpawn==true)
        {
            Instantiate(randomCarNPC, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
        
       
    }
}
