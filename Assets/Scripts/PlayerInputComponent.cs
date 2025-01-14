using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerInputComponent : MonoBehaviour
{
    [SerializeField] private PlayerMovementController _playerMovementController;
    [SerializeField] private BombSpawnerComponent _bombSpawner;

    // UI Components
    [SerializeField] private BombSelectorComponent _bombSelector;
    private bool _inMenu = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement Inputs
        if (Input.GetButtonDown("Jump") && _playerMovementController.isGrounded)
        {
            _playerMovementController.Jump();
        }

        //Store user input as a movement vector
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        
        _playerMovementController.inputDir = inputDirection;
        #endregion
        
        #region Action Inputs
        if(!_inMenu)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                _bombSpawner.SpawnObject(_bombSelector.currentIndex);
            }

            if(Input.GetButtonDown("Fire2"))
            {
                _bombSpawner.WantToDoSecondaryAction(false);
            }

            if(Input.GetButtonDown("Fire3"))
            {
                _bombSpawner.WantToDoSecondaryAction(true);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _bombSelector.ChangeSelection(-1);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                _bombSelector.ChangeSelection(1);
            }
        }
        #endregion

        #region UI Inputs
        if(Input.GetButtonDown("OpenUI"))
        {
            _inMenu = true;
            Debug.Log("Open UI!");
            _bombSelector.ActivateUI(true);
        }
        if(Input.GetButtonUp("OpenUI"))
        {
            Debug.Log("Close UI!");
            _bombSelector.ActivateUI(false);
            _inMenu = false;
        }
        #endregion

    }
}
