using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Rigidbody rb;
    Transform puckTransform;
    Vector3 targetPos;
    float gizmoFinalZ;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        puckTransform = GameObject.Find("Puck").GetComponent<Transform>();
        StartCoroutine("CalculatePosition");
        //Time.timeScale = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (puckTransform.position.x > 0)
        {
            Attack();
        } else
        {
            if (isPuckComing())
            {
                Anticipation();
            } else
            {
                Replacement();
            }
        }
        //Anticipation();
    }

    private void Attack()
    {
        MoveTo(new Vector3(puckTransform.position.x, transform.position.y, transform.position.z + 2 * (puckTransform.position.z - transform.position.z)));
    }

    private void Replacement()
    {
        MoveTo(new Vector3(0.6f, transform.position.y, transform.position.z));
    }

    private void Anticipation()
    {
        MoveTo(new Vector3(0.6f, puckTransform.position.y, getPuckXProjection(0.6f)));
    }

    private float getPuckXProjection(float x)
    {
        Rigidbody r = puckTransform.GetComponent<Rigidbody>();

        var d = (-puckTransform.position.x + x) / r.velocity.x;
        var finalZ = puckTransform.position.z + r.velocity.z * d;
        float realZ;// = (finalZ + 0.41f) % 0.82f - 0.41f;

        if (finalZ > 0)
            realZ = (finalZ + 0.41f) % 0.82f - 0.41f;
        else
            realZ = (finalZ - 0.41f) % 0.82f + 0.41f;

        if (Mathf.Floor((Mathf.Abs(finalZ) + 0.41f) / 0.82f) % 2 != 0)
        {
            realZ = -realZ;
        }
        return (realZ);
    }

    private void MoveTo(Vector3 to)
    {
        var maxSpeed = 10;
        var delta = to - transform.position;
        if (delta.magnitude > 0)
            rb.velocity = delta.normalized * Mathf.Min(delta.magnitude + 1, maxSpeed);
        else
            rb.velocity = Vector3.zero;
    }
    /*private void MoveTo(Vector3 target)
    {
        rb.velocity = Vector3.MoveTowards(transform.position, target, 10);
    }*/

    private bool isPuckComing()
    {
        return puckTransform.GetComponent<Rigidbody>().velocity.x > 0;
    }

    private IEnumerator CalculatePosition()
    {
        while (true)
        {
            targetPos = puckTransform.position;
            yield return new WaitForSeconds(0.2f);
        }
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(0.6f, transform.position.y, gizmoFinalZ), 0.2f);
    }*/
}
