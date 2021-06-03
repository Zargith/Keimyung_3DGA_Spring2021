using UnityEngine;

public class Launchable : MonoBehaviour
{
    [SerializeField] protected Rigidbody _main;
    [SerializeField] protected Rigidbody _cut1;
    [SerializeField] protected Rigidbody _cut2;
    protected ScoreManagerFruit man;
    bool cuted = false;
    float cutForce = 1.5f;

    [SerializeField] AudioSource slice;

    private void Start()
    {
        man = FindObjectOfType<ScoreManagerFruit>();
    }

    virtual public void Launch(Vector3 beg, Vector3 end)
    {
    }

    public void Cut()
    {
        if (cuted)
            return;
        slice.Play();
        cuted = true;
        //activate & setPosition
        _main.gameObject.SetActive(false);
        _cut1.gameObject.SetActive(true);
        _cut2.gameObject.SetActive(true);
        _cut1.transform.localPosition = _main.transform.localPosition;
        _cut2.transform.localPosition = _main.transform.localPosition;

        //forces
        _cut1.AddForce(Vector3.left * cutForce, ForceMode.Impulse);
        _cut1.AddTorque(new Vector3(Random.value, Random.value, Random.value) * Random.Range(1, 5), ForceMode.Impulse);
        _cut2.AddForce(Vector3.right * cutForce, ForceMode.Impulse);
        _cut2.AddTorque(new Vector3(Random.value, Random.value, Random.value) * Random.Range(1, 5), ForceMode.Impulse);
    }

    public void yolo()
    {
        if (man != null)
            man.addToScore(100);
    }
}



/*Patern to do:
 * 
 * xApple:
 *  Normal launch 
 *  
 * xBanana:
 * Side launch 
 * 
 * Bomb:
 * 
 * xKiwi:
 * go very high
 * 
 * mango:
 * toward the player
 * 
 * orange:
 * normal
 * 
 * xpineapple:
 * Only from the top
 * 
 * xwatermelon:
 * slow but not high
 * 
 * 
 */