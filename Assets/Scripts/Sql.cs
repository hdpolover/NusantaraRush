using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;
using System;

public class Sql : MonoBehaviour
{
    public GameObject playerLevel;
    public GameObject playerName;
    public GameObject playerPoin;
    public GameObject playerPart;
    public GameObject playerAmmo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetUserData()
    {
        string conn = "URI=file:" + Application.dataPath + "/Plugins/db.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM player_stat";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string nama = reader.GetString(1);
            int poin = reader.GetInt32(2);
            int part = reader.GetInt32(3);
            int ammo = reader.GetInt32(4);

            // Debug.Log(id+", "+nama+", "+poin+", "+part+", "+ammo);
            playerName.GetComponent<TMPro.TextMeshProUGUI>().text = " " + nama;
            playerPoin.GetComponent<TMPro.TextMeshProUGUI>().text = " " + poin;
            playerPart.GetComponent<TMPro.TextMeshProUGUI>().text = "" + part;
            playerAmmo.GetComponent<TMPro.TextMeshProUGUI>().text = "" + ammo;
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void UpdateData(){
        string conn = "URI=file:"+Application.dataPath+"/Plugins/db.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();

        dbcmd.CommandText = "UPDATE player_stat SET nama = Nama, poin = 500, part = 500, ammo = 500 WHERE id = 1";
        dbcmd.ExecuteNonQuery();

        string sqlQuery = "SELECT * FROM player_stat";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while(reader.Read()){
            int id = reader.GetInt32(0);
            string nama = reader.GetString(1);
            int poin = reader.GetInt32(2);
            int part = reader.GetInt32(3);
            int ammo = reader.GetInt32(4);

            // Debug.Log(id+", "+nama+", "+poin+", "+part+", "+ammo);
            playerName.GetComponent<TMPro.TextMeshProUGUI>().text = " "+nama;
            playerPoin.GetComponent<TMPro.TextMeshProUGUI>().text = " "+poin;
            playerPart.GetComponent<TMPro.TextMeshProUGUI>().text = ""+part;
            playerAmmo.GetComponent<TMPro.TextMeshProUGUI>().text = ""+ammo;
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void SaveStrategyState()
    {
        string conn = "URI=file:" + Application.dataPath + "/Plugins/db.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();

        dbcmd.CommandText = "UPDATE player_stat SET is_mission_in_progress = 1 WHERE id = 1";
        dbcmd.ExecuteNonQuery();

        //add player position data
        // here

        dbcmd.Dispose();
        dbconn.Close();
    }

    public void DoneStrategyState()
    {

    }
}
