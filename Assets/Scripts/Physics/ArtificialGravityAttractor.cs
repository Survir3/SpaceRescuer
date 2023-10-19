using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class ArtificialGravityAttractor : MonoBehaviour
{
    [SerializeField, Range(-400, 0)] private float _powerGravity;
    [SerializeField] private float _speedRotation;

    public void Attract(Rigidbody bodyAttracted, Transform body)
    {
        Vector3 normalizePositionBody = NormalizePositionBodyOnAttractor(body);

        Vector3 forceGravity = normalizePositionBody * _powerGravity;

        bodyAttracted.AddForce(forceGravity,ForceMode.VelocityChange);

        Quaternion targetRotation = Quaternion.FromToRotation(body.up, normalizePositionBody) * body.rotation;
        bodyAttracted.rotation = Quaternion.Slerp(body.rotation, targetRotation, _speedRotation * Time.deltaTime);
    }

    public Vector3 Grav(Rigidbody bodyAttracted, Transform body)
    {
        Vector3 normalizePositionBody = NormalizePositionBodyOnAttractor(body);

        return Vector3.Lerp(body.position, normalizePositionBody * -1*_powerGravity, Time.deltaTime);
    }

    private Vector3 NormalizePositionBodyOnAttractor(Transform body)
    {
        return (body.position-transform.position).normalized;
    }
}
