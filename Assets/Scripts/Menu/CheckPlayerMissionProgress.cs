using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;

public class CheckPlayerMissionProgress : MonoBehaviour
{
    public Button[] buttonChapter;
    public Button[] buttonMission;

    void Start()
    {
        CheckMissionProgress();
    }

    void CheckMissionProgress()
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        myCommand.CommandText = "SELECT mission_progress FROM player_stat WHERE id = 1";
        IDataReader reader = myCommand.ExecuteReader();

        int playerMissionProgress = 0;

        if(reader.Read())
        {
            playerMissionProgress = reader.GetInt32(0);
        }

        if (playerMissionProgress >= 1)
        {
            buttonChapter[0].interactable = true;
            if (playerMissionProgress >= 4)
            {
                buttonChapter[1].interactable = true;
                if (playerMissionProgress >= 7)
                {
                    buttonChapter[2].interactable = true;
                    if (playerMissionProgress >= 10)
                    {
                        buttonChapter[3].interactable = true;
                        if (playerMissionProgress >= 13)
                        {
                            buttonChapter[4].interactable = true;
                        }
                    }
                }
            }
        }

        for (int i = playerMissionProgress; i < buttonMission.Length; i++)
        {
            buttonMission[i].interactable = false;
        }

        reader.Close();
        myCommand.Dispose();
        myConnection.Close();
    }
}
