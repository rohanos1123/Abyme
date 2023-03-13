using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class waterInResText : MonoBehaviour
{
    private float WaterinRes;
    [SerializeField] Text WaterText;
    private playermovement pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("player").GetComponent<playermovement>();
    }
    
    // Update is called once per frame
    void Update()
    {
        WaterinRes = pm.waterInreserve;
        WaterText.text = WaterinRes + "";
    }
}
