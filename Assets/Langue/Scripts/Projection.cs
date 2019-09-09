using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour
{
    public Transform    TongueEndPoint = null;
    public Transform    HeadOrientation = null;
    public Tongue       Tongue;

    [Header("Setup throw")]
    public float ThrowStrength = 5f;

    private Rigidbody m_TongueRigidbody = null;

    // Start is called before the first frame update
    void Start()
    {
        m_TongueRigidbody = TongueEndPoint.GetComponent<Rigidbody>();
        m_TongueRigidbody.isKinematic = true;
    }


    [ContextMenu("throw tongue")]
    public void ThrowTongue()
    {
        m_TongueRigidbody.isKinematic = false;
        m_TongueRigidbody.velocity += ThrowStrength * HeadOrientation.forward;
    }

    public void SwallowTongueBack()
    {
        m_TongueRigidbody.isKinematic = true;
        m_TongueRigidbody.velocity = Vector3.zero;
    }

    
}
