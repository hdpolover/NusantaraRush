using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data;
using Mono.Data.Sqlite;

public class PlayerResource : MonoBehaviour
{
    public GameObject playerPoin;
    public GameObject playerPart;
    public GameObject playerAmmo;

    // Start is called before the first frame update
    void Start()
    {
        GetPlayerStats();
    }

    public void GetPlayerStats()
    {
        //dbHandler.MakeSqliteDatabase();

        string check = Application.persistentDataPath + "/database.db";
        if (File.Exists(check))
        {
            string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
            IDbConnection myConnection = new SqliteConnection(path_sqlite);
            myConnection.Open();
            IDbCommand myCommand = myConnection.CreateCommand();
            string sqlQuery = "SELECT poin, part, ammo FROM player_stat";
            myCommand.CommandText = sqlQuery;
            IDataReader myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                int poin = myReader.GetInt32(0);
                int part = myReader.GetInt32(1);
                int ammo = myReader.GetInt32(2);

                playerPoin.GetComponent<TMPro.TextMeshProUGUI>().text = " " + poin;
                playerPart.GetComponent<TMPro.TextMeshProUGUI>().text = " " + part;
                playerAmmo.GetComponent<TMPro.TextMeshProUGUI>().text = " " + ammo;
            }
            myReader.Close();
            myCommand.Dispose();
            myConnection.Close();
        }
    }
}
