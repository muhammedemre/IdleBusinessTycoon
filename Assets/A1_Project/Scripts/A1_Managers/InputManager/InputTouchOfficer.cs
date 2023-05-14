using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class InputTouchOfficer : InputAbstractOfficer
{
    [ReadOnly] [SerializeField] float touchStartTime = 0;
    [ReadOnly] [SerializeField] Vector2 touchStartPosition, previousTouchPosition;

    [SerializeField] float maxSwipeDuration, minSwipeDistance, maxTouchDistance;
    bool canBeATouch = true;

    bool touchedOnMapTile = false;
    float screenHeight;
    private void Start()
    {
        getInputOfficer.InputTouch += InputTouchProcess;
        screenHeight = Screen.height;
    }

    void InputTouchProcess(bool touchStart, bool touchMoved, bool touchEnded, Vector2 touchPos)
    {

        if (GameManager.instance.currentGameScreen == GameManager.ScreenState.MapScreen)
        {
            if (touchStart)
            {
                touchStartTime = Time.time;
                touchStartPosition = touchPos;
                previousTouchPosition = touchPos;
            }
            else if (touchMoved)
            {
                canBeATouch = CheckCanStillBeATouch(touchPos);
                VerticalSlideProcess(touchPos);
                CheckIsSwipe(touchPos);
                previousTouchPosition = touchPos;
            }
            else if (touchEnded)
            {
                //print("CAN BE TOUCH? : "+ canBeATouch);
            }
        }
        //else
        //{
        //    if (touchStart)
        //    {
        //        touchStartTime = Time.time;
        //        touchStartPosition = touchPos;
        //    }
        //    else if (touchMoved)
        //    {


        //    }
        //    else if (touchEnded)
        //    {

        //    }
        //}       
    }

    void VerticalSlideProcess(Vector2 touchPos) 
    {
        CameraManager.instance.cameraMoveOfficer.CameraVerticalMovement(MoveRate(touchPos.y));
    }

    void CheckIsSwipe(Vector2 touchPos)
    {
        float horizontalDistance = touchStartPosition.x - touchPos.x;
        float verticalDistance = touchStartPosition.y - touchPos.y;
        if ((Mathf.Abs(horizontalDistance) > minSwipeDistance && Mathf.Abs(verticalDistance) < (minSwipeDistance/2f)) && (Time.time < (touchStartTime+maxSwipeDuration)))
        {
            print("Thats a swipe");
            bool rightMove = (horizontalDistance > 0) ? false : true;
            CameraManager.instance.cameraMoveOfficer.CameraHorizontalMovement(rightMove);
        }
    }

    float MoveRate(float touchPosY)
    {
        float differenceBetweenPreviousAndCurrentPosY = touchPosY - previousTouchPosition.y;
        float moveRate = Mathf.Clamp((differenceBetweenPreviousAndCurrentPosY / screenHeight), -1, 1);
        return moveRate;
    }

    bool CheckCanStillBeATouch(Vector2 touchPos) 
    {
        float xDistance = Mathf.Abs(touchPos.x - touchStartPosition.x);
        float yDistance = Mathf.Abs(touchPos.y - touchStartPosition.y);
        if (xDistance > maxTouchDistance || yDistance > maxTouchDistance)
        {
            return false;
        }
        return true;
    }


    bool CheckTouchOnMapTile(Vector2 touchPosition)
    {
        Vector2 touchPositionNormalized = Camera.main.ScreenToWorldPoint(touchPosition);
        RaycastHit2D hit = Physics2D.Raycast(touchPositionNormalized, Vector2.zero);
        if (hit.collider != null && hit.collider.tag == "MapTile")
        {
            print("HitMapTile");
            return true;
        }
        return false;
    }
}
