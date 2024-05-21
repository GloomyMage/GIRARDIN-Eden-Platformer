using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCRPT_DialogDisplay : MonoBehaviour
{

    public NewControls controls;
    private InputAction _movementTalk;

    public Conversation conversation;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SCRPT_SpeakerUIController speakerUILeft;
    private SCRPT_SpeakerUIController speakerUIRight;

    private int activeLineindex = 0;

    // Start is called before the first frame update
    void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SCRPT_SpeakerUIController>();
        speakerUIRight = speakerRight.GetComponent<SCRPT_SpeakerUIController>();

        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        _movementTalk = controls.Player.Talk;
        _movementTalk.Enable();

        _movementTalk.started += Talk;


    }

    private void OnDisable()
    {
        _movementTalk.started -= Talk;
        _movementTalk.Disable();

    }

    private void Talk(InputAction.CallbackContext context)
    {
        AdvanceConversation();

    }

     void AdvanceConversation()
    {
        if (activeLineindex  < conversation.lines.Length)
        {
            DisplayLine();
            activeLineindex += 1;
        }
        else
        {
            speakerUILeft.Hide();
            speakerUIRight.Hide();
            activeLineindex = 0;

        }
    }

    void DisplayLine()
    {
        Line line = conversation.lines[activeLineindex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character))
        {
            SetDialog(speakerUILeft, speakerUIRight, line.text);
        }
        else
        {
            SetDialog(speakerUIRight, speakerUILeft, line.text);
        }
    }

    void SetDialog(

        SCRPT_SpeakerUIController activeSpeakerUI,
        SCRPT_SpeakerUIController inactiveSpeakerUI,
        string text)
        {
        activeSpeakerUI.Dialog = text;
        activeSpeakerUI.Show();
    }

}
