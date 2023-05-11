using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerUpdateFollowOfficer : MonoBehaviour
{
    [SerializeField] float updateFrequency;
    public bool updateOn = false;
    float nextUpdate = 0;
    private void Update()
    {
        UpdateCheck();
    }

    void UpdateCheck() 
    {
        if (nextUpdate < Time.time && updateOn)
        {
            nextUpdate = Time.time + updateFrequency;
            print("UPDATE FOLLOW : "+nextUpdate);
            GameManager.instance.gameManagerObserverOfficer.Publish(ObserverSubjects.UpdateFollow);
        }
    }
}
