using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;

public class Toko : MonoBehaviour
{
    public Button[] tombolBeliKapal;

    // Start is called before the first frame update
    void Start()
    {
        CekKapalPlayer();
    }

    public void BeliKapal(int id)
    {
        int gold;
        int part;
        int ammo;

        if (id == 0)
        {
            gold = 3000; part = 1800; ammo = 1200;
            ProsesBeli(id, gold, part, ammo, "Main Boat 1", "INSERT INTO player_ship VALUES(1, 1, 0, 0, 1, 0, 100)");
            //tombolBeliKapal[0].interactable = false;
        }
        else if (id == 1)
        {
            gold = 5500; part = 2400; ammo = 1800;
            ProsesBeli(id, gold, part, ammo, "Main Boat 2", "INSERT INTO player_ship VALUES(2, 2, 1, 0, 0, 0, 100)");
            //tombolBeliKapal[1].interactable = false;
        }
        else if (id == 2)
        {
            gold = 8000; part = 2800; ammo = 2200;
            ProsesBeli(id, gold, part, ammo, "Warship 1", "INSERT INTO player_ship VALUES(3, 3, 0, 1, 1, 0, 100)");
            //tombolBeliKapal[2].interactable = false;
        }
        else if (id == 3)
        {
            gold = 9500; part = 3100; ammo = 2500;
            ProsesBeli(id, gold, part, ammo, "Warship 2", "INSERT INTO player_ship VALUES(4, 4, 1, 0, 1, 0, 100)");
            //tombolBeliKapal[3].interactable = false;
        }
    }

    void ProsesBeli(int id, int gold, int part, int ammo, string shipName, string insertQuery)
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        myCommand.CommandText = "SELECT poin, part, ammo FROM player_stat WHERE id = 1";
        IDataReader myReader = myCommand.ExecuteReader();
        if (myReader.Read())
        {
            if (gold > myReader.GetInt32(0))
            {
                Debug.Log("tidak cukup gold");
            }
            else if (part > myReader.GetInt32(1))
            {
                Debug.Log("tidak cukup part");
            }
            else if (ammo > myReader.GetInt32(2))
            {
                Debug.Log("tidak cukup ammo");
            }
            else
            {
                int uGold = myReader.GetInt32(0) - gold;
                int uPart = myReader.GetInt32(1) - part;
                int uAmmo = myReader.GetInt32(2) - ammo;

                myReader.Close();
                myCommand.Dispose();

                //Update the player resource
                myCommand = myConnection.CreateCommand();
                myCommand.CommandText = "UPDATE player_stat SET poin = "+uGold+", part = "+uPart + ", ammo = " +uAmmo;
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

                //Insert new ship
                myCommand = myConnection.CreateCommand();
                myCommand.CommandText = insertQuery;
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();

                PlayerManager.instance.menuLog = "Berhasil membeli kapal dengan tipe : "+shipName+", kapal telah datang di armada.";
                tombolBeliKapal[id].interactable = false;
            }
        }
        myCommand.Dispose();
        myConnection.Close();
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
                tombolBeliKapal[reader.GetInt32(0)-1].interactable = false;
            }
        }
        reader.Close();
        myCommand.Dispose();
        myConnection.Close();
    }
}
