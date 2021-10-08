using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStats : MonoBehaviour
{
    List<Vector3> myList = new List<Vector3>();
    private float totalLineLength = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] hiPoints = GameObject.FindGameObjectsWithTag("Highlight");
        for (int i = 0; i < hiPoints.Length; i++){
            myList.Add(hiPoints[i].transform.position);
        }
        Dictionary<int, Vector3> path = ShortestPath(myList);
        int minVal = Min1(new List<int>(path.Keys));
        List<int> dataval = new List<int>(path.Keys);
        /*
        foreach(int data1 in dataval){
            Debug.Log(data1);
        }
        */
        for (int i = minVal; i < (minVal + path.Count-1); i++)
        {
            Debug.Log(path[i]);
        }
        Debug.Log(path[1000]);
        GameObject textObject1 = GameObject.Find("MinMoves");
        TextMeshProUGUI textCompontnt = textObject1.GetComponent<TextMeshProUGUI>();
        textCompontnt.text = path[1000].x.ToString();


    }
    public Dictionary<int, Vector3> ShortestPath(List<Vector3> myList)
    {

        if (myList.Count == 1){
            var returnVal = new Dictionary<int, Vector3>();
            returnVal[0] = myList[0];
            returnVal[1000] = new Vector3(0,0,0);
            return returnVal;
        }
      float shortestPath = 1000f;
      Dictionary<int, Vector3> shortestPathCopy = new Dictionary<int, Vector3>();
      int indexi = 0;
      Dictionary<int, int>  indexStaEnd = new Dictionary<int, int>();
      Dictionary<int, Vector3> returnVal1 = new Dictionary<int, Vector3>();
      for (int i = 0; i < myList.Count; i++){
          List<Vector3> copymyList = new List<Vector3>(myList);
          copymyList.Remove(copymyList[i]);
          float mindistanceEnd = 1000f;
          shortestPathCopy = ShortestPath(copymyList);
          int lenDict = shortestPathCopy.Count;
          List<int> listInt = new List<int>(shortestPathCopy.Keys);
          int minIndex = Min1(listInt);
          int maxIndex = Max2(listInt);
          float atEnd = DistanceTo(shortestPathCopy[maxIndex], myList[i]);
          float atStart = DistanceTo(shortestPathCopy[minIndex], myList[i]);
          if (atEnd > atStart){
              mindistanceEnd = atStart;
              indexStaEnd[i] = 0;
              }
          else{
              mindistanceEnd = atEnd;
              indexStaEnd[i] = 1;
          }

          float diatencePath = shortestPathCopy[1000].x + mindistanceEnd;
          if (diatencePath < shortestPath)
          {
            shortestPath = diatencePath;
            indexi = i;
            returnVal1 = shortestPathCopy;
          }
      }
    List<int> listInt1 = new List<int>(returnVal1.Keys);
    int minIndex1 = Min1(listInt1);
    int maxIndex2 = Max2(listInt1);
    if (indexStaEnd[indexi] == 0){
      returnVal1[minIndex1-1] = myList[indexi];
    }
    else{
      returnVal1[maxIndex2+1] = myList[indexi];
    }
    returnVal1[1000] = new Vector3(shortestPath, 0,0);
    return returnVal1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddToLineLength(float lineLength){
        GameObject textObject = GameObject.Find("LengthText");
        TextMeshProUGUI lengthText = textObject.GetComponent<TextMeshProUGUI>();
        totalLineLength += lineLength;
        lengthText.text = totalLineLength.ToString();
    }
    private float DistanceTo(Vector3 val1, Vector3 val2)
    {
        return Mathf.Abs(val1.x - val2.x) + Mathf.Abs(val1.y - val2.y);
    }
    private int Min1(List<int> data)
    {
        int loest = data[0];
        for (int i = 0; i< data.Count; i++){
            if (loest > data[i]){
                loest = data[i];
            }
        }
        return loest;
    }

    private int Max2(List<int> data)
    {
        int biggent = -10000;
        int secBiggent = -10000;
        for (int i = 0; i< data.Count; i++){
            if (biggent < data[i]){
                secBiggent = biggent;
                biggent = data[i];
            }
            else if(secBiggent < data[i])
            {
               secBiggent = data[i];
            }
        }

        return secBiggent;
    }
}

/*

            for (int i = 0; i < hiPoints.Length; i++){
            for (int j = 0; j < hiPoints.Length; j++){
            {
                List<int> myGri = new List<int>();
                myGri.Add(i);
                myGri.Add(j);
                Vector3 distance1 = hiPoints[i].transform.position - hiPoints[j].transform.position;
                float totalDis = distance1.x + distance1.y;
                dictForDistances[myGri] = totalDis;
            }
        }
    }
    */