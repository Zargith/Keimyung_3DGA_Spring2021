using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    public string TagRequirement = "bullet";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagRequirement))
            Destroy(gameObject);
    }
}
