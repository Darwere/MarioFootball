using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class PlayInputPass : MonoBehaviour
{
    public PlayerInputManager PInputManager;
    public List<PlayerInput> menuPInput = new List<PlayerInput>();
    bool onMenu = true;

    GameObject Team1 = null;
    PlayerInput T1PInput;
    bool T1got = false;
    GameObject Team2 = null;
    PlayerInput T2PInput;
    bool T2got = false;


    // Start is called before the first frame update
    void Start()
    {
        PInputManager = FindObjectOfType<PlayerInputManager>();
        PInputManager.onPlayerJoined += PlayerJoin;
        SceneManager.sceneLoaded += GetGamePlayerInput;
        
    }

    public void GetGamePlayerInput(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Game")
        {
            Team1 = Field.Team1.gameObject;
            Team2 = Field.Team2.gameObject;
            onMenu = false;
        }
        else
        {
            Team1 = null;
            Team2 = null;
            T1got = false;
            T2got = false;
            onMenu = true;
        }
    }

    private void PlayerJoin(PlayerInput playerInput)
    {
        if(onMenu)
        {
            menuPInput.Add(playerInput);
        }
    }


    private void OnPlayerLeft(PlayerInput playerInput)
    {
        if (onMenu)
        {
            menuPInput.Remove(playerInput);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Team1 != null && Team2 !=null)
        {
            if(Team1.GetComponent<PlayerInput>())
            {
                T1PInput = Team1.GetComponent<PlayerInput>();
                T1got = true;
            }
            if (Team2.GetComponent<PlayerInput>())
            {
                T2PInput = Team2.GetComponent<PlayerInput>();
                T2got = true;
            }

            if(T1got && T2got && menuPInput.Count == 2)
            {
                InputUser p1 = T1PInput.user;
                p1.UnpairDevices();
                foreach (InputDevice device in menuPInput[0].user.pairedDevices)
                {
                    InputUser.PerformPairingWithDevice(device, p1);
                }

                InputUser p2 = T2PInput.user;
                p2.UnpairDevices();
                foreach (InputDevice device in menuPInput[1].user.pairedDevices)
                {
                    InputUser.PerformPairingWithDevice(device, p2);
                }

                
                foreach (PlayerInput pI in menuPInput)
                {
                    Destroy(pI.gameObject);
                }
                Destroy(this.gameObject);

            }
        }
    }
}
