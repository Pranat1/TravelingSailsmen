using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Point : MonoBehaviour, IPointerClickHandler
{
    private bool selected = false;
    [SerializeField] private GameObject lineRendererPrefab;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject[] hiPoints = GameObject.FindGameObjectsWithTag("Highlight");
        GameObject cameraprefab = GameObject.Find("MainCamera");
        GameObject gridSpawner = GameObject.Find("GridSpawner");
        GridSpawner scriptSpawner = gridSpawner.GetComponent<GridSpawner>();
        List<List<GameObject>> gridIn = scriptSpawner.GetArrayOf();
        bool NoSelected = true;
        bool allColl = true;
        
        //Debug.Log(gridIn.Count);
        for (int i = 0; i < gridIn.Count; i++)
        {
            for (int j = 0; j < gridIn[i].Count; j++)
            {
                if (gridIn[i][j].GetComponent<Point>().GetSelected())
                {
                    if((int) gameObject.transform.position.x == i - 6|| (int)  gameObject.transform.position.y == j-4)
                    {
                    DrowLine(transform.position, gridIn[i][j]);
                    IfCollided(hiPoints);
                    scriptSpawner.AddPointsLine(transform.position);
                    selected = true;
                    }
                    NoSelected = false;
                }
            }
        }

        if (NoSelected)
        {
            IfCollided(hiPoints);
            scriptSpawner.AddPointsLine(transform.position);
            selected = true;
        }
    
        for (int k = 0; k < hiPoints.Length; k++){
        HighlightPoint hiPointSc1 = hiPoints[k].GetComponent<HighlightPoint>();
        if (!hiPointSc1.collisionIsColliding())
        {
            
            allColl = false;
        }
        }
        if (allColl)
        {
            SceneLoader sceneLoded = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
            sceneLoded.LoadNextScene();
        }
        }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool GetSelected(){
        return selected;
    }
    public void SetSelected(bool selun){
        selected = selun;
    }
    public void DrowLine(Vector3 position1 ,GameObject endPoint){
        GameObject lineObject = Instantiate(lineRendererPrefab, position1, transform.rotation);
        LineRenderer lineRendererObj = lineObject.GetComponent<LineRenderer>();
        lineRendererObj.SetPosition(1, new Vector3(endPoint.transform.position.x, endPoint.transform.position.y, 0));
        lineRendererObj.SetPosition(0, position1);
        endPoint.GetComponent<Point>().SetSelected(false);
        SetSelected(false);
        GameObject gameStats = GameObject.Find("GameStats");
        gameStats.GetComponent<GameStats>().AddToLineLength((position1 - new Vector3(endPoint.transform.position.x, endPoint.transform.position.y, 0)).magnitude);

         
    }
    public void IfCollided(GameObject[] hiPoints)
    {
    for (int k = 0; k < hiPoints.Length; k++)
    {
        HighlightPoint hiPointSc = hiPoints[k].GetComponent<HighlightPoint>();
        if (hiPointSc.transform.position == transform.position)
        {
            hiPointSc.SetIsColliding(true);
        }
    }
    }


}
