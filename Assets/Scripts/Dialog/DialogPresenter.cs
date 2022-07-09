using UnityEngine;
using UnityEngine.UI;

public class DialogPresenter : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    [SerializeField] private Text npcName;

    [SerializeField] private Text npcText;

    [SerializeField] private GameObject[] options;

    public void OnDialogStart(string npcName)
    {
        if (!canvas.activeInHierarchy) canvas.SetActive(true);

        this.npcName.text = npcName;
    }

    public GameObject[] GetOptionButtons()
    {
        return options;
    }

    public void SetNpcText(string Text)
    {
        npcText.text = Text;
    }

    public void OnDialogClose()
    {
        canvas.SetActive(false);
    }
}