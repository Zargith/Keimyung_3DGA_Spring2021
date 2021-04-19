using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachMouse : MonoBehaviour
{
    public Camera cam;
    private Rigidbody r;

    //private bool selected;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector3.Distance(transform.position, cam.transform.position);
        //selected = false;
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            /*if (Input.GetMouseButton(0))
            {
                selected = false;
                return;
            }*/
            //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //var point = ray.origin + (ray.direction * 1.5F);

            //transform.position = new Vector3(point.x, transform.position.y, point.z);
            var pos = Input.mousePosition;
            pos.z = distance;
            pos = cam.ScreenToWorldPoint(pos);
            pos.y = transform.position.y;
            r.velocity = (pos - transform.position) * 10;
        }
    }

    /*private void OnMouseDown()
    {
        selected = true;
    }*/
}
