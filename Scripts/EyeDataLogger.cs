using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ViveSR.anipal.Eye;

public class EyeDataLogger : MonoBehaviour
{
    private string filePath;
    private StreamWriter writer;
    private float sampleRate = 0.1f; // 100msごと
    private float lastSampleTime;

    void Start()
    {
        // CSVファイルの保存場所を指定
        filePath = Path.Combine(Application.persistentDataPath, "EyeDataLog.csv");

        // ファイル作成とヘッダーの書き込み
        writer = new StreamWriter(filePath, false);
        writer.WriteLine("Time,LeftPupilDiameter,RightPupilDiameter");
    }

    void Update()
    {
        if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING)
            return;


        // 瞳孔径の取得
        if (SRanipal_Eye.GetVerboseData(out var verboseData))
        {
            if (Time.time - lastSampleTime >= sampleRate)
            {
                lastSampleTime = Time.time;
                // データ取得とCSV書き込み
                var leftPupilDiameter = verboseData.left.pupil_diameter_mm;
                var rightPupilDiameter = verboseData.right.pupil_diameter_mm;
                var currentTime = lastSampleTime;
                // データをCSVに書き込む
                writer.WriteLine($"{currentTime},{leftPupilDiameter},{rightPupilDiameter}");
            }
            

            
            
        }
    }

    void OnApplicationQuit()
    {
        // ファイルを閉じる
        if (writer != null)
        {
            writer.Close();
        }
    }
}

