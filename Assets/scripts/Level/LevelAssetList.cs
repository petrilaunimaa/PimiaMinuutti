using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAssetList : MonoBehaviour {

    public string title = "Level";

    public int wallCountMin;
    public int wallCountMax;

    public List<GameObject> walls;
    public List<GameObject> decorations;

    public GameObject floorSpritePrefab;


}
