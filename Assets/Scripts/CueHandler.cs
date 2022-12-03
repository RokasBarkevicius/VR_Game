using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CueHandler : MonoBehaviour
{
    public ActionBasedController frontController;
    public ActionBasedController backController;
    

    //kazkas apie button press (nezinau ar pades)
    //https://www.youtube.com/watch?v=NdHGuj-u-kc
    public InputActionProperty right;
    public InputActionProperty left;

    private Rigidbody cueRB;

    private Vector3 cuePos;
    private float lockOffset;
    private Vector3 lockForward;
    

    //Vector3 frontPos;
    //Vector3 backPos;

    //GameObject original;

    // Start is called before the first frame update
    void Start()
    {
        cueRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCuePosition();
        /*frontPos = frontController.transform.position;
        backPos = backController.transform.position;
        cuePos = 0.75f * backPos + 0.25f * frontPos;
        cueRB.MovePosition(cuePos);
        cueRB.MoveRotation(Quaternion.LookRotation(frontPos - backPos));
        */

    }

    void UpdateCuePosition()
    {
        var backRight = right.action.ReadValue<float>();
        var backLeft = left.action.ReadValue<float>();
        
        Vector3 frontPos = frontController.transform.position;
        Vector3 backPos = backController.transform.position;


        if (backRight == 1  && backLeft != 1)//first press of trigger
        {
            //print("first press");
            lockForward = transform.forward; //gali reik keist priklausomai nuo pivot
            lockOffset = (frontPos - backPos).magnitude;
        }
        else if(backRight == 1 && backLeft == 1)// trigger held down
        {
            //print("press hold");
            float currOffset = (frontPos - backPos).magnitude;
            cueRB.MovePosition(cuePos + lockForward * (lockOffset - currOffset));
        }
        else // free mode
        {
            //print("free");
            cuePos = 0.75f * backPos + 0.25f * frontPos;
            cueRB.MovePosition(cuePos);
            cueRB.MoveRotation(Quaternion.LookRotation(frontPos - backPos));
        }
        
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (!rb)
        {
            return;
        }
        // nezinau ar reik to cueTip, nes cue centras kaip ir yra ant to tip
        Vector3 forceDirection = (collision.contacts[0].point - transform.position).normalized;
        rb.AddForce(forceDirection*cueRB.velocity.magnitude);
    }
}
