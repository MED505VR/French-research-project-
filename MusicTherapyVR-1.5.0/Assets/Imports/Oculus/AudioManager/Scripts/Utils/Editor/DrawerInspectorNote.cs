using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(InspectorNoteAttribute))]
public class DrawerInspectorNote : DecoratorDrawer
{
    public override void OnGUI(Rect position)
    {
        var note = attribute as InspectorNoteAttribute;

        // our header is always present
        var posLabel = position;
        posLabel.y += 13;
        posLabel.x -= 2;
        posLabel.height += 13;
        EditorGUI.LabelField(posLabel, note.header, EditorStyles.whiteLargeLabel);

        // do we have a message too?
        if (!string.IsNullOrEmpty(note.message))
        {
            var color = GUI.color;
            var faded = color;
            faded.a = 0.6f;

            var posExplain = posLabel;
            posExplain.y += 15;
            GUI.color = faded;
            EditorGUI.LabelField(posExplain, note.message, EditorStyles.whiteMiniLabel);
            GUI.color = color;
        }

        var posLine = position;
        posLine.y += string.IsNullOrEmpty(note.message) ? 30 : 42;
        posLine.height = 1f;
        GUI.Box(posLine, "");
    }

    public override float GetHeight()
    {
        var note = attribute as InspectorNoteAttribute;
        return string.IsNullOrEmpty(note.message) ? 38 : 50;
    }
}

[CustomPropertyDrawer(typeof(InspectorCommentAttribute))]
public class DrawerInspectorComment : DecoratorDrawer
{
    public override void OnGUI(Rect position)
    {
        var comment = attribute as InspectorCommentAttribute;

        // our header is always present
        var posLabel = position;
        //posLabel.y += 13;
        //posLabel.x -= 2;
        //posLabel.height += 13;
        //EditorGUI.LabelField( posLabel, comment.header, EditorStyles.whiteLargeLabel );

        // do we have a message too?
        if (!string.IsNullOrEmpty(comment.message))
        {
            var color = GUI.color;
            var faded = color;
            faded.a = 0.6f;

            var posExplain = posLabel;
            posExplain.y += 15;
            GUI.color = faded;
            EditorGUI.LabelField(posExplain, comment.message, EditorStyles.whiteMiniLabel);
            GUI.color = color;
        }
    }

    public override float GetHeight()
    {
        var note = attribute as InspectorNoteAttribute;
        return string.IsNullOrEmpty(note.message) ? 38 : 50;
    }
}