using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCursor : MonoBehaviour
{
    [SerializeField] GameObject Cursor;
    void Update()
    {
        Vector3 Look = transform.InverseTransformPoint(Cursor.transform.position);
        float Angle = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg;

        transform.Rotate(0, 0, Angle);
    }
}
