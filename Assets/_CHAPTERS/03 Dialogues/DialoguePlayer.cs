using UnityEngine;
using TMPro;

// The Dialogue Player component is meant to "read" a Dialogue Asset, and display each dialogue line until the end of the dialogue
// (basically, when there's no more lines left to read).
public class DialoguePlayer : MonoBehaviour
{

    // The [Tooltip] attribute allow you to display informations about this variable in the inspector. Try hovering tthe "Dialogue Asset"
    // field in the editor, you should see its content as a tooltip.
    [Tooltip("The asset that contains the dialogue lines to display.")]
    public DialogueAsset dialogueAsset;

    [Tooltip("The object to enable when a dialogue is displayed, or to disable when it finishes.")]
    public GameObject dialogueBox;

    [Tooltip("The object used to display a dialogue line on UI.")]
    public TMP_Text dialogueText;

    // The dialogue lines from the Dialogue Asset are stored in an "array", a list of string items. Each item is bound to an "index", which
    // is basically the nnumber of the dialogue line in our case. In C# (and most of other programming languages), the first item of an
    // array has the index 0.
    // But as we read the content of the Dialogue Asset, we must store the index of the line currently being displayed for 2 reasons:
    // - When the player press the input to read the next dialogue line, we must know which line is actually the next one
    // - If the index is "out of range" (greater than the number of items in the array), we know that the dialogue is finished
    // 
    // So here, we make the variable have a default value to -1, because the function DisplayNextDialogueLine() will first add 1 to this
    // value, in order to actually read the "next" line. So as soon as the game starts, we will add 1 to this value, so the line at index 0
    // will be displayed as expected.
    // Also, this variable is private, since it's not meant to be edited in the inspector.
    private int _dialogueLineIndex = -1;

    // When the game starts...
    private void Start()
    {
        // We play the next available dialogue line. At this step, the index of the current line should be -1. This function will first add
        // 1 to the line index, so it displays the line at index 0 the first time the function is called.
        DisplayNextDialogueLine();
    }

    // For each frame...
    private void Update()
    {
        // When the player presses the "Space" key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Play the next dialogue line, or hide the dialogue box if the dialogue is finished
            DisplayNextDialogueLine();
        }
    }

    // This function will display the next dialogue line if there's one, or hide the dialogue box if the dialogue is finished.
    private void DisplayNextDialogueLine()
    {
        // "++" is called an "increment operator". It just adds 1 to a number. It's the exact same operation as "variable += 1".
        // So first, we increment the current dialogue line index.
        _dialogueLineIndex++;

        // If the new index is not "out of range" (lower than the number of lines to read)
        if (_dialogueLineIndex < dialogueAsset.dialogues.Length)
        {
            // We make the dialogue box visible in the scene (if it was not already)
            dialogueBox.SetActive(true);
            // We replace the displayed text by the dialogue line at the new index
            dialogueText.text = dialogueAsset.dialogues[_dialogueLineIndex];
        }
        // Else, if the index is "out of range", meaning there's no more dialogue line to read
        else
        {
            // We just disable the dialogue box
            dialogueBox.SetActive(false);
        }
    }

}
