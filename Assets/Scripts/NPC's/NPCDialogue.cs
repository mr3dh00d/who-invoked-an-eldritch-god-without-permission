using System.Collections;
using UnityEngine;
using TMPro;

[System.Serializable] public class NPCDialogue
{
    [SerializeField] private string Name;

    [SerializeField] private Sprite Avatar;
    [SerializeField, TextArea] private string Dialogue;

    public NPCDialogue(string name, Sprite avatar, string dialogue)
    {
        Name = name;
        Avatar = avatar;
        Dialogue = dialogue;
    }

    public string GetName()
    {
        return Name;
    }

    public Sprite GetAvatar()
    {
        return Avatar;
    }

    public string GetDialogue()
    {
        return Dialogue;
    }
    
}