using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class DialogEditor : EditorWindow
{
    public List<DialogNode> nodes; //узлы диалогов

    private int _currentOperation;
    private Dictionary<int, string> _dialogContainer = new Dictionary<int, string>();

    private int _dialogID;

    private bool _editorModeChangeTrigger = true;

    private Languages _language;

    private Vector2 _scrollPosition;

    public GameObject NPC { set; get; }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        _currentOperation =
            GUILayout.Toolbar(_currentOperation, new[] {"Создать новый диалог", "Редактировать диалог"});
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("ID Диалога:");
        try
        {
            _dialogID = Convert.ToInt32(GUILayout.TextField(_dialogID.ToString()));
        }
        catch (FormatException)
        {
            Debug.LogError("Введите правильный ID");
            _dialogID = 0;
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Space(300);
        if (GUILayout.Button("↑")) _dialogID++;

        if (GUILayout.Button("↓"))
        {
            if (_dialogID == 0 || _dialogID < 0)
                _dialogID = 0;
            else
                _dialogID--;
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        // if (_npc != null)//!
        // {
        //     try
        //     {
        //         GUILayout.Label("Существующие ID на языке " + _language + " для объекта <" + _npc.name + ">:");
        //         string[] allfiles = Directory.GetFiles(Application.dataPath + "/Dialogs/" + _language + "/" + _npc.name + "/");
        //         foreach (string filename in allfiles)
        //         {
        //         
        //         }
        //         GUILayout.Label("5");
        //     }
        //     catch (DirectoryNotFoundException)
        //     {
        //         Debug.LogError("Не существует диалогов для этого NPC");
        //     }
        // }
        GUILayout.EndHorizontal();
        _language = (Languages) EditorGUILayout.EnumPopup("Язык диалога", _language);
        NPC = (GameObject) EditorGUILayout.ObjectField("NPC", NPC, typeof(GameObject), true);
        GUILayout.Space(30);
        GUILayout.BeginHorizontal();
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        ScriptableObject target = this;
        var so = new SerializedObject(target);
        var stringsProperty = so.FindProperty("nodes");
        EditorGUILayout.PropertyField(stringsProperty, new GUIContent("Диалог"), true, GUILayout.MinWidth(350),
            GUILayout.MaxWidth(535));
        so.ApplyModifiedProperties(); // Remember to apply modified properties
        EditorGUILayout.EndScrollView();
        switch (_currentOperation)
        {
            case 0:
                CreateNewDialog();
                break;
            case 1:
            {
                EditDialog();
                break;
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginVertical();
        if (GUILayout.Button("+"))
        {
            if (nodes.Count == 0) nodes = new List<DialogNode>();
            nodes.Add(new DialogNode());
        }

        GUILayout.EndVertical();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Сохранить в .xml")) Generate();

        if (GUILayout.Button("Очистить")) nodes.Clear();
        GUILayout.EndHorizontal();
    }

    [MenuItem("Window/General/DialogEditor")]
    public static void ShowWindow()
    {
        DialogEditor window;
        window = GetWindow<DialogEditor>("Dialog Editor");
        window.minSize = new Vector2(350, 500);
        window.maxSize = new Vector2(535, 5000);
    }

    private void CreateNewDialog()
    {
        if (_editorModeChangeTrigger)
        {
            nodes = new List<DialogNode>();
            _editorModeChangeTrigger = false;
        }

        GUILayout.EndVertical();
    }

    private void EditDialog()
    {
        GUILayout.EndVertical();
        _editorModeChangeTrigger = true;
        if (GUILayout.Button("Загрузить из .xml"))
            try
            {
                Load();
            }
            catch (UnassignedReferenceException)
            {
                Debug.LogError("Необходимо назначить поле NPC");
            }
            catch (DirectoryNotFoundException)
            {
                Debug.LogError("Диалога не существует");
            }
    }

    private void Generate()
    {
        string path;
        if (NPC != null)
        {
            Directory.CreateDirectory(Application.dataPath + "/Dialogs/" + _language + "/" + NPC.name + "/");
            path = Application.dataPath + "/Dialogs/" + _language + "/" + NPC.name + "/" +
                   _dialogID +
                   "_dialog" + ".xml";

            XmlNode userNode;
            XmlElement element;

            var xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("dialog");
            var attribute = xmlDoc.CreateAttribute("name");
            //attribute.Value = "Dialog";
            rootNode.Attributes.Append(attribute);
            xmlDoc.AppendChild(rootNode);

            for (var j = 0; j < nodes.Count; j++)
            {
                userNode = xmlDoc.CreateElement("node");
                attribute = xmlDoc.CreateAttribute("id");
                attribute.Value = j.ToString();
                userNode.Attributes.Append(attribute);
                attribute = xmlDoc.CreateAttribute("npcText");
                attribute.Value = nodes[j].npcText;
                userNode.Attributes.Append(attribute);

                for (var i = 0; i < nodes[j].playerAnswer.Count; i++)
                {
                    element = xmlDoc.CreateElement("answer");
                    element.SetAttribute("text", nodes[j].playerAnswer[i].text);
                    if (nodes[j].playerAnswer[i].jumpToNode > 0)
                        element.SetAttribute("JumpToNode", nodes[j].playerAnswer[i].jumpToNode.ToString());
                    if (nodes[j].playerAnswer[i].exit)
                        element.SetAttribute("exit", nodes[j].playerAnswer[i].exit.ToString());
                    userNode.AppendChild(element);
                }

                rootNode.AppendChild(userNode);
            }

            xmlDoc.Save(path);
            Debug.Log(this + " Создан .xml файл диалога [ " + _dialogID + "dialog" + " ] по адресу: " + path);
        }
    }

    private void Load()
    {
        nodes = new List<DialogNode>();
        DialogNode dialogNode;
        try // чтение XML
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.dataPath + "/Dialogs/" + _language + "/" + NPC.name + "/" + _dialogID +
                             "_dialog.xml");
            var xmlElement = xmlDocument.DocumentElement;
            for (var i = 0; i < xmlElement.ChildNodes.Count; i++)
            {
                dialogNode = new DialogNode();
                dialogNode.playerAnswer = new List<PlayerAnswer>();
                dialogNode.nodeID = Convert.ToInt32(xmlElement.ChildNodes[i].Attributes.GetNamedItem("id").Value);
                dialogNode.npcText = xmlElement.ChildNodes[i].Attributes.GetNamedItem("npcText").Value;
                for (var j = 0; j < xmlElement.ChildNodes[i].ChildNodes.Count; j++)
                {
                    dialogNode.playerAnswer.Add(new PlayerAnswer());
                    var childNodes = xmlElement.ChildNodes[i].ChildNodes;
                    dialogNode.playerAnswer[j].text = childNodes[j].Attributes.GetNamedItem("text").Value;
                    if (childNodes[j].Attributes.GetNamedItem("JumpToNode") != null)
                        dialogNode.playerAnswer[j].jumpToNode =
                            Convert.ToInt32(childNodes[j].Attributes.GetNamedItem("JumpToNode").Value);

                    if (childNodes[j].Attributes.GetNamedItem("exit") != null)
                        dialogNode.playerAnswer[j].exit =
                            Convert.ToBoolean(childNodes[j].Attributes.GetNamedItem("exit").Value);
                    else
                        dialogNode.playerAnswer[j].exit = false;
                }

                nodes.Add(dialogNode);
            }
        }
        catch (Exception error)
        {
            Debug.Log(this + " Ошибка чтения файла диалога: " + "/Dialogs/" + _language + "/" + NPC.name + "/" +
                      _dialogID + "_dialog.xml" + error.Message);
        }
    }

    private enum Languages
    {
        Russian,
        English
    }
}

[Serializable]
public class DialogNode
{
    [HideInInspector] public int nodeID;

    public string npcText;
    public List<PlayerAnswer> playerAnswer;
}


[Serializable]
public class PlayerAnswer
{
    public string text;
    public int jumpToNode;
    public bool exit;
}