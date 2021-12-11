using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [SerializeField] private List<LevelAssetList> levels;

    [Header("Bounds")]
    public float boundMinX;
    public float boundMaxX;
    public float boundMinY;
    public float boundMaxY;

    [SerializeField] private List<GameObject> spawnPositions;
    private List<GameObject> unusedPositions = new List<GameObject>();

    private LevelAssetList currentLevel = null;

    void Start() {
        currentLevel = levels[Random.Range(0, levels.Count)];
        int wallCount = Random.Range(currentLevel.wallCountMin, currentLevel.wallCountMax+1);
        InitializeUnusedPositions();

        Instantiate<GameObject>(currentLevel.floorSpritePrefab, null);

        for (int i=0; i<wallCount; ++i) {
            Wall wallPrefab = currentLevel.walls[Random.Range(0, currentLevel.walls.Count)];
            int positionIndex = Random.Range(0, unusedPositions.Count);
            Vector3 position = unusedPositions[positionIndex].transform.position;
            Wall wallObject = Object.Instantiate<Wall>(wallPrefab, null);
            wallObject.transform.position = position;
            unusedPositions.RemoveAt(positionIndex);
        }

    }

    void InitializeUnusedPositions() {
        foreach (GameObject go in spawnPositions) {
            unusedPositions.Add(go);
        }
    }
}
