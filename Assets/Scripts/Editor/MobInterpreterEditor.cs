using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomPropertyDrawer(typeof(MobCommand), true)]
public class MobCommandDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var value = property.managedReferenceValue;
        var commandName = (value as MobCommand)?.GetSummary() ?? "null";
        var idx = label.text.Replace("Element ", "");

        var labelPos = position;
        labelPos.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.LabelField(labelPos, $"{idx})", EditorStyles.boldLabel);

        labelPos.x += 20;
        labelPos.width -= 20;
        EditorGUI.LabelField(labelPos, commandName, EditorStyles.boldLabel);

        if (value is null)
        {
            position.width -= EditorGUIUtility.labelWidth;
            position.x += EditorGUIUtility.labelWidth;
            DisplayActionSelector(position, property, label);
        }
        else
        {
            EditorGUI.PropertyField(position, property, GUIContent.none, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.managedReferenceValue is null)
        {
            return EditorGUIUtility.singleLineHeight;
        }
        else
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
    }

    private readonly System.Type[] commandTypes = typeof(MobCommand).Assembly
        .GetTypes()
        .Where(t => t.IsSubclassOf(typeof(MobCommand)))
        .ToArray();

    private void DisplayActionSelector(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.BeginChangeCheck();

        string[] selections = new[] { "Select..." }
            .Concat(commandTypes.Select(t => t.Name.Replace("MobCommand", "")))
            .ToArray();

        var commandValue = EditorGUI.Popup(position, 0, selections);

        if (EditorGUI.EndChangeCheck())
        {
            var commandType = commandValue == 0 ? null : commandTypes[commandValue - 1];
            if (commandType is not null)
            {
                property.managedReferenceValue = System.Activator.CreateInstance(commandType);
            }
        }

        EditorGUI.EndProperty();
    }
}