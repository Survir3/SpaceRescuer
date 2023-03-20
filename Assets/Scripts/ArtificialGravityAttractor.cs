using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class ArtificialGravityAttractor : MonoBehaviour
{
    [SerializeField, Range(int.MinValue, 0)] private float _powerGravity;
    [SerializeField] private float _speedRotation;

    public void Attract(Rigidbody bodyAttracted, Transform body)
    {
        Vector3 normalizePositionBody = NormalizePositionBodyOnAttractor(body);
        Vector3 forceGravity= normalizePositionBody * _powerGravity;
        Quaternion targetRotation = Quaternion.FromToRotation(body.up, normalizePositionBody)*body.rotation;
        bodyAttracted.AddForce(forceGravity);
        body.rotation = Quaternion.Lerp(body.rotation, targetRotation, _speedRotation*Time.deltaTime);
    }

    private Vector3 NormalizePositionBodyOnAttractor(Transform body)
    {
        return (body.position-transform.position).normalized;
    }
}