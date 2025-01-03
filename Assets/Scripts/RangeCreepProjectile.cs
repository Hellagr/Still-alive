using UnityEngine;

public class RangeCreepProjectile : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 0.8f);
    }
}