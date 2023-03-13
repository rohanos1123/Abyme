using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genHeat : MonoBehaviour
{
    public float emittingtemp;
    private float distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float tempEffectSys(GameObject obj)
    {
        float tempEffect; 
        Vector2 objectPos = obj.transform.position;
        distanceToPlayer = Mathf.Sqrt(Mathf.Pow((transform.position.x - objectPos.x), 2) + Mathf.Pow((transform.position.y - objectPos.y), 2));
        return tempEffect = emittingtemp / ((int)distanceToPlayer + 1);
    }

    
}
