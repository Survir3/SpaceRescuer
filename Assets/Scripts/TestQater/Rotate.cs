using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Quaternion _quaternion;
    public Rigidbody Rigidbody;
    public Transform Transform;

    private void Awake()
    {
        //  _quaternion = Quaternion.LookRotation(Transform.position).normalized;
        _quaternion = Quaternion.LookRotation(transform.position + Vector3.left).normalized;
    }

    private void FixedUpdate()
    {
        Rigidbody.rotation= Quaternion.Slerp(Rigidbody.rotation.normalized, _quaternion, 5*Time.deltaTime);
    }
}
