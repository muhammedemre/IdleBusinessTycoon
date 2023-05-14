public class LevelManager : Manager
{
    public static LevelManager instance;
    public LevelCreateOfficer levelCreateOfficer;

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

    public override void PreLevelInstantiateProcess()
    {
        levelCreateOfficer.CreateLevelProcess();
    }

    public override void LevelReadyToPlayProcess()
    {
        GameManager.instance.gameManagerUpdateFollowOfficer.updateOn = true;
    }

    public override void LevelEndProcess()
    {
        GameManager.instance.gameManagerUpdateFollowOfficer.updateOn = false;
    }

}
