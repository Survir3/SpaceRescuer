using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class IgnorTimeScale : MonoBehaviour
{
    private float _startSpeed;

    private void Start()
    {
        _startSpeed = GetComponent<Rigidbody>().velocity.magnitude;
    }

    private void FixedUpdate()
    {
        float currenSpeed = _startSpeed * (1 / Time.fixedDeltaTime) / Time.timeScale;

        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * currenSpeed;
    }
}
