using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPreparationOfficer : MonoBehaviour
{
    [SerializeField] LevelActor levelActor;
    [SerializeField] float afterReadyDelay;

    private void Start()
    {
        PrepareTheLevel();
    }

    void PrepareTheLevel()
    {
        levelActor.mapActor.mapGenerateOfficer.GenerateMap();
        StartCoroutine(LevelIsReadyDelay());
    }

    IEnumerator LevelIsReadyDelay()
    {
        yield return new WaitForSeconds(afterReadyDelay);
        GameManager.instance.gameManagerObserverOfficer.Publish(ObserverSubjects.PostLevelInstantiate);
        GameManager.instance.gameManagerObserverOfficer.Publish(ObserverSubjects.LevelReadyToPlay);
    }
}
