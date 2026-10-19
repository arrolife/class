using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;　//追加
using TMPro;



public class csvReader : MonoBehaviour
{
    public TMP_Text text;

    private int num = 0;

    private TextAsset csvFile; // CSVファイル
    private List<string[]> csvData = new List<string[]>(); // CSVファイルの中身を入れるリスト

    void Start()
    {
        csvFile = Resources.Load("MainTemplate") as TextAsset; // ResourcesにあるCSVファイルを格納
        StringReader reader = new StringReader(csvFile.text); // TextAssetをStringReaderに変換

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1行ずつ読み込む
            csvData.Add(line.Split(',')); // csvDataリストに追加する
        }

        for (int i = 0; i < csvData.Count; i++) // csvDataリストの条件を満たす値の数（全て）
        {
            // データの表示
            Debug.Log(csvData[i][4]);
        }

        text.text = csvData[1][4];

   
    }


    public void OnButtonClick()
    {
        num += 1;
        text.text = csvData[1 + num][4];

    }


}