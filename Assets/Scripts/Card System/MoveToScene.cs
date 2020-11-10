using UnityEngine;

public class MoveToScene : MonoBehaviour
{
    public void GoScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
