using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hambubger : MonoBehaviour
{
    private float luckynum;
    private Rigidbody2D rb;
    public float existanceTime; 
    [SerializeField] float upwardForce;
    [SerializeField] float ChaosCoefficient;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        existanceTime = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        ChaosMovement();
        existanceTime += Time.deltaTime;
        timeDestroy(existanceTime); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "fluid")
        {Destroy(gameObject);}
    }

    void ChaosMovement()
    {
        luckynum = Random.Range(0, ChaosCoefficient);
        if ((int)Random.Range(0, ChaosCoefficient) == (int)luckynum && rb.velocity.y < 2)
        {
            rb.AddForce(Vector2.up * upwardForce * Time.deltaTime);
        }
    }

    void timeDestroy(float time)
    {
        if(time >= 5)
        {
            Destroy(gameObject);
        }
    }





}
