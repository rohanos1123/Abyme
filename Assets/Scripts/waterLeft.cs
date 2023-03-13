using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class waterLeft : MonoBehaviour
{
    private float WaterLeft;
    [SerializeField] Text WaterText;
    private SpawnWater sp;

    private void Start()
    {
        sp = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SpawnWater>(); 
    }

    // Update is called once per frame
    void Update()
    {
        WaterLeft = sp.WaterLeft;
        WaterText.text = WaterLeft + "";
    }
}
