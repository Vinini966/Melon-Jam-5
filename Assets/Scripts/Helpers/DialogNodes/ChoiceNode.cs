//Code by Vincent Kyne

using Defective.JSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceNode : TextBoxNode
{

    public List<TextBoxNode> choices = new List<TextBoxNode>();

    public ChoiceNode(string character, string text, JSONObject choiceList, NodeType type = NodeType.CHOICE) :
        base(null, character, text, type)
    {
        foreach(JSONObject n in choiceList.list)
        {
            choices.Add(new TextBoxNode(n[DialogImporter.next].stringValue,
                                        null,
                                        n[DialogImporter.text][DialogImporter.lang].stringValue,
                                        NodeType.NULL));
        }
    }
    

    public string GetNextBasedOnChoice(int choice)
    {
        return choices[choice].nextNode;
    }
}
