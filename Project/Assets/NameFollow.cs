using UnityEngine;
using System.Collections;

public class NameFollow : MonoBehaviour
{
    public Transform leader;
    public float followSharpness = 1f;
    private Vector3 OffSetVector;
    private Vector3 a;

    void LateUpdate()
    {
        OffSetVector = new Vector3(leader.position.x, leader.position.y, leader.position.z);
        a = transform.position;
        a += (OffSetVector - transform.position) * followSharpness;
        a.z += 2;
        transform.position = a;
    }
}
