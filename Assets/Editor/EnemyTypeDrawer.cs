using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemyType))]
public class EnemyTypeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
    }
}

