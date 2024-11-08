using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Net.Security;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField]
    private float typingSpeed = 0.04f;

    [Header("Dialogue UI")]
    [SerializeField]
    private GameObject continueIcon;
    
    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private TextMeshProUGUI displayNameText;

    [SerializeField]
    private Animator portraitAnimator;

    private Animator layoutAnimator;


    [Header("Choices UI")]
    [SerializeField]
    private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;


    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = true;

    private bool canSkipCurrentLine = false;

    private bool choiceMustBeMade = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private InkExternalFunctions inkExternalFunctions;

    // label keys
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        inkExternalFunctions = new InkExternalFunctions();
    }

    /// <summary>
    /// Gets the instance of the DialogueManager
    /// </summary>
    /// <returns>The instance of the DialogueManager</returns>
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        // Dialogue is not playing at the beginning of the game
        // And there is no Dialogue UI that starts immeadiatley
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get the layout animator
        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        // get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    /// <summary>
    /// Enters the Dialogue using the appropriate inkJSON
    /// </summary>
    /// <param name="inkJSON">the inkJSON file that corresponds to an NPC's ink file</param>
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        // Sets the ink story to the Json's text
        // Then displays Dialogue panel
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        // Start Binding functions
        inkExternalFunctions.Bind(currentStory);

        // reset portraits, layout, and speakers to default values
        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("left");

        // reset other relevant variables 
        canContinueToNextLine = true;
        canSkipCurrentLine = false;
        choiceMustBeMade = false;
    }

    /// <summary>
    /// Exits the dialogue mode and stops displaying UI
    /// </summary>
    private void ExitDialogueMode()
    {
        inkExternalFunctions.Unbind(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    /// <summary>
    /// Exit the Dialogue based on an input action
    /// </summary>
    /// <param name="ctx">Input action being used</param>
    public void ExitDialogueMode(InputAction.CallbackContext ctx)
    {
        if(ctx.phase.Equals(InputActionPhase.Started))
        {
            ExitDialogueMode();
        }
    }

    /// <summary>
    /// This Continues the next line in the ink file's story
    /// </summary>
    public void ContinueStory()
    {
        // If you can continue the story
        // displays next line of dialogue in ink file
        // similar to a Queue
        if (currentStory != null && currentStory.canContinue)
        {
           // If there is no Coroutine running
           // Stop the Coroutine
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            string nextLine = currentStory.Continue();

            if(nextLine.Equals("") && !currentStory.canContinue)
            {
                ExitDialogueMode();
            }
            else
            {
                // set the Coroutine to the DisplayLine() function
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));

                // handle tags
                HandleTags(currentStory.currentTags);
            }

        }
        else
        {
            ExitDialogueMode();
        }
    }

    /// <summary>
    /// This performs the typing text effect and also handles Rich Text Tags
    /// </summary>
    /// <param name="line">The Line of dialogue the effect is impacting</param>
    /// <returns></returns>
    private IEnumerator DisplayLine(string line)
    {
        // Reset Dialogue text to nothing
        dialogueText.text = "";

        // Hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        // This will be used to check if we are using Rich Text Tags
        // To change things like color within texts
        // You simply have to add them to the ink file
        // and wherever a '#' is used you have to add a back slash: \
        bool isAddingRichTextTag = false;

        foreach(char letter in line.ToCharArray())
        {
            // if submit button is pressed, skip typing effect
            if (canSkipCurrentLine)
            {
                dialogueText.text = line;
                canSkipCurrentLine = false;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if(letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                dialogueText.text += letter;
                if(letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
  
        }

        continueIcon.SetActive(true);

        // display choices, if any, for this dialogue line
        DisplayChoices();

        canContinueToNextLine = true;
    }

    /// <summary>
    /// Hides all Choices if there are any available
    /// </summary>
    private void HideChoices()
    {
        foreach(GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    /// <summary>
    /// Handles the tags that are within an ink story that changes parts of the UI
    /// </summary>
    /// <param name="currentTags">list of tags within the current line of the ink story</param>
    private void HandleTags(List<string> currentTags)
    {
        // Loop through each tag and handle it accordingly 
        foreach (string tag in currentTags)
        {
            //parse the tag
            string[] splitTag = tag.Split(':');

            // splitTag array should be of length 2 so
            // check to see if the length is not 2
            if (splitTag.Length > 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }

            // clean up key and tag pair
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue; // change speaker name
                    break;

                case PORTRAIT_TAG:
                    // will play animation that has same name
                    // This means make sure both the animation's name in the editor
                    // is the same as the tag in the ink file
                    portraitAnimator.Play(tagValue);
                    break;

                // will play animation that has same name
                // This means make sure both the animation's name in the editor
                // is the same as the tag in the ink file
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;

            }
        }
    }

    /// <summary>
    /// This calls the ContinueStory() with no params based of Input Actions
    /// It also checks to see if you can skip the current line that is being typed
    /// </summary>
    /// <param name="ctx">The Input Action</param>
    public void ContinueStory(InputAction.CallbackContext ctx)
    {
        // If the player can continue to the next line of dialogue
        // AND There is NO choice to be made
        // AND the player presses the appropriate action button
        if (canContinueToNextLine
            && !choiceMustBeMade 
            && ctx.phase.Equals(InputActionPhase.Started))
        {
            ContinueStory();
        }
        
        // If player can't go to the next line they can skip the current line
        else if(!canContinueToNextLine && ctx.phase.Equals(InputActionPhase.Started))
        {
            canSkipCurrentLine = true;
        }
    }

    /// <summary>
    /// Displays the Choices to the player if there are any
    /// </summary>
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // Defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. " +
                "Number of choices given: " + currentChoices.Count);
        }

        // enable and initialize the choices up to the amount of choices for this Line of dialogue
        for (int i = 0; i < currentChoices.Count; i++)
        {
            choices[i].gameObject.SetActive(true);
            choicesText[i].text = currentChoices[i].text;
        }

        // go through the remaining choices the UI supports and make sure they are hidden
        for (int i = currentChoices.Count; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        // If a Choice can be selected, then a choice must be made
        if (currentChoices.Count > 0)
        {
            choiceMustBeMade = true;
        }

        StartCoroutine(SelectFirstChoice());
    }

    // Event System requires we clear the first selected choice first,
    // then wait for atleast one frame before we set the selected choice
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null); yield return null;
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);

    }

    /// <summary>
    /// This Function is called when the player performs the "Submit"
    /// Action on the Choice screen
    /// </summary>
    /// <param name="choiceIndex">The index of the choice that is being made</param>
    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            // Choose the Choice based on the index passed in
            currentStory.ChooseChoiceIndex(choiceIndex);

            // after choosing the choice, a choice is no longer needed to be made
            choiceMustBeMade = false;
            ContinueStory();
        }
    }
}
