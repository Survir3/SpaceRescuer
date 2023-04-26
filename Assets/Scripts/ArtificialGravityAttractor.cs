using UnityEngine;

//[RequireComponent (typeof(Rigidbody))]
public class ArtificialGravityAttractor : MonoBehaviour
{
    [SerializeField, Range(-400, 0)] private float _powerGravity;
    [SerializeField] private float _speedRotation;

    public void Attract(Rigidbody bodyAttracted, Transform body)
    {
        Vector3 normalizePositionBody = NormalizePositionBodyOnAttractor(body);

        Vector3 forceGravity = normalizePositionBody * _powerGravity;

        bodyAttracted.AddForce(forceGravity);

        Quaternion targetRotation = Quaternion.FromToRotation(body.up, normalizePositionBody) * body.rotation;
         bodyAttracted.rotation = Quaternion.Slerp(body.rotation, targetRotation, _speedRotation * Time.deltaTime);
       // bodyAttracted.AddRelativeTorque(targetRotation.eulerAngles);

       // bodyAttracted.MoveRotation(targetRotation);
    }

    public Vector3 Grav(Rigidbody bodyAttracted, Transform body)
    {
        Vector3 normalizePositionBody = NormalizePositionBodyOnAttractor(body);

        return Vector3.Lerp(body.position, normalizePositionBody * -1*_powerGravity, 3*Time.deltaTime);
    }

    //public Quaternion AttractRotation(Rigidbody bodyAttracted,Transform body)
    //{
    //    Vector3 normalizePositionBody = NormalizePositionBodyOnAttractor(body);

    //    Vector3 forceGravity = normalizePositionBody * _powerGravity;
    //    bodyAttracted.AddForce(forceGravity);

    //    Quaternion targetRotation = Quaternion.FromToRotation(body.up, normalizePositionBody) * body.rotation;
    //    return Quaternion.Slerp(body.rotation, targetRotation, _speedRotation * Time.deltaTime);
    //}

    private Vector3 NormalizePositionBodyOnAttractor(Transform body)
    {
        return (body.position-transform.position).normalized;
    }


    //private void OnCollisionStay(Collision collision)
    //{
    //    if(collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody survivor))
    //    {
    //        Vector3 normalizePositionBody = NormalizePositionBodyOnAttractor(collision.transform);

    //        Vector3 forceGravity = normalizePositionBody *-1* _powerGravity;;

    //        survivor.AddForce(forceGravity);
    //    }
    //}
}
