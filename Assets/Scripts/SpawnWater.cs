using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWater : MonoBehaviour
{
    public Vector2 mousePosition;


    public GameObject[] listOfParticles;
    public GameObject crosshair;
    public GameObject waterPrefab;
    public GameObject oilPrefab;
    public GameObject match;
    public float thrustForce;
    public float  i;
    
    public int waterShot2;


    public float waterForce;
    public float numOfClicks;
    public float calcAveragePos;
    public float parPosx;
    public float parPosy;
    private Vector2 firePosition;
    [SerializeField] GameObject gunPivot;
    [SerializeField] Rigidbody2D gunrb;
    private GameObject[] listOfWaterVapor; 
    private followCursor fc;
    private float angle;
    private GameObject player;
    private playermovement playerScript;
    [SerializeField] int InwaterAmount;
    public int WaterLeft;


    // Start is called before the first frame update
    void Start()
    {
        fc = gunPivot.GetComponent<followCursor>();
        player = GameObject.FindGameObjectWithTag("player");
        gunrb = player.GetComponent<Rigidbody2D>();
        playerScript = player.GetComponent<playermovement>();
        i = 0;

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            numOfClicks++;
        }
        
        
        i += Time.deltaTime;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        angle = fc.angle;
        listOfParticles = GameObject.FindGameObjectsWithTag("fluid");
        listOfWaterVapor = GameObject.FindGameObjectsWithTag("WaterVapor");
        mousePosition = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshair.transform.position = new Vector2(mousePosition.x, mousePosition.y);
        firePosition = GameObject.FindGameObjectWithTag("firehole").transform.position;
        waterShot2 = listOfParticles.Length + listOfWaterVapor.Length + (int)playerScript.waterInreserve;

        if (calcAmmo())
        {
            if (!(playerScript.fireactive))
            {
                if (Input.GetButton("Fire1"))
                {
                    fluidFlow(firePosition, angle, waterPrefab, "Water");
                    gunrb.AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * -thrustForce * Time.deltaTime);

                }
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            destroyWater();
        }
        if (Input.GetMouseButton(1))
        {
            freezeWater();
        }
        if ((playerScript.fireactive))
            {
              if (Input.GetButton("Fire1"))
                {
                fluidFlow(firePosition, angle, oilPrefab, "Oil");
                gunrb.AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * -thrustForce * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.Escape))
                {
                    destroyWater();
                }

                if (Input.GetMouseButton(1) && i > 3)
                {
                    fluidFlow(firePosition, angle, match, "Match");
                    i = 0;
                }
            }
        }

        void fluidFlow(Vector2 pos, float angle, GameObject fluidPrefab, string mode)
        {
            GameObject droplet = Instantiate(fluidPrefab, pos, Quaternion.identity) as GameObject;
            droplet.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * waterForce * Time.deltaTime);
            
        }

        void freezeWater()
        {
            float numOfIce = 0;
            parPosx = 0;
            parPosy = 0;
            foreach (GameObject particle in listOfParticles)
            {
                Rigidbody2D rb = particle.GetComponent<Rigidbody2D>();
                drip ParticleScript = particle.GetComponent<drip>();
                if (ParticleScript.groupCount == numOfClicks)
                {
                    numOfIce++;
                    parPosx += (particle.transform.position.x);
                    parPosy += (particle.transform.position.y);

                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true;
                    ParticleScript.isFrozen = true;
                }

            }
            foreach (GameObject particle in listOfParticles)
            {
                drip particlecrip = particle.GetComponent<drip>();
                if (particlecrip.groupCount == numOfClicks)
                {
                    particlecrip.central = new Vector2((parPosx / numOfIce), (parPosy / numOfIce));
                }
            }

        }

        void destroyWater()
        {
            foreach (GameObject particle in listOfParticles)
            {
                Destroy(particle);
            }
        }

        bool calcAmmo()
        {
            WaterLeft = InwaterAmount - waterShot2;
            if (WaterLeft == 0)
            { return false; }
            else
            { return true; }
        }
    }

