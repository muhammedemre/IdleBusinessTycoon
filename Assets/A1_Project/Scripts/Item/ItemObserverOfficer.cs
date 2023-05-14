using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObserverOfficer : MonoBehaviour, IObserver
{
    [SerializeField] ItemActor itemActor;
    public delegate void SubjectEventHandler();
    event SubjectEventHandler UpdateFollow;

    private void Start()
    {
        UpdateFollow += UpdateFollowProcess;
        Subscriber(this);
    }

    public void GetUpdated(ISubject subject)
    {
        if (subject is ObserverSubject observerSubject)
        {
            UpdateFollow.Invoke();
        }
    }

    public void Subscriber(IObserver observer)
    {
        GameManagerObserverOfficer gameManagerObserverOfficer = GameManager.instance.gameManagerObserverOfficer;
        gameManagerObserverOfficer.observerSubjectDict[ObserverSubjects.UpdateFollow].Subscribe(observer);  
    }

    public void UpdateFollowProcess()
    {
        //print("Item UpdateFollow 11:    "+Time.time);
        itemActor.CalculateLeftLifeTime();
        itemActor.CalculateCurrentPrice();
    }
}
