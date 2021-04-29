using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickableByShoot : Shootable
{
    override internal void OnHit()
    {
        GetComponent<Button>().onClick.Invoke();
    }
}
