using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE
{
    MainMenu,
    Menu,
    Cinematic,
    Gameplay
}

public class GameManager : MonoBehaviour
{
    private DogController dogController;
    private DogAnimation dogAnimation;
    private DogAudio dogAudio;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject gameMenu;
    [SerializeField]
    private Cinematic startCinematic;
    private GAME_STATE gameState;

    public static GameManager instance = null;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        gameState = GAME_STATE.MainMenu;
        dogController = GameObject.FindGameObjectWithTag("Player").GetComponent<DogController>();
        dogAnimation = GameObject.FindGameObjectWithTag("Player").GetComponent<DogAnimation>();
        dogAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<DogAudio>();
        dogController.enabled = false;
        dogAnimation.enabled = false;
        dogAudio.enabled = false;
    }

    private void Update()
    {
        
        if(gameState == GAME_STATE.Menu && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("cmon dud exit");
            ExitMenu();
        } 
        else if (gameState == GAME_STATE.Gameplay && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("cmon dud enter");
            EnterMenu();
        }
    }

    private void DisableDog()
    {
        dogController.enabled = false;
        dogAnimation.enabled = false;
        dogAudio.enabled = false;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        PlayCinematic(startCinematic);
    }

    public void EnterMenu()
    {
        gameMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        gameState = GAME_STATE.Menu;
        DisableDog();
    }

    public void ExitMenu()
    {
        gameMenu.SetActive(false);
        EnterGameplay();
    }

    public void EnterGameplay()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameState = GAME_STATE.Gameplay;
        dogController.enabled = true;
        dogAnimation.enabled = true;
        dogAudio.enabled = true;
    }

    public void PlayCinematic(Cinematic cinematic)
    {
        gameState = GAME_STATE.Cinematic;
        DisableDog();
        StartCoroutine(nameof(CinematicPlayer), cinematic);
    }

    IEnumerator CinematicPlayer(Cinematic cinematic)
    {
        CinemachineVirtualCamera prevCamera = null; 
        foreach(Shot shot in cinematic.ShotList)
        {
            if (prevCamera != null) prevCamera.Priority = 0;
            shot.virtualCamera.Priority = 100;
            prevCamera = shot.virtualCamera;
            yield return new WaitForSeconds(shot.time);
        }
        EnterGameplay();
    }
}
