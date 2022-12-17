using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class CueHandler : MonoBehaviour
{
    public ActionBasedController frontController;
    public ActionBasedController backController;
    
    public InputActionProperty right;
    public InputActionProperty left;
    public InputActionProperty rightA;

    private Rigidbody cueRB;

    private Vector3 cuePos;
    private float lockOffset;
    private Vector3 lockForward;
    public Transform cueTip;

    public bool hitCueBall = false;

    private Vector3 originalCuePosition;
    public bool grabbed =  false;

    // Start is called before the first frame update
    void Start()
    {
        cueRB = gameObject.GetComponent<Rigidbody>();
        StartCue(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbed){
            UpdateCuePosition();
        }

        if(gameObject.transform.position != originalCuePosition && !grabbed){
            ReturnCue(gameObject);
        }
    }

    void UpdateCuePosition()
    {
        var rightAValue = rightA.action.ReadValue<float>();
        var backRight = right.action.ReadValue<float>();
        var backLeft = left.action.ReadValue<float>();
        
        Vector3 frontPos = frontController.transform.position;
        Vector3 backPos = backController.transform.position;


        if (backRight > 0.7  && backLeft < 0.7)//first press of trigger
        {
            lockForward = transform.forward;
            lockOffset = (frontPos - backPos).magnitude;
        }
        else if(backRight > 0.7 && backLeft > 0.7)// trigger held down
        {
            float currOffset = (frontPos - backPos).magnitude;
            cueRB.MovePosition(cuePos + lockForward * (lockOffset - currOffset));
        }
        else if(rightAValue >  0.7)
        {
            SetGrabbedFalse();
        }
        else // free mode
        {
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
        else if(collision.gameObject.tag != "CueBall" || hitCueBall){
            PoolGameController.GameInstance.Fouled("cue");
        }
        else{
            Vector3 forceDirection = (collision.contacts[0].point - cueTip.position).normalized;
            rb.AddForce(forceDirection*cueRB.velocity.magnitude);
            hitCueBall = true;
        }
        
    }

    public void ReturnCue(GameObject cue1){
        cue1.transform.position = new Vector3(-0.383f, 0.781f, 1.133f);
        cue1.transform.rotation = Quaternion.Euler(0.0f,90.0f,0.0f);
        //cue1.transform.eulerAngles = Vector3(0.0,90.0,0.0);
    }

    public void StartCue(GameObject cue1){
        originalCuePosition = cue1.transform.position;
    }

    public void SetGrabbedTrue()
    {
        grabbed = true;
    }

    public void SetGrabbedFalse()
    {
        grabbed = false;
    }
}
