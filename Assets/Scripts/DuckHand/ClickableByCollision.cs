using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickableByCollision : MonoBehaviour
{
    public string TagRestriction = "";

    private void OnCollisionEnter(Collision collision)
    {
        if (string.IsNullOrEmpty(TagRestriction) || collision.gameObject.CompareTag(TagRestriction))
            GetComponent<Button>().onClick.Invoke();
    }
}
