using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private int lineindex = 0;
    int lengthGrid = 9;
    int widthGrid = 12;
    [SerializeField] private int destinations = 3;
    [SerializeField] private GameObject highlightPoint;
    [SerializeField] private GameObject pointPrefab;
    public List<List<GameObject>> arrayOf;
    List<Vector3> pointsLine = new List<Vector3>();
    void Awake()
    {
        System.Random random = new System.Random(); 
        for (int k = 0; k < destinations; k++){
            Instantiate(highlightPoint, new Vector3(random.Next(-6, 5), random.Next(-4, 4), 0), highlightPoint.transform.rotation);
        }
        GameObject StorePointsPrefab = GameObject.Find("StorePoints");
        arrayOf = new List<List<GameObject>>();
        for (int i = 0; i<  widthGrid; i++)
        {
            List<GameObject> inBetween = new List<GameObject>();
            for (int j = 0; j<  lengthGrid; j++)
            {
               GameObject childObj = Instantiate(pointPrefab, new Vector3(i - widthGrid/2, j - lengthGrid/2, 0), pointPrefab.transform.rotation);
               childObj.transform.parent = StorePointsPrefab.transform;
               inBetween.Add(childObj);
            }
            arrayOf.Add(inBetween);
        }
    }
    public List<List<GameObject>> GetArrayOf(){
        return arrayOf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int Getlineindex()
    {
        return lineindex;
    }
    public void Setlineindex(int indexGiven)
    {
        lineindex = indexGiven;
    }
    public List<Vector3> GetPointsLine()
    {
        return pointsLine;
    }
    public void AddPointsLine(Vector3 addPoint)
    {
        pointsLine.Add(addPoint);
    }
}
