using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MenuManager : MonoBehaviour
    {
        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
