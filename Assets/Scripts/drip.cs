using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drip : MonoBehaviour
{
    public bool isFrozen;
    public float microcount;
    [SerializeField] SpawnWater sw;
    public float decayConstant;
    public float luckyNum;
    public float localDensity;
    public float groupCount;
    public Vector2 central;
    public float distFromCenter;
    private Rigidbody2D rb;
    public bool attached;
    public float attractionForce;
    public int localArry;
    [SerializeField] float temperature;
    [SerializeField] float multiplier;
    [SerializeField] float constMult;
    [SerializeField] GameObject smoke; 
    private Collider2D[] surroundingColls;
    

    // Start is called before the first frame update
    void Start()
    {
        sw = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SpawnWater>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        microcount = sw.listOfParticles.Length + 1;
        groupCount = sw.numOfClicks;
        central = Vector2.one;
        
    }

    // Update is called once per frame



    void Update()
    {
        
        luckyNum = Random.Range(0, decayConstant / (microcount * (distFromCenter * 2f)));
        if (isFrozen)
        {
            gameObject.layer = 9;
            distFromCenter = Mathf.Sqrt((Mathf.Pow((central.x - transform.position.x), 2)) + Mathf.Pow((central.y - transform.position.y), 2));

        }
        else
        {
            gameObject.layer = 8;
            rb.isKinematic = false;
            distFromCenter = 1;
        }
        if (Mathf.Pow(transform.position.x - sw.mousePosition.x, 2) + Mathf.Pow(transform.position.y - sw.mousePosition.y, 2) > 10)
        {   
            if ((int)Random.Range(0, decayConstant / (microcount * (distFromCenter * 2f))) == (int)luckyNum)
            {
                if (isFrozen)
                {
                    isFrozen = false;
                }
                else
                {
                    evaporate(); 
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "HeatSource")
        {
            evaporate(); 
        }
    }

    public void FixedUpdate()
    {
        temperature = calctemp();
        surfaceTension();
        changeStates(temperature); 
    }

    void surfaceTension()
    {
        localDensity = Physics2D.OverlapCircleAll(transform.position, 0.19f).Length;
    }

    public float calctemp()
    {
        float temp = 0;
        surroundingColls = Physics2D.OverlapCircleAll(transform.position, 20f, 13 << LayerMask.NameToLayer("Heater"));
        foreach (Collider2D heater in surroundingColls)
        {
            temp += heater.gameObject.GetComponent<genHeat>().tempEffectSys(gameObject);
        }
        return temp;
    }

    void changeStates(float temp)
    {
        if(temp < 0)
        {
            freeze(); 
        }
        else if(temp>30)
        {
            evaporate();

        }
    }

    private void evaporate()
    {
        GameObject smokePart = Instantiate(smoke, transform.position, Quaternion.identity);
        smokePart.GetComponent<Rigidbody2D>().velocity = (Vector2.up * 100f * Time.deltaTime);
        Destroy(gameObject);
    }

    void freeze()
    {
        gameObject.layer = 9;
        distFromCenter = Mathf.Sqrt((Mathf.Pow((central.x - transform.position.x), 2)) + Mathf.Pow((central.y - transform.position.y), 2));
    }

}





