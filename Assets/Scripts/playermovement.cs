using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class playermovement : MonoBehaviour
{
    public CharacterController2D controller;
    public SpawnWater sp; 
    private float horizontalMovement = 0f;
    public float runSpeed = 40f;
    public bool isJump = false;
    bool isCrouch = false;
    public bool isKick;
    [SerializeField] Animator anim;
    public float waterPressure;
    private Collider2D[] surroundingColls;
    private Rigidbody2D rb;
    public List<float> temp = new List<float>();
    public float extemp;
    public bool fireactive;
    [SerializeField] float reloadRadius; 
    public float waterInreserve;
    [SerializeField] float reloadPower; 
    private Collider2D[] vaporArry;
    public float time; 




    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sp = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SpawnWater>(); 
        extemp = 0;
    
        
    }

    // Update is called once per frame
    void Update()
    {
        
        vaporArry = Physics2D.OverlapCircleAll(transform.position, reloadRadius);
        extemp = calctemp();
        dehumidify();
        //Gather Input in Update 

        anim.SetFloat("Speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x));
        horizontalMovement = Input.GetAxisRaw("Horizontal") * runSpeed; //a equals -1. d equals +1 

        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            isCrouch = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isKick = true;
              
        }

        succ(vaporArry); 

        

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (fireactive == true)
            {
                fireactive = false;
            }
            else
            {
                fireactive = true;
            }
        }

       

        

    }
    private void FixedUpdate()
    {
        //move character 
        controller.Move(horizontalMovement * Time.fixedDeltaTime, isCrouch, isJump);
        isJump = false;
        isCrouch = false;
        isKick = false;
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

    void succ(Collider2D[] VaporArry)
    { 
        foreach(Collider2D vape in vaporArry)
        {
            if(vape.gameObject.tag == "WaterVapor")
            {
                Vector3 move = transform.position - vape.transform.position;
                vape.GetComponent<Rigidbody2D>().MovePosition(vape.transform.position + (move * reloadPower * Time.deltaTime));
                if (Mathf.Abs(move.x) < 0.85 && Mathf.Abs(move.y) < 0.85)
                 {
                    waterInreserve++;
                    Destroy(vape.gameObject);   
                }   

            }
        }
    }

    void dehumidify()
    {
        time += Time.deltaTime;
        for (float i = 0; i < waterInreserve; i++)
        {
            if (time > 0.50)
            {
                waterInreserve--;
                sp.WaterLeft++;
                time = 0;
            }
        }
    }


      

       
    }


  

