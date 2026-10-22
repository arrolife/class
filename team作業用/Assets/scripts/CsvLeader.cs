using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;



public class csvReader : MonoBehaviour
{
    public TMP_Text MainText; //メインテキスト
    public GameObject MainButton; //メインボタン
    public GameObject Choice1Button; //選択肢１ボタン
    public GameObject Choice2Button; //選択肢２ボタン
    public GameObject Choice3Button; //選択肢３ボタン
    public TMP_Text Choice1Text; //選択肢１テキスト
    public TMP_Text Choice2Text; //選択肢２テキスト
    public TMP_Text Choice3Text; //選択肢３テキスト

    
    private int LineNum = 0; //行番号
    private int ColumnNum = 0; //列番号

    //全体の列の数に増減があった場合、変更可能性あり
    private int IdColumn = 0; //会話idの行番号
    private int TextColumn = 4; //textの行番号
    private int Choice1Column = 5; //choice1の列番号
    private int Choice2Column = 6; //choice2の列番号
    private int Choice3Column = 7; //choice3の列番号
    private int Jump1Column = 11; //jump1の列番号
    private int Jump2Column = 12; //jump2の列番号
    private int Jump3Column = 13; //jump3の列番号

    private int MaxChoiceNum = 3; //選択肢の最大の数
    private TextAsset csvFile; //CSVファイル
    private List<string[]> csvData = new List<string[]>(); //CSVファイルの中身を入れるリスト

    void Start()
    {
        ChoiceButtonHide();

        csvFile = Resources.Load("ChoiceTemplate") as TextAsset; //ResourcesにあるCSVファイルを格納
        StringReader reader = new StringReader(csvFile.text); //TextAssetをStringReaderに変換

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); //1行ずつ読み込む
            csvData.Add(line.Split(',')); //csvDataリストに追加する
        }

        for (int i = 0; i < csvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
        {
            //データの表示
            Debug.Log(csvData[i][TextColumn]); //仮
        }

        LineNum = 1; //実験用
        MainText.text = csvData[LineNum][TextColumn] + "\nこのテキストのIDは" + csvData[LineNum][IdColumn] + "です" ;

   
    }


    public void MainButtonClick() //次の会話に進む処理
    {

        if (csvData[LineNum][Jump1Column] == "") //jump1にidが入力してあるか
        {
            LineNum++; //ない場合は次の行に進む
        }
        else
        {
            for (int i = 0; i < csvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
            {

                if (csvData[i][IdColumn] == csvData[LineNum][Jump1Column])
                {

                    LineNum = i;
                    break;
                }

            }
        }


        //選択肢がある会話について
        if (csvData[LineNum][Choice1Column] != "") //次の行の選択肢の有無を確認
        {
            //選択肢があった場合
            MainButton.SetActive(false);
            Choice1Button.SetActive(true);
            Choice1Text.text = csvData[LineNum][Choice1Column];

        }
        if (csvData[LineNum][Choice2Column] != "")
        {

            Choice2Button.SetActive(true);
            Choice2Text.text = csvData[LineNum][Choice2Column];

        }
        if (csvData[LineNum][Choice3Column] != "")
        {

            Choice3Button.SetActive(true);
            Choice3Text.text = csvData[LineNum][Choice3Column];

        }
        
        MainText.text = csvData[LineNum][TextColumn] + "\nこのテキストのIDは" + csvData[LineNum] [IdColumn] + "です";
        
    }


    //それぞれの選択に応じた処理を行う

    public void Choice1Click()
    {

        ChoiceButtonHide();

        for (int i = 0; i < csvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
        {

            if (csvData[i][IdColumn] == csvData[LineNum][Jump1Column])
            {
                
                LineNum = i;
                MainText.text = csvData[LineNum][TextColumn] + "\nこのテキストのIDは" + csvData[LineNum][IdColumn] + "です";
                break;
            }
            
        }

        MainButton.SetActive(true);

    }

    public void Choice2Click()
    {

        ChoiceButtonHide();

        for (int i = 0; i < csvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
        {

            if (csvData[i][IdColumn] == csvData[LineNum][Jump2Column])
            {

                LineNum = i;
                MainText.text = csvData[LineNum][TextColumn] + "\nこのテキストのIDは" + csvData[LineNum][IdColumn] + "です";
                break;
            }

        }

        MainButton.SetActive(true);
    }

    public void Choice3Click()
    {

        ChoiceButtonHide();

        for (int i = 0; i < csvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
        {

            if (csvData[i][IdColumn] == csvData[LineNum][Jump3Column])
            {

                LineNum = i;
                MainText.text = csvData[LineNum][TextColumn] + "\nこのテキストのIDは" + csvData[LineNum][IdColumn] + "です";
                break;
            }

        }

        MainButton.SetActive(true);
    }

    private void ChoiceButtonHide()
    {

        Choice1Button.SetActive(false);
        Choice2Button.SetActive(false);
        Choice3Button.SetActive(false);
    }


}