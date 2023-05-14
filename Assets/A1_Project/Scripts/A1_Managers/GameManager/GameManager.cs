using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    public static GameManager instance;
    public GameManagerObserverOfficer gameManagerObserverOfficer;
    public GameManagerUpdateFollowOfficer gameManagerUpdateFollowOfficer;
    [SerializeField] private float preGameStartDelay;
    public ObserverSubjects currentGameState = (ObserverSubjects)0;

    public ScreenState currentGameScreen = ScreenState.SplashScreen;

    public string gameName;

    public enum ScreenState
    {
        SplashScreen, LandingMenuScreen, MapScreen, ShopScreen
    }

    private void Awake()
    {
        SingletonCheck();
        
        gameManagerObserverOfficer.CreateSubjects();
        StartCoroutine(PreGameStartDelay());
    }

    IEnumerator PreGameStartDelay()
    {
        yield return new WaitForSeconds(preGameStartDelay);
        gameManagerObserverOfficer.Publish(ObserverSubjects.PreLevelInstantiate);
    }
    

    void SingletonCheck()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }
    
}
