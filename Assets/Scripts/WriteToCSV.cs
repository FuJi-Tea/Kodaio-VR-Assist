using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// ChatGPT-4 2023/4/9
[System.Serializable]
public class WriteToCSV : MonoBehaviour
{
    private List<int[]> dataList = new List<int[]>(); // スコアデータを格納するリスト
    private string filePath = "Assets/Score.csv"; // CSVファイルのパス
    private int[] currentData; // 現在編集中のデータ

    private int lineNow = 0;

    // スコアデータを追加する
    public void AddScoreData()
    {
        dataList.Add(currentData);
        currentData = new int[currentData.Length]; // 新しい配列を生成して、currentDataを初期化する
    }

    // CSVファイルにデータを書き込む
    public void WriteDataToCSV()
    {
        // CSVファイルに書き込む
        StreamWriter writer = new StreamWriter(filePath, true); // trueを指定することで追記モードで開く

        for (int i = lineNow; i < dataList.Count; i++)
        {
            string line = string.Join(",", dataList[i]); // 配列をカンマで区切った文字列に変換する
            writer.WriteLine(line);
            lineNow++;
        }
        writer.Flush();
        writer.Close();
    }

    // 配列を指定の長さで初期化する
    public void InitializeData(int length)
    {
        currentData = new int[length];
    }

    // 指定のインデックスにデータを設定する
    public void SetDataAt(int index, int data)
    {
        currentData[index] = data;
    }
}