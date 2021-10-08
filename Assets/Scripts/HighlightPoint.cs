using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPoint : MonoBehaviour
{
    bool isColliding = false;
    SceneLoader objScene;
    GridSpawner scriptSpawner;
    GameObject[] highlightPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        objScene = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        GameObject gridSpawner = GameObject.Find("GridSpawner");
        GridSpawner scriptSpawner = gridSpawner.GetComponent<GridSpawner>();
        GameObject[] highlightPoints = GameObject.FindGameObjectsWithTag("Highlight");
    }

    // Update is called once per frame
    void Update()
    {
        


        
    }
    
    
    public bool collisionIsColliding(){
        return isColliding;
    }
    public void SetIsColliding(bool col)
    {
        isColliding = col;
    }

}
