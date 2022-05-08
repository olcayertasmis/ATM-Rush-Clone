using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class FileManager
{
    public enum SaveFile
    {
        FILE_ONE,
        FILE_TWO,
        FILE_THREE,
        FILE_FOUR,
        FILE_FIVE
    }
    static SaveFile currentFile = SaveFile.FILE_ONE;

    public static void SaveScore(int totalMoney)
    {
        PlayerPrefs.SetInt(currentFile.ToString() + "totalMoney", totalMoney);
        PlayerPrefs.Save();
    }

    public static int LoadScore()
    {
        int score = PlayerPrefs.GetInt(currentFile.ToString() + "totalMoney");     
        return score;
    }

    public static void ChangeFile(SaveFile file)
    {
        currentFile = file;
    }
}