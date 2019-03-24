using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Terrain WorldTerrain;
    public LayerMask TerrainLayer;
    public static float TerrainLeft, TerrainRight, TerrainTop, TerrainBottom, TerrainWidth, TerrainLenght, TerrainHeight;

    private TerrainCollider terrainCollider;

    private int numberOfEnemyOne = 50;
    private int numberOfEnemyTwo = 40;

    // Start is called before the first frame update
    void Awake()
    {
        terrainCollider = GetComponent<TerrainCollider>();

        TerrainLeft = WorldTerrain.transform.position.x;
        TerrainBottom = WorldTerrain.transform.position.z;

        TerrainWidth = WorldTerrain.terrainData.size.x;
        TerrainLenght = WorldTerrain.terrainData.size.z;
        TerrainHeight = WorldTerrain.terrainData.size.y;

        TerrainRight = TerrainLeft + TerrainWidth;
        TerrainTop = TerrainBottom + TerrainLenght;

        InstantiateRandomPosition("StoneMonster", numberOfEnemyOne, 2f);
        InstantiateRandomPosition("Ghost_Brown", numberOfEnemyTwo, 15f);

        GlobalStaticVariables.totalEnemyCounter = numberOfEnemyOne + numberOfEnemyTwo;

    }

    public void InstantiateRandomPosition(string _resource, int _amount, float _addedHeight)
    {
        int i = 0;
        float terrainHeight = 0f;
        RaycastHit hit;
        float randomPositionX, randomPositionY, randomPositionZ;
        Vector3 randomPosition = Vector3.zero;

        int mountainCompensation = 70; // This variable is to reduce the size of the spawn area

        do
        {
            i++;
            // Generate the positions
            randomPositionX = Random.Range(TerrainLeft + mountainCompensation, TerrainRight - mountainCompensation);
            randomPositionZ = Random.Range(TerrainBottom + mountainCompensation, TerrainTop - mountainCompensation);

            if (Physics.Raycast(new Vector3(randomPositionX, 9999f, randomPositionZ),
                Vector3.down,
                out hit,
                Mathf.Infinity, 
                TerrainLayer))
            {
                terrainHeight = hit.point.y;
            }

            randomPositionY = terrainHeight + _addedHeight;
            randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);

            // Note that the prefabs must been in the Resources folder
            Instantiate(Resources.Load(_resource, typeof(GameObject)), randomPosition, Quaternion.identity);

        } while (i < _amount);
    }
}
