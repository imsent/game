using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Objects/Map")]

public class Map : ScriptableObject
{
    // Start is called before the first frame update
    public int mapIndex;
    public string mapName;
    public string mapDescription;
    public Sprite mapImage;
    public string sceneToLoad;
}
