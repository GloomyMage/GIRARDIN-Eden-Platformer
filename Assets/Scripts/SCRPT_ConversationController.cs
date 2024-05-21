using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Collections;
using UnityEditor.ShaderGraph;
using UnityEngine.InputSystem;

[System.Serializable]
public class QuestionEvent : UnityEvent<Question> { }

public class ConversationController : MonoBehaviour
{
    public Conversation conversation;
    public Conversation defaultConversation;
    public QuestionEvent questionEvent;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SCRPT_SpeakerUIController speakerUILeft;
    private SCRPT_SpeakerUIController speakerUIRight;

   
    public NewControls controls;
    private InputAction _movementEscape;
    private InputAction _movementTalk;


    private int activeLineIndex;
    private bool conversationStarted = false;

    private void OnEnable()
    {
        _movementEscape = controls.Player.Escape;
        _movementEscape.Enable();

        _movementEscape.started += Escape;

        _movementTalk = controls.Player.Talk;
        _movementTalk.Enable();

        _movementTalk.started += Talk;


    }

    private void OnDisable()
    {
        _movementEscape.started -= Escape;
        _movementEscape.Disable();

        _movementTalk.started -= Talk;
        _movementTalk.Disable();

    }

    public void ChangeConversation(Conversation nextConversation)
    {
        conversationStarted = false;
        conversation = nextConversation;
        AdvanceLine();
    }

    private void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SCRPT_SpeakerUIController>();
        speakerUIRight = speakerRight.GetComponent<SCRPT_SpeakerUIController>();
    }

    private void Talk(InputAction.CallbackContext context)
    {
        AdvanceLine();

    }

    private void Escape(InputAction.CallbackContext context)
    {
        EndConversation();

    }

    private void EndConversation()
    {
        conversation = defaultConversation;
        conversationStarted = false;
        speakerUILeft.Hide();
        speakerUIRight.Hide();
    }

    private void Initialize()
    {
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;
    }

    private void AdvanceLine()
    {
        if (conversation == null) return;
        if (!conversationStarted) Initialize();

        if (activeLineIndex < conversation.lines.Length)
            DisplayLine();
        else
            AdvanceConversation();
    }

    private void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character))
        {
            SetDialog(speakerUILeft, speakerUIRight, line);
        }
        else
        {
            SetDialog(speakerUIRight, speakerUILeft, line);
        }

        activeLineIndex += 1;
    }

    private void AdvanceConversation()
    {
        // These are really three types of dialog tree node
        // and should be three different objects with a standard interface
        if (conversation.question != null)
            questionEvent.Invoke(conversation.question);
        else if (conversation.nextConversation != null)
            ChangeConversation(conversation.nextConversation);
        else
            EndConversation();
    }

    private void SetDialog(
        SCRPT_SpeakerUIController activeSpeakerUI,
        SCRPT_SpeakerUIController inactiveSpeakerUI,
        Line line
    )
    {
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();

        activeSpeakerUI.Dialog = "";

        StopAllCoroutines();
        StartCoroutine(EffectTypewriter(line.text, activeSpeakerUI));
    }

    private IEnumerator EffectTypewriter(string text, SCRPT_SpeakerUIController controller)
    {
        foreach (char character in text.ToCharArray())
        {
            controller.Dialog += character;
            yield return new WaitForSeconds(0.05f);
            // yield return null;
        }
    }
}
