using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;

public class SceneManaging : MonoBehaviour
{

    public void MoveScene(int sceneDestination){
        SceneManager.LoadScene(sceneDestination);
    }

    public void UnfreezeTime()
    {
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SavePlayerOnMission(int missionOrder)
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        myCommand.CommandText = "UPDATE player_stat SET on_mission = "+missionOrder+" WHERE id = 1";
        myCommand.ExecuteNonQuery();

        myCommand.Dispose();
        myConnection.Close();
    }
}
