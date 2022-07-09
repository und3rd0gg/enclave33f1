using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    [RequireComponent(typeof(Animator))]
    public class SceneTransition: MonoBehaviour
    {
        private static Animator _animator;
        private static readonly int CloseCurtains = Animator.StringToHash("CloseCurtains");
        private static AsyncOperation _loadingSceneOperation;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void LoadScene(string scene)
        {
            _animator.SetTrigger(CloseCurtains);
            _loadingSceneOperation = SceneManager.LoadSceneAsync(scene);
            _loadingSceneOperation.allowSceneActivation = false;
        }

        public void OnAnimationOver()
        {
            _loadingSceneOperation.allowSceneActivation = true;
        }
    }
}
