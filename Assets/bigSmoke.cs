using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigSmoke : MonoBehaviour
{
    private float existanceTime;
    private SpawnWater sp; 
    // Start is called before the first frame update
    void Start()
    {
        existanceTime = 0;
        sp = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SpawnWater>(); 
    }

    // Update is called once per frame
    void Update()
    {
        existanceTime += Time.deltaTime;
        timeDestroy(existanceTime); 
    }

    void timeDestroy(float time)
    {
        if (time >= 3)
        {
            sp.WaterLeft -= 1; 
            Destroy(gameObject);
        }
    }
}
