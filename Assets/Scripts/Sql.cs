using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;
using System;

public class Sql : MonoBehaviour
{
    void Start()
    {

    }

    public void SaveStrategyState(int node, int shipId)
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        //set mission in progress is true
        myCommand.CommandText = "UPDATE player_stat SET is_mission_in_progress = 1 WHERE id = 1";
        myCommand.ExecuteNonQuery();

        //if ship id is bigger than 50, then it's an enemy ship
        if (shipId > 50)
        {
            myCommand.CommandText = "SELECT id FROM mission_ships WHERE id = "+shipId;
            IDataReader reader = myCommand.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();
                myCommand.CommandText = "UPDATE mission_ships SET node = " + node + " WHERE ship = " + shipId;
                myCommand.ExecuteNonQuery();
            }
            else
            {
                reader.Close();
                myCommand.CommandText = "INSERT INTO mission_ships(id, node, ship, x_mission_info_id) VALUES(" + shipId + ", "+node+", " + shipId + ", 1)";
                myCommand.ExecuteNonQuery();
            }
        }
        else
        {
            if (node == 0)
            {
                myCommand.CommandText = "INSERT INTO mission_ships(id, node, ship, x_mission_info_id) VALUES(" + shipId + ", 0, " + shipId + ", 1)";
                myCommand.ExecuteNonQuery();
            }
            else
            {
                myCommand.CommandText = "UPDATE mission_ships SET node = " + node + " WHERE ship = " + shipId;
                myCommand.ExecuteNonQuery();
            }
        }

        myCommand.Dispose();
        myConnection.Close();
    }

    public void DoneStrategyState()
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        //set mission in progress is false
        myCommand.CommandText = "UPDATE player_stat SET is_mission_in_progress = 0 WHERE id = 1";
        myCommand.ExecuteNonQuery();

        //Delete all save ship position
        myCommand.CommandText = "DELETE FROM mission_ships WHERE x_mission_info_id = 1";
        myCommand.ExecuteNonQuery();

        myCommand.Dispose();
        myConnection.Close();
    }

    public void CheckStrategyState(GameObject confirmWindow)
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();
        myCommand.CommandText = "SELECT * FROM mission_ships";
        IDataReader reader = myCommand.ExecuteReader();

        if (reader.Read())
        {
            confirmWindow.SetActive(true);
        }

        reader.Close();
        myCommand.Dispose();
        myConnection.Close();
    }

    public void FreeLoot()
    {
        //if strategy win

    }
}
