using System;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private string _currentText;
    private DialogPresenter _dialogPresenter;
    private Text _npcName;
    private XmlDocument _xmlDocument;
    private XmlElement _xmlElement;

    private void Start()
    {
        if (!(Camera.main is null)) _dialogPresenter = Camera.main.GetComponent<DialogPresenter>();
        _xmlDocument = new XmlDocument();
        StartDialog(0);
    }

    private void CloseDialog()
    {
        _dialogPresenter.OnDialogClose();
    }

    private void SetOptions()
    {
        var buttons = _dialogPresenter.GetOptionButtons();
    }

    private void StartDialog(int id)
    {
        try
        {
            _xmlDocument.Load(Application.dataPath + "/Dialogs/" + GameManager.Instance.language + "/" +
                              gameObject.name + "/" + id + "_dialog.xml");
        }
        catch (Exception)
        {
            Debug.Log("Не удалось загрузить файл диалога");
        }

        _dialogPresenter.OnDialogStart(gameObject.name);
        _xmlElement = _xmlDocument.DocumentElement;
        LoadDialogNode(0);
    }

    private void StartDialog(string id)
    {
        StartDialog(Convert.ToInt32(id));
    }

    private void LoadDialogNode(int node)
    {
        var buttons = _dialogPresenter.GetOptionButtons();
        _dialogPresenter.SetNpcText(_xmlElement.ChildNodes[node].Attributes.GetNamedItem("npcText").Value);
        var xmlNodeList = _xmlElement.ChildNodes[node].ChildNodes;
        if (xmlNodeList.Count > 3) Debug.Log("Player answers count is bigger than 3");
        for (var i = 0; i < 3; i++)
            if (xmlNodeList[i] != null)
            {
                buttons[i].SetActive(true);
                buttons[i].transform.GetChild(0).TryGetComponent<Text>(out var buttonText);
                buttonText.text = xmlNodeList[i].Attributes.GetNamedItem("text").Value;
                buttons[i].TryGetComponent<Button>(out var button);
                if (xmlNodeList[i].Attributes.GetNamedItem("exit") != null)
                    button.onClick.AddListener(delegate { CloseDialog(); });
                if (xmlNodeList[i].Attributes.GetNamedItem("JumpToNode") != null)
                {
                    var id = xmlNodeList[i].Attributes.GetNamedItem("JumpToNode").Value;
                    button.onClick.AddListener(delegate { LoadDialogNode(id); });
                }
            }
            else
            {
                buttons[i].SetActive(false);
            }
    }

    private void LoadDialogNode(string node)
    {
        LoadDialogNode(Convert.ToInt32(node));
    }
}