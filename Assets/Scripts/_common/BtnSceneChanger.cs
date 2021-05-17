using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnSceneChanger : MonoBehaviour
{
    public string SceneName;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            SceneManager.LoadSceneAsync(SceneName);
        });
    }
}
