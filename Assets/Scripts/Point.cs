using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Point : MonoBehaviour, IPointerClickHandler
{
    private bool selected = false;
    [SerializeField] private GameObject lineRendererPrefab;
    [SerializeField] private GameObject lineRendererPrefab1;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameStats statsGame = GameObject.Find("GameStats").GetComponent<GameStats>();
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
                    IfCollided(hiPoints, gridIn[i][j]);
                    scriptSpawner.AddPointsLine(transform.position);
                    selected = true;
                    }
                    NoSelected = false;
                }
            }
        }

        if (NoSelected)
        {
            IfCollided(hiPoints, null);
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
            GameStats gameStats = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>();
            if(gameStats.totalLineLength == gameStats.path[1000].x)
            {
                scriptSpawner.ActivateButton(true);
            }
            Dictionary<int, Vector3> pathAns = statsGame.path;
            List<int> keyspathAns = new List<int>(pathAns.Keys);
            keyspathAns.Sort();
            Debug.Log(keyspathAns);

            for (int l = 0; l < keyspathAns.Count-2; l++)
            {
                Vector3 distanceGap = pathAns[keyspathAns[l+1]] - pathAns[keyspathAns[l]];
                GameObject lineObject1 = Instantiate(lineRendererPrefab1, pathAns[keyspathAns[l]], Quaternion.identity);
                LineRenderer lineRendererObj = lineObject1.GetComponent<LineRenderer>();
                Color c1 = Color.green;
                lineRendererObj.SetColors(c1, c1);
                lineRendererObj.SetPosition(0, pathAns[keyspathAns[l]]);
                Vector3 firstStep = new Vector3(distanceGap.x + pathAns[keyspathAns[l]].x, pathAns[keyspathAns[l]].y,pathAns[keyspathAns[l]].z);
                lineRendererObj.SetPosition(1,firstStep);
                Vector3 secondStep = new Vector3(distanceGap.x + pathAns[keyspathAns[l]].x, distanceGap.y + pathAns[keyspathAns[l]].y, pathAns[keyspathAns[l]].z);
                lineRendererObj.SetPosition(2,secondStep);

            }
            /*
            SceneLoader sceneLoded = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
            sceneLoded.LoadNextScene();
            */
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
    public void IfCollided(GameObject[] hiPoints, GameObject selectedPoint)
    {
    for (int k = 0; k < hiPoints.Length; k++)
    {
        HighlightPoint hiPointSc = hiPoints[k].GetComponent<HighlightPoint>();
        if (hiPointSc.transform.position == transform.position)
        {
            hiPointSc.SetIsColliding(true);
        }
        if (selectedPoint != null){
            if (hiPointSc.transform.position.x == transform.position.x){
                if (transform.position.y < hiPointSc.transform.position.y && hiPointSc.transform.position.y < selectedPoint.transform.position.y){
                    hiPointSc.SetIsColliding(true);
                }
                if (transform.position.y > hiPointSc.transform.position.y && hiPointSc.transform.position.y > selectedPoint.transform.position.y){
                    hiPointSc.SetIsColliding(true);
                }
        }
            if (hiPointSc.transform.position.y == transform.position.y){
                if (transform.position.x < hiPointSc.transform.position.x && hiPointSc.transform.position.x < selectedPoint.transform.position.x){
                    hiPointSc.SetIsColliding(true);
                }
                if (transform.position.x > hiPointSc.transform.position.x && hiPointSc.transform.position.x > selectedPoint.transform.position.x){
                    hiPointSc.SetIsColliding(true);
                }
        
        }
        }


    }
    }
}
