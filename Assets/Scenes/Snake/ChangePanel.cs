using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    [SerializeField] private GameObject Up = null;
    [SerializeField] private GameObject Down = null;
    [SerializeField] private GameObject Left = null;
    [SerializeField] private GameObject Right = null;

    private void Update()
    {
        Change();
    }

    // Update is called once per frame
    void Change()
    {
        if (transform.position.z >= Left.transform.position.z) {
            Debug.Log("end");
        } else if (transform.position.z <= Right.transform.position.z) {
            Debug.Log("dead");
        } else if (transform.position.y >= Up.transform.position.y) {
            Debug.Log("dead");
        } else if (transform.position.y <= Down.transform.position.y) {
            Debug.Log("dead");
        }
    }
}
