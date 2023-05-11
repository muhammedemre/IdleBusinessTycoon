using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapTileActor : MonoBehaviour
{
    public MapActor relatedMap;
    public MapTileModelOfficer mapTileModelOfficer;
    public List<int> mapTilePosition;
    [SerializeField] TextMeshProUGUI gridPositionText;

    public void MapTilePrepare(List<int> _position, MapActor _relatedMap) 
    {
        mapTilePosition = _position;
        relatedMap = _relatedMap;
        mapTileModelOfficer.MapTileTypeDecider(mapTilePosition);
        gridPositionText.text = mapTilePosition[0].ToString() + "," + mapTilePosition[1].ToString();
    }

    
}
