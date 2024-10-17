// "using" instructions at the top of a script are "import" commands. This means that we declare which libraries will be usied in this
// script. For example, "using UnityEngine" means that we will use Unity's classes and features (like MonoBehaviour or Vector3).
using TMPro;
using UnityEngine;

// "public class" is used to declare a class that is accessible from anywhere, which means that this new type can be used inside all the
// other scripts of your game.
// The name of the class can't contain special characters (_, -, space, ...), and can't begin with a number. It also must have the exact
// same name as the script name, or Unity won't be able to read it, making your component unusable.
public class GuessTheNumber : MonoBehaviour
{

    // This variable is public to expose it in the inspector, so it can be edited manually from the editor. It should contain a reference
    // to the text box used to display a message to the player.
    public TMP_Text messageText;
    // This variable contains a reference to the input field used by the player to try a number, so we can read it in this script and
    // compare that number to the generated one.
    public TMP_InputField numberInput;

    // This variable stores the number generated randomly when the game starts. Since it shouldn't be editable, we make it "private", so
    // it won't appear in the inspector.
    private int generatedNumber;

    // The Start() function is a "lifecycle callback" of a component in Unity, a function meant to be called automatically by the engine
    // at a specific moment. In this case, this function is called the first time the component is enabled (which happens as soon as the
    // scene is loaded if that component is not disabled in the inspector).
    private void Start()
    {
        ResetGame();
    }

    // This function will make the player "guess" one more time. We can compare the number entered in the input field with the generated
    // number, and provide the appropriate feedback.
    // This function is public, so it can be visible in the inspector and bound to the "try" button easily.
    public void Try()
    {
        // First, we want to check if the input value is valid. We could check if the text is equal to "null", or if it's empty, etc.. But
        // to avoid testing every possible "empty text" scenario, we can use string.IsNullOrWhiteSpace(). This function checks if the text
        // is "null", empty, or contains only spaces, carriage returns, or other invisible characters like end-of-line markers.
        if (string.IsNullOrWhiteSpace(numberInput.text))
        {
            messageText.text = "Please enter a valid number.";
        }
        // If the input text is not empty, we can now try to convert the text input as an integer number, so we can compare it to our
        // generated number. This can be done by the int.TryParse() function. That function returns true/false if it succeeded/failed to
        // convert the text (eg. if you enter only letters, this operation will fail). But that function is able to output another value,
        // using the "out" keyword. This allow us to declare a variable that will contain the converted number only if the operation
        // succeeded.
        else if (int.TryParse(numberInput.text, out int playerNumber))
        {
            // If the player guessed right, show victory message
            if (playerNumber == generatedNumber)
            {
                messageText.text = "VICTORY!";
            }
            // Else, if the player entered a number greater than ( > ) the generated number, show "lower" message
            else if (playerNumber > generatedNumber)
            {
                messageText.text = "Lower...";
            }
            // Else, if the player entered a number lower than ( < ) the generated number, show "greater" message
            else if (playerNumber < generatedNumber)
            {
                messageText.text = "Greater...";
            }
        }
        // In any other case (if the text is not empty but can't be converted into a number), just show an error message.
        else
        {
            messageText.text = "Please enter a valid number.";
        }

        // This instruction will clear the input field, so the player can enter a new value without the need of erasing the previous one
        // manually.
        numberInput.text = "";
    }

    // This function restarts the game. It is used in the Start() function (making the game start when the scene is loaded), and is bound
    // to the "reset" button (so we can restart a game at will).
    public void ResetGame()
    {
        // When we start a new game, we can invite the player to input a value
        messageText.text = "Guess the number...";

        // We clear the content of the input field, so the player is ready to enter a number
        numberInput.text = "";

        // We can generate a number randomly by using Random.Range(). It takes two parameters: a minimum and a maximu value. There's a trap
        // here: this function, when used with integer numbers, has a minimum value INCLUSIVE, and a maximum value EXCLUSIVE. Using
        // mathematical notation, we get [min;max[. So if we use Random.Range(1, 100), the number 100 will never be generated, just because
        // of that exclusive maximum number rule. To include 100 in the possible numbers, we can just add 1 to it.

        // Note that writing Random.Range(1, 101) has the same result as writing Random.Range(1, 100 + 1). It's a matter of readability:
        // our maximum value is 100, so instead of writing "101" which means nothing in our gameplay, we could prefer writing 100 + 1,
        // making the intention more clear.
        generatedNumber = Random.Range(1, 100 + 1);

        // For debug purposes, we can log a message in the Console window to make the generated number value visible. We can then try if
        // our game rules and feedbacks behave as expected.
        // This message will only be visible in the editor, but hidden to your player. So it's a safe way to test and debug your logic
        // during the development.
        Debug.Log("Generated number: " + generatedNumber);
    }

}
