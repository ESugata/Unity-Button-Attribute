using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <inheritdoc cref="PropertyDrawer"/>
/// <summary>
/// Creates a button in the inspector when a <see cref="ButtonAttribute"/> is used on a bool field.
/// </summary>
[CustomPropertyDrawer(typeof(ButtonAttribute))]
public class ButtonAttributeDrawer : PropertyDrawer
{
        /// <inheritdoc />
        /// <summary>
        ///   <para>Draws a button for the <see cref="ButtonAttribute"/>.</para>
        /// </summary>
        /// <param name="position">Rectangle on the screen to use for the property GUI.</param>
        /// <param name="property">The SerializedProperty to make the custom GUI for.</param>
        /// <param name="label">The label of this property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

                // If the property type is not a boolean return.
                if (property.propertyType != SerializedPropertyType.Boolean)
                {
                        EditorGUI.LabelField(position, label.text, "Use Button with a bool field.");
                        return;
                }
                
                        // Get the ButtonAttribute.
                        ButtonAttribute buttonAttribute = ((ButtonAttribute) this.fieldInfo.GetCustomAttribute(typeof(ButtonAttribute)));

                        // If the validation method exists Validate!
                        if (!buttonAttribute.Validate())
                        {
                                GUI.enabled = false;
                        }


                        // Draw the button.
                        if (GUI.Button(position, label))
                        {
                                // If the button is clicked call the method from the attribute.
                                buttonAttribute.Callback();
                        }

                        GUI.enabled = true;
        }
        
}
