using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CameraMoveOfficer : MonoBehaviour
{
    [SerializeField] Transform cameraStartRefObject;

    private void Update()
    {
        
    }
    public void StartPositioning() 
    {
        LevelActor currentLevel = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>();
        float referenceLeftGridPos = currentLevel.mapActor.mapGenerateOfficer.gridDict["0,0"].transform.position.x;
        float referenceRightGridPos = currentLevel.mapActor.mapGenerateOfficer.gridDict["0,1"].transform.position.x;

        float referenceTopGridPos = currentLevel.mapActor.mapGenerateOfficer.gridDict["1,1"].transform.position.y;
        float referenceBottomGridPos = currentLevel.mapActor.mapGenerateOfficer.gridDict["1,0"].transform.position.y;

        float midPointX = referenceLeftGridPos + ((referenceRightGridPos - referenceLeftGridPos)/2);
        float midPointY = referenceBottomGridPos + ((referenceTopGridPos - referenceBottomGridPos) / 2);
        cameraStartRefObject.position = new Vector3(midPointX, midPointY, 0f);

        CameraManager.instance.camera.transform.position = cameraStartRefObject.position + new Vector3(0f, 0f, -10f);
    }

    [Button("CamRePosition")]
    void ButtonCamReposition() 
    {
        CameraManager.instance.camera.transform.position = cameraStartRefObject.position + new Vector3(0f, 0f, -10f);
    }
}
