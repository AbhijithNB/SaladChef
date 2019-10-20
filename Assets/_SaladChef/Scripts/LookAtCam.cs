using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{

    public bool isCanvasElement = false;

    private Camera cam = null;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        if (cam)
        {
            if (isCanvasElement)
            {
                Quaternion q = Quaternion.LookRotation(2 * transform.position - cam.transform.position, transform.up);
                transform.rotation =
                    Quaternion.Euler(transform.rotation.eulerAngles.x, q.eulerAngles.y, transform.rotation.eulerAngles.z);

                //transform.LookAt(2 * transform.position - cam.transform.position, transform.up);
            }
            else
            {
                Quaternion q = Quaternion.LookRotation(cam.transform.position, transform.up);
                transform.rotation =
                    Quaternion.Euler(transform.rotation.eulerAngles.x, q.eulerAngles.y, transform.rotation.eulerAngles.z);

                //transform.LookAt(cam.transform.position, transform.up);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
