using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class SCRPT_Interaction : MonoBehaviour, SCRPT_Interactible
{
    [SerializeField] private SpriteRenderer _interactSprite;
    private Transform _playerTransform;
    private const float _interact_distance = 5f;

    public NewControls controls;
    private InputAction _movementTalk;

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
        if (IsWithinInteractDistance())
        {
            Interact();
        }

        if (_interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            _interactSprite.gameObject.SetActive(false);
        }
        else if (!_interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
            {
            _interactSprite.gameObject.SetActive(true);
        }
    }

    
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Interact();

    private bool IsWithinInteractDistance()
    {
        if (Vector2.Distance(_playerTransform.position, transform.position) < _interact_distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
  
}
