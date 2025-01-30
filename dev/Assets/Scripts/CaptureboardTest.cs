using UnityEngine;

public class CaptureboardTest : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        string microphone = null;

        // デバイス名に"RICOH THETA Z1"を含むものを探す
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Detected device: " + device);
            if (device.Contains("RICOH THETA Z1"))
            {
                microphone = device;
                break;
            }
        }

        if (microphone != null)
        {
            Debug.Log("Using microphone: " + microphone);

            // マイク録音を開始
            audioSource.clip = Microphone.Start(microphone, true, 10, 44100);
            audioSource.loop = true;

            // マイク準備完了を待つ（タイムアウト付き）
            float waitTime = 0f;
            float timeout = 5f; // 最大5秒待つ
            while (!(Microphone.GetPosition(microphone) > 0))
            {
                waitTime += Time.deltaTime;
                if (waitTime > timeout)
                {
                    Debug.LogError("Microphone initialization timed out.");
                    return; // 初期化失敗の場合処理を中断
                }
            }

            Debug.Log("Microphone initialized and recording started.");
            audioSource.Play();
        }
        else
        {
            Debug.LogError("RICOH THETA Z1 microphone not found.");
        }
    }
}
