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
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
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

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        // reset portraits, layout, and speakers to default values
        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("left");

        // reset other relevant variables 
        canContinueToNextLine = true;
        canSkipCurrentLine = false;
        choiceMustBeMade = false;
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    public void ExitDialogueMode(InputAction.CallbackContext ctx)
    {
        if(ctx.phase.Equals(InputActionPhase.Started))
        {
            ExitDialogueMode();
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // displays next line of dialogue in ink file
            // similar to a Queue
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            // handle tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";

        //hide items while text is typing
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

    private void HideChoices()
    {
        foreach(GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        // Loop through each tag and handle it accordingly 
        foreach (string tag in currentTags)
        {
            //parse the tag
            string[] splitTag = tag.Split(':');

            // splitTag array should be of length 2 so
            // check to see if the length is not 2 defensively
            if (splitTag.Length > 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            // clean up key and tag pair
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // hangle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    // will play animation that has same name
                    // This means make sure both the animation's name in the editor
                    // is the same as the tag in the ink file
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;

            }
        }
    }

    public void ContinueStory(InputAction.CallbackContext ctx)
    {
        if (canContinueToNextLine 
            && ctx.phase.Equals(InputActionPhase.Started) 
            && !choiceMustBeMade)
        {
            ContinueStory();
        }
        
        // If player can't go to the next line they can skip the current line
        else if(!canContinueToNextLine && ctx.phase.Equals(InputActionPhase.Started))
        {
            canSkipCurrentLine = true;
        }
    }

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

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear the first selected choice first,
        // then wait for atleast one frame before we set the selected choice
        EventSystem.current.SetSelectedGameObject(null); yield return null;
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);

    }

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
