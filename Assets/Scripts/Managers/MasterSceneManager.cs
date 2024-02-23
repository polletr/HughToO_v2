using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class MasterSceneManager : Singleton<MasterSceneManager>
{
    public List<string> _scene;

    void Awake()
    {
        foreach (string scene in _scene)
        {
            if (!SceneManager.GetSceneByName(scene).isLoaded)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
