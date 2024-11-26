using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : SaveSystem
{
    public static float startTime;
    [SerializeField] public static float playTime = 20;
    [SerializeField] GameObject recordOBJ;
    public static int amount = 0;
    public static SaveData data = new SaveData();
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] GameObject keyBoard;
    [SerializeField] Collider con1;
    [SerializeField] Collider con2;
    [SerializeField] Collider con3;
    [SerializeField] Collider con4;
    [SerializeField] Collider con5;
    [SerializeField] GameObject Login;
    [SerializeField] GameObject Adjust;
    [SerializeField] GameObject Create;
    [SerializeField] GameObject Level;
    [SerializeField] GameObject Gaming;
    [SerializeField] GameObject End;
    [SerializeField] Image Choose;
    [SerializeField] GameObject upCheckUI;
    [SerializeField] GameObject downCheckUI;
    [SerializeField] GameObject adjustCon;
    [SerializeField] GameObject enterError;
    [SerializeField] GameObject createError;
    [SerializeField] GameObject scoreBreakUI;
    [SerializeField] GameObject distanceBreakUI;
    int difficult;
    bool Adjusting = false;
    bool colliderUp = false;
    bool colliderDown = false;
    bool upAdjustDone = false;
    bool downAdjustDone = false;
    Vector3 upAdjustPos;
    Vector3 downAdjustPos;
    Vector3 checkPos;
    float adjustTime;
    bool scoreBreak = false;
    bool distanceBreak = false;

    //掛在網子上

    public class SaveData
    {
        //放入要儲存的變數
        //記得儲存前要更新這裡的數據
        public float score;
        public float moveDis;
    }

    public class MoveData
    {
        Vector3 pos;
        public float time;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Adjusting)
        {
            CheckAdjust();
        }
        else
        {
            adjustTime = Time.time;
        }
        if (Gaming.activeSelf)
        {
            if(Time.time - startTime > playTime)
            {
                Gaming.SetActive(false);
                End.SetActive(true);
                EndGame();
            }
        }
    }
    void SetUI()
    {
        scoreUI.SetText(data.score.ToString());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.tag == "Butterfly")
            {
                CatchButterfly(collision);
                amount -= 1;
            }
        }
    }


    void CatchButterfly(Collision collision)
    {
        data.score++;
        Destroy(collision.gameObject);
    }

    //void CheckEndGame()
    //{
    //    if(Time.time - startTime > playTime)
    //    {
    //        EndGame();
    //    }
    //}

    void EndGame()
    {
        if(data.score > CheckID.bestScore)
        {
            scoreBreak = true;
        }
        if (data.moveDis > CheckID.bestDis)
        {
            distanceBreak = true;
        }
        BreakRecord();
        GameObject[] Butterfly = GameObject.FindGameObjectsWithTag("Butterfly");
        ButterflyBorn.setPos = false;
        for(int i = 0;i < Butterfly.Length; i++)
        {
            Destroy(Butterfly[i]);
        }

    }
    //如果打破最高紀錄
    void BreakRecord()
    {
        End.GetComponent<SetEnd_UI>().SetUI();
        if (scoreBreak)
        {
            scoreBreakUI.SetActive(true);
            CheckID.bestScore = data.score;
        }
        else
        {
            scoreBreakUI.SetActive(false);
            data.score = CheckID.bestScore;
        }
        if (distanceBreak)
        {
            distanceBreakUI.SetActive(true);
            CheckID.bestDis = data.moveDis;
        }
        else
        {
            distanceBreakUI.SetActive(false);
            data.moveDis = CheckID.bestDis;
        }
        string fileName = string.Format("{0}_GameData", CheckID.playerID);
        Save(data, fileName);
    }

    public void SetKeyboard()
    {
        con1.enabled = false;
        con2.enabled = false;
        con3.enabled = false;
        con4.enabled = false;
        con5.enabled = false;
        keyBoard.SetActive(true);
    }
    public void KeyboardCon()
    {
        con1.enabled = true;
        con2.enabled = true;
        con3.enabled = true;
        con4.enabled = true;
        con5.enabled = true;
        keyBoard.SetActive(false);
    }
    public void But0()
    {
        CheckID.playerID = CheckID.playerID + "0";
    }
    public void But1()
    {
        CheckID.playerID = CheckID.playerID + "1";
    }
    public void But2()
    {
        CheckID.playerID = CheckID.playerID + "2";
    }
    public void But3()
    {
        CheckID.playerID = CheckID.playerID + "3";
    }
    public void But4()
    {
        CheckID.playerID = CheckID.playerID + "4";
    }
    public void But5()
    {
        CheckID.playerID = CheckID.playerID + "5";
    }
    public void But6()
    {
        CheckID.playerID = CheckID.playerID + "6";
    }
    public void But7()
    {
        CheckID.playerID = CheckID.playerID + "7";
    }
    public void But8()
    {
        CheckID.playerID = CheckID.playerID + "8";
    }
    public void But9()
    {
        CheckID.playerID = CheckID.playerID + "9";
    }
    public void ButBack()
    {
        CheckID.playerID = CheckID.playerID.Substring(0, CheckID.playerID.Length -1);
    }
    public void LoginCon()
    {
        if (CheckID.checkID())
        {
            Login.SetActive(false);
            Adjust.SetActive(true);
            Adjusting = true;
        }
        else
        {
            StartCoroutine(LoginError());
        }
    }
    public void GoToCreateCon()
    {
        Login.SetActive(false);
        Create.SetActive(true);
    }
    public void CreateCon()
    {
        if (CheckID.checkID())
        {
            StartCoroutine(CreatingError());
        }
        else
        {
            string fileName = string.Format("{0}_GameData", CheckID.playerID);
            Save(data, fileName);
            Create.SetActive(false);
            Adjust.SetActive(true);
            Adjusting = true;
        }
    }
    public void AdjustCon()
    {
        adjustCon.SetActive(false);
        upCheckUI.SetActive(false);
        downCheckUI.SetActive(false);
        upAdjustDone = false;
        downAdjustDone = false;
        Adjust.SetActive(false);
        Adjusting = false;
        Level.SetActive(true);
    }
    public void Level1()
    {
        difficult = 1;
        Choose.transform.localPosition = new Vector3(-61f, Choose.transform.localPosition.y, Choose.transform.localPosition.z);
    }
    public void Level2()
    {
        difficult = 2;
        Choose.transform.localPosition = new Vector3(0.27f, Choose.transform.localPosition.y, Choose.transform.localPosition.z);
    }
    public void Level3()
    {
        difficult = 3;
        Choose.transform.localPosition = new Vector3(60.6f, Choose.transform.localPosition.y, Choose.transform.localPosition.z);
    }
    public void EnterGame()
    {
        ButterflyBorn.setPos = true;
        ButterflyBorn.bornDirection = Camera.main.transform.forward;
        ButterflyBorn.bornDirection = Vector3.ClampMagnitude(ButterflyBorn.bornDirection * 1000, ButterflyBorn.bornDistance);
        Level.SetActive(false);
        Gaming.SetActive(true);
        startTime = Time.time;
        Debug.Log(startTime);
    }
    public void CheckAdjust()
    {
        if (colliderUp)
        {
            if (!upAdjustDone)
            {
                if (Vector3.Distance(checkPos, this.transform.position) > 0.018f)
                {
                    adjustTime = Time.time;
                    checkPos = this.transform.position;
                }
                else if (Time.time - adjustTime > 2f)
                {
                    upAdjustPos = this.transform.position;
                    upCheckUI.SetActive(true);
                    upAdjustDone = true;
                }
            }
        }
        else if (colliderDown)
        {
            if (!downAdjustDone)
            {
                if (Vector3.Distance(checkPos, this.transform.position) > 0.015f)
                {
                    adjustTime = Time.time;
                    checkPos = this.transform.position;
                }
                else if (Time.time - adjustTime > 2f)
                {
                    downAdjustPos = this.transform.position;
                    downCheckUI.SetActive(true);
                    downAdjustDone = true;
                }
            }
        }

        if(upAdjustDone && downAdjustDone)
        {
            ButterflyBorn.bornDistance = (Vector3.Distance(Camera.main.transform.position, upAdjustPos) + Vector3.Distance(Camera.main.transform.position, downAdjustPos)) / 2 + 0.15f;
            adjustCon.SetActive(true);
        }
    }
    void OnCollisionStay(Collision collider)
    {
        if (collider.gameObject.tag == "upAdjust")
        {
            colliderUp = true;
            colliderDown = false;
        }
        else if(collider.gameObject.tag == "downAdjust")
        {
            colliderDown = true;
            colliderUp = false;
        }
        else
        {
            colliderUp = false;
            colliderDown = false;
        }
    }
    IEnumerator LoginError()
    {
        enterError.SetActive(true);
        yield return new WaitForSeconds(2f);
        enterError.SetActive(false);
    }
    IEnumerator CreatingError()
    {
        createError.SetActive(true);
        yield return new WaitForSeconds(2f);
        createError.SetActive(false);
    }
    public void playAgainCon()
    {
        scoreBreak = false;
        distanceBreak = false;
        End.SetActive(false);
        Adjust.SetActive(true);
    }
}
