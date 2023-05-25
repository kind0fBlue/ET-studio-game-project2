using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManger : MonoBehaviour
{
    public int[,] grid;

    public GameObject[,] gridObj;

    public int mapSize = 10;

    public GameObject birthPoint;

    public GameObject[] buildingTypeOne;

    public GameObject[] buildingTypeTwo;
    
    public GameObject[] monster;

    public GameObject[] monsterObjNum;

    // public GameObject trigger;

    // private GameObject triggerObj;

    // public bool isTriggerBuilding = false;

    // public int generationProbability = 45;

    public int buildingScale = 20;

    public int monsterDis = 3;

    public int monsterNum = 2;

    public int levelUpScore = 10;

    private int upScore = 10;

    public int addMonsterBum = 1;
    
    private int maxMonsterNum = 20;


    void Start (){
        grid = new int[mapSize,mapSize];
        gridObj = new GameObject[mapSize,mapSize];
        GenerateBuilding();

    }

    void Update(){
        if (GameObject.FindWithTag("Player") == null) {
            return;
        }
        
        float x = GameObject.FindWithTag("Player").transform.position.x;
        float z = GameObject.FindWithTag("Player").transform.position.z;

        monsterObjNum = GameObject.FindGameObjectsWithTag("Monster");

        if(monsterObjNum.Length < monsterNum){
            GenerateMonster(x, z);
        }

        if(UI.getInstance().getScore() > levelUpScore){
            levelUpScore += upScore;
            if(monsterNum < maxMonsterNum){
                monsterNum = monsterNum + addMonsterBum;
            }
        } 
        
    }

    public void GenerateMonster(float x, float z){
        int r = Random.Range(0,4);
        Debug.Log(r);

        switch(r){
            case 0:

                Instantiate(monster[Random.Range(0,2)],new Vector3(x-buildingScale*monsterDis,1,z),Quaternion.identity);
                break;
            case 1:

                Instantiate(monster[Random.Range(0,2)],new Vector3(x+buildingScale*monsterDis,1,z),Quaternion.identity);
                break;
            case 2:

                Instantiate(monster[Random.Range(0,2)],new Vector3(x,1,z-buildingScale*monsterDis),Quaternion.identity);
                break;
            case 3:

                Instantiate(monster[Random.Range(0,2)],new Vector3(x,1,z+buildingScale*monsterDis),Quaternion.identity);
                break;

        }
    }

    public void GenerateBuilding(){
        for (int i = buildingScale / 2; i < mapSize; i += buildingScale)
        {
            for (int j = buildingScale / 2; j < mapSize; j += buildingScale)
            {   
                int r = Random.Range(-9,9);

                if(i == mapSize/2 && j == mapSize/2){
                    gridObj[i,j] = Instantiate(birthPoint,transform.position + new Vector3(i,0,j),Quaternion.identity);
                    continue;
                }
                //int a = Random.Range(0,21);

                if(Random.Range(0,21) < 10){
                //if(GetCubeNumber(i,j)<10){
                    gridObj[i,j] = Instantiate(buildingTypeOne[Random.Range(0,buildingTypeOne.Length)],transform.position + new Vector3(i+r,0,j+r),Quaternion.identity);

                }else{
                    gridObj[i,j] = Instantiate(buildingTypeTwo[Random.Range(0,buildingTypeTwo.Length)],transform.position + new Vector3(i+r,0,j+r),Quaternion.identity);

                }
            }
        }

    }



}
