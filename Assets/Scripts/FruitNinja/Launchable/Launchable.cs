using UnityEngine;

public class Launchable : MonoBehaviour
{
    protected ScoreManager man;

    private void Start()
    {
        man = FindObjectOfType<ScoreManager>();
    }

    virtual public void Launch(Vector3 beg, Vector3 end)
    {
    }

    public void yolo(int id)
    {
        if (gameObject.GetInstanceID().Equals(id))
        {
            man.addToScore(1000);
        }
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