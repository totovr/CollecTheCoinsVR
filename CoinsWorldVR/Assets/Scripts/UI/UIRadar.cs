using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRadar : MonoBehaviour
{
    [HideInInspector]
    public float InsideRadarDistance = 40;
    
    [HideInInspector]
    public float CoinSizePercentage = 0.05f; 

    public GameObject CoinImageGold;
    public GameObject CoinImageSilver;
    public GameObject CoinImageBronze;

    private RawImage RawImageRadarBackground; // The minimap

    private Transform PlayerTransform; 

    private float RadarWidth;  
    private float RadarHeight;  

    private float CoinWidth; 
    private float CoinHeight; 


    // Use this for initialization
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("PlayerFPS").transform;
        RawImageRadarBackground = GetComponent<RawImage>();

        RadarWidth = RawImageRadarBackground.rectTransform.rect.width;
        RadarHeight = RawImageRadarBackground.rectTransform.rect.height;

        CoinWidth = RadarWidth * CoinSizePercentage / 100;
        CoinHeight = RadarHeight * CoinSizePercentage / 100;
    }

    // Update is called once per frame
    void Update()
    {

        RemoveAllObjectsFromMinimap();
        FindAndDisplayObjectsForTag("Coin");

    }

    //Muestra los objetos en el minimapa con la etiqueta dada
    private void FindAndDisplayObjectsForTag(string tag)
    {

        Vector3 _playerPosition = PlayerTransform.position;
        GameObject[] _allTheCoins = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject _coin in _allTheCoins)
        {
            Vector3 _coinPosition = _coin.transform.position;
            float _distanceToCoin = Vector3.Distance(_coinPosition, _playerPosition);

            if (_distanceToCoin <= InsideRadarDistance)
            {
                Vector3 _normalisedCoinPosition = NormalisedPosition(_playerPosition, _coinPosition);
                Vector2 _coinMinimapPosition = CalculateObjectPosition(_normalisedCoinPosition);

                Coin _coinScript = _coin.GetComponent<Coin>(); // Get the script of the current coin

                GameObject _coinPrefab = null;

                if (_coinScript.Type == CoinType.BRONZE)
                {
                    _coinPrefab = CoinImageBronze;
                }
                else if (_coinScript.Type == CoinType.SILVER)
                {
                    _coinPrefab = CoinImageSilver;
                }
                else
                {
                    _coinPrefab = CoinImageGold;
                }
                DrawObjectInMinimap(_coinMinimapPosition, _coinPrefab);
            }

        }
    }

    // Delate all the coins of the map 
    private void RemoveAllObjectsFromMinimap()
    {
        GameObject[] _allTheCoins = GameObject.FindGameObjectsWithTag("MiniMap");
        foreach (GameObject _coin in _allTheCoins)
        {
            Destroy(_coin);
        }
    }

    // It returns the vector that leaves the player's position and points where the coin is, normalized to the size of the minimap
    private Vector3 NormalisedPosition(Vector3 _playerPos, Vector3 _targetPos)
    {
        float _normalisedXPosition = (_targetPos.x - _playerPos.x) / InsideRadarDistance;
        float _normalisedZPosition = (_targetPos.z - _playerPos.z) / InsideRadarDistance;

        return new Vector3(_normalisedXPosition, 0, _normalisedZPosition);
    }

    // Given an object in the 3D world, what coordinates of the 2D world are you
    private Vector2 CalculateObjectPosition(Vector3 _targetPos)
    {

        // Angle of between the player and the coin
        float _angleTarget = Mathf.Atan2(_targetPos.x, _targetPos.z) * Mathf.Rad2Deg; // angle of the target
        float _anglePlayer = PlayerTransform.eulerAngles.y; // angle of the user view

        float _angleRadarDegrees = _angleTarget - _anglePlayer - 90; // Add an offset to look counterclockwise
        float _angleRadarRad = _angleRadarDegrees * Mathf.Deg2Rad; // Transform deg to rad

        // Calculate the coordinates x and y in the minimap
        float NormalisedDistanceFromPlayerToTarget = _targetPos.magnitude;

        float _minimapX = NormalisedDistanceFromPlayerToTarget * Mathf.Cos(_angleRadarRad); 
        float _minimapY = NormalisedDistanceFromPlayerToTarget * Mathf.Sin(_angleRadarRad);

        // The position was previously normalized so it must be changed again 
        _minimapX *= RadarWidth / 2;
        _minimapY *= RadarHeight / 2;

        // Add the offset to show it in the center of the minimap 
        _minimapX += RadarWidth / 2;
        _minimapY += RadarHeight / 2;

        return new Vector2(_minimapX, _minimapY);
    }

    // Draw the prefab in the indicated position;
    private void DrawObjectInMinimap(Vector2 _pos, GameObject _objectPrefab)
    {
        GameObject MinimapGO = Instantiate(_objectPrefab);

        MinimapGO.transform.SetParent(transform.parent); // it will be a part of the same MiniMapImageRenderer

        RectTransform _rt = MinimapGO.GetComponent<RectTransform>();
        _rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _pos.x, CoinWidth);
        _rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, _pos.y, CoinHeight);
    }

}
