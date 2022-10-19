using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 200;

    // Update is called once per frame
    void Update()
    {
        this.transform.localEulerAngles = new Vector3(
            this.transform.localEulerAngles.x,
            this.transform.localEulerAngles.y + speed * Time.deltaTime,
            this.transform.localEulerAngles.z + speed * Time.deltaTime
        );
    }
}
