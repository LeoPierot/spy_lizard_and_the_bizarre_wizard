using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TONGUE_STATE
{
    FREE,
    STICKED
}

public class Tongue : MonoBehaviour
{
    public Transform    TongueEndPoint = null;
    public TONGUE_STATE TongueState = TONGUE_STATE.FREE;
    public Transform    Body = null;
    public Transform    HeadOrientation = null;
    [Header("Setup throw")]
    public float        ThrowStrength = 15f;

    private Rigidbody m_TongueRigidbody = null;
    private Transform m_EndPointOriginalParent = null;

    public void Start()
    {
        m_TongueRigidbody = TongueEndPoint.GetComponent<Rigidbody>();
        m_EndPointOriginalParent = TongueEndPoint.parent;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (TongueState != TONGUE_STATE.STICKED)
        {
            m_TongueRigidbody.velocity = Vector3.zero;
            m_TongueRigidbody.isKinematic = true;
            TongueState = TONGUE_STATE.STICKED;
        }
    }

    [ContextMenu("throw tongue")]
    public void ThrowTongue()
    {
        TongueEndPoint.SetParent(null);
        m_TongueRigidbody.isKinematic = false;
        m_TongueRigidbody.velocity += ThrowStrength * HeadOrientation.forward + ThrowStrength/2 * Vector3.up;
    }
    [ContextMenu("swallow back tongue")]
    public void SwallowBackTongue()
    {
        m_TongueRigidbody.isKinematic = true;
        m_TongueRigidbody.velocity = Vector3.zero;
        m_TongueRigidbody.angularVelocity = Vector3.zero;

        switch(TongueState) 
        {
            case TONGUE_STATE.FREE:
                //move the endpoint to the body
                //to lerp
                TongueEndPoint.SetParent(m_EndPointOriginalParent);
                TongueEndPoint.localPosition = Vector3.zero;
                TongueEndPoint.localRotation = Quaternion.identity;
                break;
            case TONGUE_STATE.STICKED:
                //move the body to the endpoint.
                //to lerp
                Body.position = TongueEndPoint.position;
                TongueEndPoint.SetParent(m_EndPointOriginalParent);
            break;
        }
    }
}
