using UnityEngine;

public class ButtonAttributeExample : MonoBehaviour
{
    [Button(typeof(ButtonAttributeExample),"ButtonCallback1")]
    public bool ExampleButton;

    public bool enableNextButton;

    [Button(typeof(ButtonAttributeExample),"ButtonCallback2",typeof(ButtonAttributeExample),"ExampleButton2Validation")]
    public bool ExampleButton2;

    
    public static object ButtonCallback1()
    {
        Debug.Log("ExampleButton Pressed");
        return null;
    }

    public static object ButtonCallback2()
    {
        Debug.Log("ExampleButton2 Pressed");
        return null;
    }

    public static bool ExampleButton2Validation()
    {
        return FindObjectOfType<ButtonAttributeExample>().enableNextButton;
    }
}