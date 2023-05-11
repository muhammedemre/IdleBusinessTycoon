using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MapTileModelOfficer : MonoBehaviour
{
    [SerializeField] MapTileActor mapTileActor;
    [SerializeField] GameObject bottomLeftAddition, bottomRightAddition, topLeftAddition, topRightAddition;

    public enum GridType 
    {
        BottomLeftCornerTile, BottomRightCornerTile, BottomMidTile, MidTile, TopLeftCornerTile, TopRigtCornerTile, TopMidTile
    }

    public GridType selectedType;

    public void SelectTheModel(GridType selectedModel)
    {
        RefreshTheGrid();
        ShapeTheGrid();

        void ShapeTheGrid()
        {
            switch (selectedModel)
            {
                case GridType.BottomLeftCornerTile:
                    bottomLeftAddition.SetActive(true);
                    topLeftAddition.SetActive(true);
                    break;
                case GridType.BottomRightCornerTile:
                    bottomRightAddition.SetActive(true);
                    topRightAddition.SetActive(true);
                    break;
                case GridType.BottomMidTile:
                    bottomLeftAddition.SetActive(true);
                    bottomRightAddition.SetActive(true);
                    break;
                case GridType.MidTile:
                    break;
                case GridType.TopLeftCornerTile:
                    topLeftAddition.SetActive(true);
                    bottomLeftAddition.SetActive(true);
                    break;
                case GridType.TopRigtCornerTile:
                    topRightAddition.SetActive(true);
                    bottomRightAddition.SetActive(true);
                    break;
                case GridType.TopMidTile:
                    topLeftAddition.SetActive(true);
                    topRightAddition.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    void RefreshTheGrid() 
    {
        bottomLeftAddition.SetActive(false);
        bottomRightAddition.SetActive(false);
        topLeftAddition.SetActive(false);
        topRightAddition.SetActive(false);
    }

    public void MapTileTypeDecider(List<int> position)  
    {
        if (position[1] == 0) // LeftSide
        {
            AdditionPrepare(topLeftAddition);
        }
        if (position[0] == 0 && (position[1] % 2) == 0) // BottomSide
        {
            AdditionPrepare(bottomLeftAddition);
        }
        if (position[1] == mapTileActor.relatedMap.mapGenerateOfficer.mapHorizontalSize-1) // RightSide
        {
            AdditionPrepare(topRightAddition);
            if (position[0] == 0)
            {
                AdditionPrepare(bottomRightAddition);
            }
        }
        if (position[0] == mapTileActor.relatedMap.mapGenerateOfficer.mapVerticalSize - 1 && (position[1] % 2) == 1) // TopSide
        {
            AdditionPrepare(topLeftAddition);
            if (position[1] == mapTileActor.relatedMap.mapGenerateOfficer.mapHorizontalSize - 2)
            {
                AdditionPrepare(topRightAddition);
            }
        }
        if (mapTileActor.relatedMap.mapGenerateOfficer.mapHorizontalSize % 2 == 0) 
        {
            if (position[0] == 0 && position[1] == mapTileActor.relatedMap.mapGenerateOfficer.mapHorizontalSize - 2)
            {
                AdditionPrepare(bottomRightAddition);
            }
        }        
    }

    void AdditionPrepare(GameObject additionObject) 
    {
        additionObject.SetActive(true);
        Color newColor = new Color(1f, 1f, 1f, 0.5f);
        additionObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = newColor;
    }
    

    #region Button

    [Title("Select Model Button")]
    [Button("Select Model", ButtonSizes.Large)]
    void ButtonSelectTheModel(GridType selectedModel)
    {
        SelectTheModel(selectedModel);
    }
    #endregion
}
