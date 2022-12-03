using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallController : MonoBehaviour
{
    private Rigidbody cueBallRB;
    private Vector3 originalCueBallPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        cueBallRB = gameObject.GetComponent<Rigidbody>();
        StartCueBall(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if ((!rb  || 
            (collision.gameObject.tag != "Solids" &&
            collision.gameObject.tag != "Stripes" &&
            collision.gameObject.tag != "8Ball" ))&&
            collision.gameObject.tag != "Table" &&
            collision.gameObject.tag != "Cue"
            )
        {
            PoolGameController.GameInstance.Fouled("first if");
            ReturnCueBall(gameObject);
            return;
        }
        else if(collision.gameObject.tag == "Table" || collision.gameObject.tag == "Cue"){
            return;
        }
        else if(collision.gameObject.tag != PoolGameController.GameInstance.CurrentPlayer.type && 
            PoolGameController.GameInstance.CurrentPlayer.type != ""){
            PoolGameController.GameInstance.Fouled("second if");
        }
        else{
            Vector3 forceDirection = (collision.contacts[0].point - transform.position).normalized;
            rb.AddForce(forceDirection*cueBallRB.velocity.magnitude);
        }
        
    }

    public void ReturnCueBall(GameObject cueball1){
        cueBallRB.velocity = Vector3.zero;
        cueball1.transform.position = originalCueBallPosition;
    }

    public void StartCueBall(GameObject cueball1){
        originalCueBallPosition = cueball1.transform.position;
    }
}
