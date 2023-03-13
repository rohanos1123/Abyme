using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterInteraction : MonoBehaviour
{
    private  List<GameObject> currentColls = new List<GameObject>();
    private Rigidbody2D rb;
    public float collidingWater;
    private float totalDensity;
    public float avgDensity;
    public float multiplier;
    public float constantMult;
    public Vector2 vect2Obj;
   



    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
     
    }


    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<drip>() && (collision.gameObject.transform.position.y < transform.position.y))
        {
            currentColls.Add(collision.gameObject);
        }
      
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<drip>())
        {
            currentColls.Remove(collision.gameObject);
        }
    }
    void FixedUpdate()
    {
        collidingWater = currentColls.ToArray().Length;
        addForce(collidingWater);
    }

    void addForce(float colliding)
    {
        foreach (GameObject gameObj in currentColls) 
        { 
            float angInRads = Mathf.Atan2((transform.position.y - gameObj.transform.position.y), (transform.position.x- gameObj.transform.position.x));
            vect2Obj = new Vector2(Mathf.Cos(angInRads)/2f, Mathf.Sin(angInRads));
            rb.AddForce(vect2Obj * (Mathf.Pow(gameObj.GetComponent<drip>().localDensity, multiplier) * constantMult *  Time.deltaTime));
        }
    }


}
