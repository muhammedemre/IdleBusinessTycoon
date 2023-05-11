using UnityEngine;
public class CameraManager : Manager
{
    public static CameraManager instance;
    public GameObject camera;
    public CameraMoveOfficer cameraMoveOfficer;

    public override void PostLevelInstantiateProcess()
    {
        print("StartPositioning");
        cameraMoveOfficer.StartPositioning();
    }


    public enum CameraState     
    {
        inGame, Finish
    }

    private void Awake()
    {
        SingletonCheck();
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
