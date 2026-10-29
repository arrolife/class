using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Image LeftImage; //左側に立つキャラ
    public Image RightImage; //右側に立つキャラ

    
    private int LineNum = 1; //行番号
    // private int ColumnNum = 0; //列番号 //まだいらなさそう

    //main
    private int IdColumn = 0; //idの列番号 //どのcsvもidを一番左に書くので全部これ使う
    private int LeftColumn = 1; //leftの列番号
    private int RightColumn = 2; //rightの列番号
    private int TextColumn = 4; //textの列番号
    private int Choice1Column = 5; //choice1の列番号
    private int Choice2Column = 6; //choice2の列番号
    private int Choice3Column = 7; //choice3の列番号
    private int Jump1Column = 11; //jump1の列番号
    private int Jump2Column = 12; //jump2の列番号
    private int Jump3Column = 13; //jump3の列番号

    //character
    private int PassColumn = 1; //passの列番号

    private TextAsset MainCsvFile; //MainCSVファイル
    private List<string[]> MainCsvData = new List<string[]>(); //MainCSVファイルの中身を入れるリスト

    private TextAsset CharacterCsvFile; //CharacterCSVファイル
    private List<string[]> CharacterCsvData = new List<string[]>(); //MainCSVファイルの中身を入れるリスト

    void Start()
    {

        //選択肢ボタンを隠す
        ChoiceButtonHide();

        //maincsv
        MainCsvFile = Resources.Load("ChoiceTemplate") as TextAsset; //ResourcesにあるCSVファイルを格納
        StringReader mainreader = new StringReader(MainCsvFile.text); //TextAssetをStringReaderに変換
        while (mainreader.Peek() != -1)
        {
            string line = mainreader.ReadLine(); //1行ずつ読み込む
            MainCsvData.Add(line.Split(',')); //csvDataリストに追加する
        }

        //chractercsv
        CharacterCsvFile = Resources.Load("CharacterImageTemplate") as TextAsset; //ResourcesにあるCSVファイルを格納
        StringReader characterreader = new StringReader(CharacterCsvFile.text); //TextAssetをStringReaderに変換
        while (characterreader.Peek() != -1)
        {
            string line = characterreader.ReadLine(); //1行ずつ読み込む
            CharacterCsvData.Add(line.Split(',')); //csvDataリストに追加する
        }

        //maincsvにおける各Columnの位置をセット
        MainColumnNumSet();

        //charactercsvにおける各Columnの位置をセット
        CharacterColumnSet();

        //テキストに入力
        MainText.text = MainCsvData[LineNum][TextColumn] + "\nこのテキストのIDは" + MainCsvData[LineNum][IdColumn] + "です" ;

        //キャラクターimageをセット
        CharacterImageSet();



   
    }

    //character内の各columnの位置を探す
    private void CharacterColumnSet()
    {

        //""の中身が正しいか必ず確認
        for (int i = 0; i < CharacterCsvData[0].Length; i++)
        {

            if (CharacterCsvData[0][i] == "pass")
            {
                PassColumn = i;
            }
        }
            



    }



    //maincsv内の各columnの位置を探す
    private void MainColumnNumSet()
    {
        
        //""の中身が正しいか必ず確認
        for (int i = 0; i < MainCsvData[0].Length; i++){


            if (MainCsvData[0][i] == "id")
            {
                IdColumn = i;
            }
            else if (MainCsvData[0][i] == "left")
            {
                LeftColumn = i;
            }
            else if (MainCsvData[0][i] == "right")
            {
                RightColumn = i;
            }
            else if (MainCsvData[0][i] == "text")
            {
                TextColumn = i;
            }
            else if (MainCsvData[0][i] == "choice1")
            {
                Choice1Column = i;
            }
            else if (MainCsvData[0][i] == "choice2")
            {
                Choice2Column = i;
            }
            else if (MainCsvData[0][i] == "choice3")
            {
                Choice3Column = i;
            }
            else if (MainCsvData[0][i] == "jump1")
            {
                Jump1Column = i;
            }
            else if (MainCsvData[0][i] == "jump2")
            {
                Jump2Column = i;
            }
            else if (MainCsvData[0][i] == "jump3")
            {
                Jump3Column = i;
            }

        }



    }


    public void MainButtonClick() //次の会話に進む処理
    {

        if (MainCsvData[LineNum][Jump1Column] == "") //jump1にidが入力してあるか
        {
            LineNum++; //ない場合は次の行に進む
        }
        else
        {
            for (int i = 0; i < MainCsvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
            {

                if (MainCsvData[i][IdColumn] == MainCsvData[LineNum][Jump1Column])
                {

                    LineNum = i;
                    break;
                }

            }
        }

        CharacterImageSet();

        //選択肢がある会話について
        if (MainCsvData[LineNum][Choice1Column] != "") //次の行の選択肢の有無を確認
        {
            //選択肢があった場合
            MainButton.SetActive(false);
            Choice1Button.SetActive(true);
            Choice1Text.text = MainCsvData[LineNum][Choice1Column];

        }
        if (MainCsvData[LineNum][Choice2Column] != "")
        {

            Choice2Button.SetActive(true);
            Choice2Text.text = MainCsvData[LineNum][Choice2Column];

        }
        if (MainCsvData[LineNum][Choice3Column] != "")
        {

            Choice3Button.SetActive(true);
            Choice3Text.text = MainCsvData[LineNum][Choice3Column];

        }
        
        MainText.text = MainCsvData[LineNum][TextColumn] + "\nこのテキストのIDは" + MainCsvData[LineNum] [IdColumn] + "です";
        
    }


    //それぞれの選択に応じた処理を行う

    public void Choice1Click()
    {

        ChoiceButtonHide();

        //ジャンプ先のIDを検索してそこの会話に飛ぶ
        for (int i = 0; i < MainCsvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
        {

            if (MainCsvData[i][IdColumn] == MainCsvData[LineNum][Jump1Column])
            {
                
                LineNum = i;
                MainText.text = MainCsvData[LineNum][TextColumn] + "\nこのテキストのIDは" + MainCsvData[LineNum][IdColumn] + "です";
                break;
            }
            
        }

        CharacterImageSet();
        MainButton.SetActive(true);

    }

    public void Choice2Click()
    {

        ChoiceButtonHide();

        for (int i = 0; i < MainCsvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
        {

            if (MainCsvData[i][IdColumn] == MainCsvData[LineNum][Jump2Column])
            {

                LineNum = i;
                MainText.text = MainCsvData[LineNum][TextColumn] + "\nこのテキストのIDは" + MainCsvData[LineNum][IdColumn] + "です";
                break;
            }

        }

        CharacterImageSet();
        MainButton.SetActive(true);
    }

    public void Choice3Click()
    {

        ChoiceButtonHide();

        for (int i = 0; i < MainCsvData.Count; i++) //csvDataリストの条件を満たす値の数（全て)
        {

            if (MainCsvData[i][IdColumn] == MainCsvData[LineNum][Jump3Column])
            {

                LineNum = i;
                MainText.text = MainCsvData[LineNum][TextColumn] + "\nこのテキストのIDは" + MainCsvData[LineNum][IdColumn] + "です";
                break;
            }

        }

        CharacterImageSet();
        MainButton.SetActive(true);
    }

    private void ChoiceButtonHide()
    {

        Choice1Button.SetActive(false);
        Choice2Button.SetActive(false);
        Choice3Button.SetActive(false);
    }

    //キャラクターのimageをセットする
    private void CharacterImageSet()
    {

        //Leftの確認
        if (MainCsvData[LineNum][LeftColumn] != "")
        {
            for (int i = 0; i < CharacterCsvData.Count; i++)
            {

                if (CharacterCsvData[i][IdColumn] == MainCsvData[LineNum][LeftColumn])
                {

                    LeftImage.sprite = Resources.Load<Sprite>(CharacterCsvData[i][PassColumn]);

                }
            }
        }

        //Rightの確認
        if (MainCsvData[LineNum][RightColumn] != "")
        {
            for (int i = 0; i < CharacterCsvData.Count; i++)
            {

                if (CharacterCsvData[i][IdColumn] == MainCsvData[LineNum][RightColumn])
                {

                    RightImage.sprite = Resources.Load<Sprite>(CharacterCsvData[i][PassColumn]);

                }
            }
        }

        


    }


}