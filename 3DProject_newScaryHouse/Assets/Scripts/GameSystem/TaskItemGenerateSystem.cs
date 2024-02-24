using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskItemGenerateSystem : IGameSystem
{
    private Transform taskItem = null;
    private GameObject[] taskItemGeneratePosition = null;
    private List<int> nums;
    public TaskItemGenerateSystem(MainGame main) : base(main)
    {
        Initialize();
    }
    public override void Initialize()
    {
        taskItemGeneratePosition = GameObject.FindGameObjectsWithTag("PaperPiecePosition");
        nums = GenerateRandomNumbers();
        for(int i = 0; i < mainGame.GetInstantiateObjDB().PaperPieces.Length;i++)
        {
            taskItem = mainGame.GetInstantiateObjDB().PaperPieces[i].transform;
            InsItem(taskItem, nums[i]);
        }
        
        //if(mainGame.GetInstantiateObjDB().PaperPiece != null)
            //taskItem = mainGame.GetInstantiateObjDB().PaperPiece.transform;
        
        //InsItem(taskItem);
    }
    
    public void InsItem(Transform obj,int insPosNum)
    {
        if(obj != null)
        {
            GameObject.Instantiate(taskItem, taskItemGeneratePosition[insPosNum].transform.position, Quaternion.identity);
        }
    }

    public List<int> GenerateRandomNumbers()
    {
        List<int> numbers = new List<int>();

        for (int i = 0; i < mainGame.GetInstantiateObjDB().PaperPieces.Length; i++)
        {
            int newNumber;
            do
            {
                newNumber = Random.Range(0, taskItemGeneratePosition.Length);
            } while (numbers.Contains(newNumber));

            numbers.Add(newNumber);
        }

        return numbers;
    }
}
