using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MapGenerateOfficer : SerializedMonoBehaviour
{
    [SerializeField] MapActor mapActor;
    public int mapVerticalSize, mapHorizontalSize;
    [SerializeField] float verticalOffset, horizontalOffset, gridScaleCoefficient;
    [SerializeField] GameObject mapTileActor;

    public Dictionary<string, MapTileActor> gridDict = new Dictionary<string, MapTileActor>();

    public void GenerateMap() 
    {
        ClearTheMapTileContainer();
        for (int i = 0; i < mapVerticalSize; i++)
        {
            for (int i2 = 0; i2 < mapHorizontalSize; i2++)
            {
                CreateTheMapTile(i, i2);
            }
        }
    }

    void CreateTheMapTile(int verticalIndex, int horizontalIndex)
    {
        GameObject tempMapTile = Instantiate(mapTileActor, mapActor.mapTileContainer);
        gridDict.Add((verticalIndex.ToString()+","+ horizontalIndex.ToString()), tempMapTile.GetComponent<MapTileActor>());
        tempMapTile.GetComponent<MapTileActor>().MapTilePrepare(new List<int>() {verticalIndex, horizontalIndex}, mapActor);
        tempMapTile.transform.localScale *= gridScaleCoefficient;
        float oddTileAddition = ((horizontalIndex % 2) == 0) ? 0 : verticalOffset / 2;
        tempMapTile.transform.position = new Vector3(horizontalIndex * horizontalOffset* gridScaleCoefficient, ((verticalIndex * verticalOffset) + oddTileAddition) * gridScaleCoefficient, 0f);
    }

    void ClearTheMapTileContainer()
    {
        gridDict = new Dictionary<string, MapTileActor>();
        while (mapActor.mapTileContainer.childCount > 0) 
        {
            DestroyImmediate(mapActor.mapTileContainer.GetChild(0).gameObject);
        }
    }

    [Button("Generate The Map")]
    void ButtonGenerateMap() 
    {
        GenerateMap();
    }

}
