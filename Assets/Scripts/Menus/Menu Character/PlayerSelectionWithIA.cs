using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerSelectionWithIA : MonoBehaviour
{
    public Color colorSelection;
    public AudioSource SelectionAudio;
    public AudioSource ValidateAudio;
    public GameObject PositionVisualization;
    public PlayerSpecs Mate1PlayerSpec;
    public PlayerSpecs Mate2PlayerSpec;
    public PlayerSpecs Goal1PlayerSpec;
    public PlayerSpecs Goal2PlayerSpec;



    private Quaternion rotationVisualization = Quaternion.identity;
    private Menu menuAction;
    private GameObject prefabCharacterSelected;
    private GameObject characterSelected;
    private int counterCharacter = 0;


    private void Awake()
    {
        menuAction = new Menu();
    }
    private void Start()
    {

        SelectionCharacter();
    }

    public void ListenController(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        Vector2 positionJoystick = context.ReadValue<Vector2>();
        //Debug.Log(positionJoystick);
        if (Math.Abs(positionJoystick.y) > 0)
        {
            if (positionJoystick.x > 0.5)
            {
                SelectionCharacterRight(context);
            }
            else if (positionJoystick.x < -0.5)
            {
                SelectionCharacterLeft(context);
            }
        }

    }

    public void SelectionCharacter()
    {
        
        characterSelected = CharacterGrid.instance.listCharacters[counterCharacter];
        SelectionCharacterUI(characterSelected);
    }

    void SelectionCharacterUI(GameObject characterSelected)
    {
        characterSelected.GetComponent<Image>().color = colorSelection;
        PlayAudio(SelectionAudio);
        prefabCharacterSelected = Instantiate(characterSelected.GetComponent<PlayerSpecChoice>().PrefabVisualization, PositionVisualization.transform.position, rotationVisualization);
    }


    void DeselectionCharacterUI(GameObject characterSelected)
    {
        characterSelected.GetComponent<Image>().color = Color.black;
        if(prefabCharacterSelected)
        {
            if (prefabCharacterSelected.GetComponent<TurnObject>())
                prefabCharacterSelected.transform.Rotate(new Vector3(0f, -15 * prefabCharacterSelected.GetComponent<TurnObject>().speed, 0f));
            rotationVisualization = prefabCharacterSelected.transform.rotation;
        }
            
        Destroy(prefabCharacterSelected);
    }

    public void SelectionCharacterRight(InputAction.CallbackContext context)
    {

        DeselectionCharacterUI(CharacterGrid.instance.listCharacters[counterCharacter]);
        counterCharacter++;
        if (counterCharacter < CharacterGrid.instance.listCharacters.Count)
        {

            SelectionCharacter();
        }
        else
        {
            counterCharacter = 0;
            SelectionCharacter();
        }

    }

    public void SelectionCharacterLeft(InputAction.CallbackContext context)
    {
        DeselectionCharacterUI(CharacterGrid.instance.listCharacters[counterCharacter]);


        counterCharacter--;
        if (counterCharacter >= 0)
        {
            SelectionCharacter();
        }
        else
        {
            counterCharacter = CharacterGrid.instance.listCharacters.Count - 1;
            SelectionCharacter();
        }

    }

    public void ValidateChoice(InputAction.CallbackContext context)
    {
        PlayAudio(ValidateAudio);
        Match.instance.captain1 = characterSelected.GetComponent<PlayerSpecChoice>().PlayerSpecs;
        Match.instance.mate1 = Mate1PlayerSpec;
        Match.instance.goalKeeper1 = Goal1PlayerSpec;

        int randomCharacter = UnityEngine.Random.Range(0, 3);
        if (randomCharacter == counterCharacter)
        {
            int randomrCharacter = UnityEngine.Random.Range(0, 3);

        }
        counterCharacter = randomCharacter;
        characterSelected = CharacterGrid.instance.listCharacters[counterCharacter];
        Match.instance.captain2 = characterSelected.GetComponent<PlayerSpecChoice>().PlayerSpecs;
        Match.instance.mate2 = Mate2PlayerSpec;
        Match.instance.goalKeeper2 = Goal2PlayerSpec;
        ChoiceGameMode.ModeIA = true;
        //Field.Team2.SetIABrain();
        SceneManager.LoadScene("Game");
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    public void QuitSelectionMenu(InputAction.CallbackContext context)
    {
        DeselectionCharacterUI(CharacterGrid.instance.listCharacters[counterCharacter]);
    }

    public void OnEnable()
    {
        menuAction.ControlMenu.SelectionItemRigth.Enable();
        menuAction.ControlMenu.SelectionItemRigth.performed += SelectionCharacterRight;



        menuAction.ControlMenu.SelectionItemLeft.Enable();
        menuAction.ControlMenu.SelectionItemLeft.performed += SelectionCharacterLeft;



        menuAction.ControlMenu.SelectionItem.Enable();
        menuAction.ControlMenu.SelectionItem.performed += ListenController;

        menuAction.ControlMenu.Validate.Enable();
        menuAction.ControlMenu.Validate.performed += ValidateChoice;

        menuAction.ControlMenu.PreviousSceneChangement.Enable();
        menuAction.ControlMenu.PreviousSceneChangement.performed += QuitSelectionMenu;

    }
    private void OnDisable()
    {
        menuAction.ControlMenu.SelectionItemRigth.Disable();
        menuAction.ControlMenu.SelectionItemLeft.Disable();
        menuAction.ControlMenu.SelectionItem.Disable();
        menuAction.ControlMenu.Validate.Disable();
        menuAction.ControlMenu.PreviousSceneChangement.Disable();
    }
}
