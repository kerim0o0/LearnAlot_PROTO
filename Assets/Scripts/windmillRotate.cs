using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windmillRotate : MonoBehaviour
{
    private float zVec = 0;
    public float speed;
    void Update()
    {
        zVec -= speed * Time.deltaTime;
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, zVec);
    }
}
