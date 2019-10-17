using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class CheckShipMenu : MonoBehaviour
{
    public GameObject[] ships;
    // Start is called before the first frame update
    void Start()
    {
        CekKapalTerpilih();
    }

    void CekKapalTerpilih()
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        myCommand.CommandText = "SELECT chosen_ship_id FROM player_stat WHERE id = 1";
        IDataReader myReader = myCommand.ExecuteReader();

        while (myReader.Read())
        {
            for (int i = 0; i < ships.Length; i++)
            {
                if (i == myReader.GetInt32(0))
                {
                    ships[i].SetActive(true);
                }
                else
                {
                    ships[i].SetActive(false);
                }
            }
        }

        myReader.Dispose();
        myCommand.Dispose();
        myConnection.Close();
    }
}
