using UnityEngine;

// In Unity, components are meant to be placed in the scene to "add a behavior" to an object. But here, we just want to store data, so
// a component would be meaningless.
// So instead of inheriting from MonoBehaviour, we can inherit from ScriptableObject, which is exactly meant for this usage.

// The [CreateAssetMenu] attribute tells Unity that we want a menu to create assets of that type. With this, you can create assets with
// your own custom values and functions, just like you would create a Material for example.

[CreateAssetMenu]
public class DialogueAsset : ScriptableObject
{

    // The use of crotchets here means that this variable doesn't contain a single string but an "array", a list of strings.
    // This will appear in the inspector as a list to which we can add, move and remove items. In our case, each entry of this list will
    // represent a single dialogue line.
    // The [TextArea] attirbute displays a large text field in the inspector, making it more convenient to write long texts.
    [TextArea(2, 2)]
    public string[] dialogues;

    // ... That's it. "Dialogue Assets" are just meant to contain the dialogue lines, and allow us to edit them from the inspector.

}