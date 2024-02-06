using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    private InputActions actions;

    public ButtonInputHandler pause, action;

    public Vector2 movementAxis { private set; get; }

    private void Awake()
    {
        if (InputManager.instance == null)
        {
            InputManager.instance = this;
        }
    }

    private void OnEnable()
    {
        if (actions == null)
        {
            actions = new InputActions();
            actions.Main.MovementAxis.performed += i => movementAxis = i.ReadValue<Vector2>();
        }
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void Start()
    {
        pause = new ButtonInputHandler(actions.Main.Pause);
        pause = new ButtonInputHandler(actions.Main.ActionButton);
    }
}

public class ButtonInputHandler
{
    private InputAction action;
    private bool tapUsed;
    private bool releaseUsed;

    public ButtonInputHandler(InputAction action)
    {
        this.action = action;
    }

    public bool HOLD
    {
        get
        {
            return input();
        }
    }

    public bool TAP
    {
        get
        {
            bool usedLastFrame = tapUsed;
            tapUsed = input();
            return usedLastFrame ? false : tapUsed;
        }
    }

    public bool RELEASE
    {
        get
        {
            bool usedLastFrame = releaseUsed;
            releaseUsed = input();
            return usedLastFrame ? !releaseUsed : false;
        }
    }

    private bool input()
    {
        return action.phase == InputActionPhase.Performed;
    }
}
