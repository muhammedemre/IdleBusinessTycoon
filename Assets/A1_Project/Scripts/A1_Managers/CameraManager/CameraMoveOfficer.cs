using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class CameraMoveOfficer : MonoBehaviour
{
    [SerializeField] Transform cameraStartRefObject;
    [SerializeField] float bottomBorder, topBorder;
    [SerializeField] List<float> mapPortionColumnPoints = new List<float>();
    [ReadOnly] [SerializeField] int currentColumn = 0;
    [SerializeField] float CameraVerticalSlideSpeed, CameraVerticalSlideSmoothTime, CameraHorizontalMoveDuration;

    Vector3 refVelocityVerticalSlide = Vector3.zero;


    private void Update()
    {
        
    }
    public void StartPositioning() // Position the cam on start
    {
        CalculateTopAndBottomBorders();
        CalculateMapPortionColumnPoints();

        CameraManager.instance.camera.transform.position = cameraStartRefObject.position + new Vector3(0f, 0f, -10f);
    }

    void CalculateTopAndBottomBorders() 
    {
        LevelActor currentLevel = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>();
        Vector3 bottomMidPosition = (currentLevel.mapActor.mapGenerateOfficer.gridDict["1,0"].transform.position +
                              currentLevel.mapActor.mapGenerateOfficer.gridDict["1,1"].transform.position) / 2f;

        Vector3 topMidPosition = (currentLevel.mapActor.mapGenerateOfficer.gridDict[(currentLevel.mapActor.mapGenerateOfficer.mapVerticalSize - 2).ToString() + ",0"].transform.position +
                              currentLevel.mapActor.mapGenerateOfficer.gridDict[(currentLevel.mapActor.mapGenerateOfficer.mapVerticalSize - 2).ToString() + ",1"].transform.position) / 2f;

        cameraStartRefObject.position = bottomMidPosition;
        bottomBorder = bottomMidPosition.y;
        topBorder = topMidPosition.y;
    }

    void CalculateMapPortionColumnPoints()   
    {
        LevelActor currentLevel = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>();
        int horizontalSize = currentLevel.mapActor.mapGenerateOfficer.mapHorizontalSize;

        for (int i = 0; i < horizontalSize; i+=2)
        {
            Vector3 columnPoint = (currentLevel.mapActor.mapGenerateOfficer.gridDict["0," + i.ToString()].transform.position +
                              currentLevel.mapActor.mapGenerateOfficer.gridDict["0," + (i+1).ToString()].transform.position) / 2f;
            //Transform tempRefPoint = Instantiate(cameraStartRefObject, columnPoint, Quaternion.identity);

            mapPortionColumnPoints.Add(columnPoint.x);
        }
    }

    public void CameraHorizontalMovement(bool rightMove) 
    {
        int addition = rightMove ? 1 : -1;
        currentColumn = Mathf.Clamp(currentColumn + addition, 0, mapPortionColumnPoints.Count-1);
        CameraManager.instance.camera.transform.DOMoveX(mapPortionColumnPoints[currentColumn], CameraHorizontalMoveDuration);
    }

    public void CameraVerticalMovement(float moveCoefficient)
    {
        float newYPos = Mathf.Clamp(CameraManager.instance.camera.transform.position.y - (moveCoefficient * CameraVerticalSlideSpeed), bottomBorder, topBorder);
        Vector3 camNewPos = new Vector3(CameraManager.instance.camera.transform.position.x,
                                        newYPos,
                                        CameraManager.instance.camera.transform.position.z);
        CameraManager.instance.camera.transform.position = Vector3.SmoothDamp(CameraManager.instance.camera.transform.position, camNewPos, ref refVelocityVerticalSlide, CameraVerticalSlideSmoothTime);
    }


    [Button("CameraPosition")]
    void ButtonCamReposition() 
    {
        CameraManager.instance.camera.transform.position = cameraStartRefObject.position + new Vector3(0f, 0f, -10f);
    }
}
