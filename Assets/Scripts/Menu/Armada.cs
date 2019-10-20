using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.SceneManagement;

public class Armada : MonoBehaviour
{
    public Button[] tombolKapalArmada;

    // Start is called before the first frame update
    void Start()
    {
        tombolKapalArmada[0].interactable = false;
        tombolKapalArmada[1].interactable = false;
        tombolKapalArmada[2].interactable = false;
        tombolKapalArmada[3].interactable = false;

        CekKapalPlayer();   
    }

    public void CekKapalPlayer()
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        myCommand.CommandText = "SELECT id FROM player_ship";
        IDataReader reader = myCommand.ExecuteReader();

        while (reader.Read())
        {
            if (reader.GetInt32(0) != 0)
            {
                tombolKapalArmada[reader.GetInt32(0)-1].interactable = true;
            }
        }
        reader.Close();
        myCommand.Dispose();
        myConnection.Close();
    }

    public void Pilihkapal(int id)
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        myCommand.CommandText = "UPDATE player_stat SET chosen_ship_id = "+id;
        myCommand.ExecuteNonQuery();
        myCommand.Dispose();
        myConnection.Close();

        SceneManager.LoadScene(3);
    }
}
