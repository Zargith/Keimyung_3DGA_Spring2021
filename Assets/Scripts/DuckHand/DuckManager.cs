using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DuckHand
{
    public class DuckManager : MonoBehaviour, IShootable
    {
        public GameObject ExplosionEffectPrefab;

        void IShootable.OnHit()
        {
            GetComponent<AudioSource>().Play();
            FindObjectOfType<DuckHandGameManager>().OnEnemyKill();


            Instantiate(ExplosionEffectPrefab, transform);

            Invoke(nameof(hideDuck), 0.1f); // Wait for the duck do disappear in the explosion effect
            GetComponent<Collider>().enabled = false;

            Destroy(gameObject, 4f); // also destroys the destroy effect object
        }

        private void hideDuck()
        {
            GetComponent<Renderer>().enabled = false;

        }
    }
}
