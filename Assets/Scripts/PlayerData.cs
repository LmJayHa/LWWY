using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;





/*
    ==========================================================
     playerData : 플레이어의 데이터들을 관리하는 스크립트 : 싱글턴

    (---수정사항---)

    21.07.15 : 임재하 : 기본적인 틀 제작
    21.07.20 : 임재하 : phone에 사용된 노트의 스트링변수 제작 -> note
    21.07.23 : 임재하 : phone에 사용된 노트의 스트링변수 제작 -> screenShot
    21.07.26 : 임재하 : 데이터의 세이브와 로드 구현
    21.07.27 : 임재하 : 싱글턴으로 수정
    ==========================================================
     */





public class PlayerData : MonoBehaviour
{
    public static PlayerData playerData
    {
        get
        {
            if (playerD_instance == null)
            {
                playerD_instance = FindObjectOfType<PlayerData>();
            }

            return playerD_instance;
        }
    }//playerData를 싱글턴으로 설정

    private static PlayerData playerD_instance; //싱글턴에 이용된 인스턴스

    public List<int> codesOfHavingItems=new List<int>(); //가지고 있는 아이템들의 아이템 코드
    public int sizeOfCodesOfHavingItems = 0;

    public bool[] isOpenKnoweds = {false, false, false, false, false, false, false, false, false}; //현재 언락된 knowed데이터

    public string[] stringsOfNotes = { "","","","","","","",""}; //플레이어의 노트 스트링 세트 ; 8개로 고정

    public bool[] isSavedPicture = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }; //현재 찍은 사진이 있는지 ; 16개로 고정

    public int usingItemCode; //현재 플레이어가 들고있는 아이템의 코드

    public bool addItem(int code) {
        if (codesOfHavingItems.Count >= 28) return false;

        sizeOfCodesOfHavingItems++;
        codesOfHavingItems.Add(code);
        return true;
    }//아이템을 추가하는 함수 true 리턴시 성공

    public bool addItem_NoPlus(int code)
    {
        if (codesOfHavingItems.Count >= 28) return false;

        codesOfHavingItems.Add(code);
        return true;
    }//아이템을 추가하는 함수 true 리턴시 성공

    private int findItem(int code) {
        for (int i = 0; i < codesOfHavingItems.Count; i++)
        {
            if (codesOfHavingItems[i] == code) { return i; }
        }
        return -1;
    }//아이템의 인덱스를 찾는 함수 -1 리턴시 없음

    public bool removeItem(int code)
    {
        if (sizeOfCodesOfHavingItems == 0) return false;

        int idx = findItem(code);

        if (idx == -1) return false;
        else {
            codesOfHavingItems.RemoveAt(idx);
            sizeOfCodesOfHavingItems--;
            return true;
        }
    }//아이템을 삭제하는 함수 true 리턴시 성공

    public void saveData(int where) {
        Debug.Log("세이브 시작 : PlayerData ");
        string savePath = Application.dataPath + "/savingData/";

        DirectoryInfo dir = new DirectoryInfo(savePath);
        if (!dir.Exists) {
            Directory.CreateDirectory(savePath);
        }
        //경로가 존재하지 않으면 새로 디렉토리 생성

        savePath = savePath + "playerData"+where.ToString()+".dat";

        FileInfo file = new FileInfo(savePath);
        if (!file.Exists)
        { File.Create(savePath).Close() ; }

        FileStream fs = file.OpenWrite();
        StreamWriter sw = new StreamWriter(fs);
        Debug.Log("스트림 작성중 ");
        for (int i = 0; i < isOpenKnoweds.Length; i++)
        {
            sw.WriteLine(isOpenKnoweds[i].ToString());
        }

        for (int i = 0; i < stringsOfNotes.Length; i++)
        {
            sw.WriteLine(stringsOfNotes[i]);
        }

        for (int i = 0; i < isSavedPicture.Length; i++)
        {
            sw.WriteLine(isSavedPicture[i].ToString());
        }

        sw.WriteLine(sizeOfCodesOfHavingItems.ToString());
        for (int i = 0; i < sizeOfCodesOfHavingItems; i++)
        {
            sw.WriteLine(codesOfHavingItems[i].ToString());
        }
        sw.Close();
        fs.Close();
    }//데이터를 세이브시키는 함수

    public void loadData(int where)
    {

        Debug.Log("데이터 로드 시작 : 플레이어 데이터");
        string savePath = Application.dataPath + "/savingData/playerData" + where.ToString() + ".dat";
        if (!File.Exists(savePath)) { return; }

        StreamReader sr = new StreamReader(savePath);

        for (int i = 0; i < isOpenKnoweds.Length; i++)
        {
            isOpenKnoweds[i]=bool.Parse(sr.ReadLine());
        }

        for (int i = 0; i < stringsOfNotes.Length; i++)
        {
            stringsOfNotes[i] = sr.ReadLine();
        }

        for (int i = 0; i < isSavedPicture.Length; i++)
        {
            isSavedPicture[i]= bool.Parse(sr.ReadLine());
        }

        codesOfHavingItems = new List<int>();

        sizeOfCodesOfHavingItems = int.Parse(sr.ReadLine());

        string str;
        for (int i = 0; i < sizeOfCodesOfHavingItems; i++)
        {
            addItem_NoPlus(int.Parse(sr.ReadLine()));
        }

        sr.Close();
    }//데이터를 로드시키는 함수

}
