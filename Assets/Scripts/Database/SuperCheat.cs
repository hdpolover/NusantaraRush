using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;

public class SuperCheat : MonoBehaviour
{
    public GameObject confirmWindow;
    public Text missionPassField;

    void Start()
    {
        confirmWindow.SetActive(false);
    }

    public void MaxResource()
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        myCommand.CommandText = "UPDATE player_stat SET poin = 999999, part = 999999, ammo = 999999 WHERE id = 1";
        myCommand.ExecuteNonQuery();

        confirmWindow.SetActive(true);

        myCommand.Dispose();
        myConnection.Close();
    }

    public void MissionPass()
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        myCommand.CommandText = "UPDATE player_stat SET mission_progress = "+missionPassField.text+" WHERE id = 1";
        myCommand.ExecuteNonQuery();

        confirmWindow.SetActive(true);

        myCommand.Dispose();
        myConnection.Close();
    }

    public void OpeneConfirmWindow()
    {
        confirmWindow.SetActive(true);
    }

    public void CloseConfirmWindow()
    {
        confirmWindow.SetActive(false);
    }
}
