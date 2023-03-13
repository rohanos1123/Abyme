using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oil : MonoBehaviour
{
    public float temperature;
    public GameObject firePrefab;
    public float upwardForce;
    private GameObject player;
    private Collider2D[] surroundingColls;
    [SerializeField] float threshholdtemp;
    [SerializeField] float oilDec;
    public bool istouchingFire; 
    // Start is called before the first frame update
    void Start()
    { 
        temperature = 50f;
        player = GameObject.FindGameObjectWithTag("player"); 
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        temperature = calctemp();
        if(temperature > threshholdtemp)
        {
          ignite();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
      if(coll.gameObject.tag == ("HeatSource"))
        {
            istouchingFire = true; 
            decay(oilDec); 
        }

    }



    void ignite()
    {
      GameObject fireObj = Instantiate(firePrefab, transform.position, Quaternion.identity);
      fireObj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * upwardForce * Time.deltaTime);
      Destroy(gameObject);
    }

    public float calctemp()
    {
        float temp = 0;
        surroundingColls = Physics2D.OverlapCircleAll(transform.position, 20f, (13 << LayerMask.NameToLayer("Heater"))); 
        foreach (Collider2D heater in surroundingColls)
        {
            temp += heater.gameObject.GetComponent<genHeat>().tempEffectSys(gameObject);
        }
        return temp;
    }

    void decay(float decayRange)
    {
        float luckyNumber = Random.Range(0, decayRange);
        float randInt = Random.Range(0, decayRange);
        if((int)randInt == (int)luckyNumber)
        {
            ignite(); 
        }
        
        
    }





}
