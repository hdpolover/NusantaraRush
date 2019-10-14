using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    public Connection connData;
    string path;
    string path_sqlite;

    IDbConnection myConnection;
    IDbCommand myCommand;

    private void Start()
    {
        path = Application.persistentDataPath + "/connection.json";
        //MakeJsonConnection();
        MakeSqliteDatabase();
    }

    void MakeJsonConnection()
    {
        if (!File.Exists(path))
        {
            connData = new Connection("192.168.43.58");
            string jsonDataString = JsonUtility.ToJson(connData, false);
            File.WriteAllText(path, jsonDataString);
        }
    }

    public void MakeSqliteDatabase()
    {
        string check = Application.persistentDataPath + "/database.db";
        if (!File.Exists(check))
        {
            //Create DB
            path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
            IDbConnection conn = new SqliteConnection(path_sqlite);
            conn.Open();

            IDbCommand dbcmd;

            //Create Structure
            dbcmd = conn.CreateCommand();
            string create_table_dialogs =
                "CREATE TABLE dialogs (" +
                "id  INTEGER NOT NULL," +
                "text  TEXT," +
                "x_tutorial_pogress_id  INTEGER," +
                "PRIMARY KEY(id ASC)," +
                "FOREIGN KEY(x_tutorial_pogress_id) REFERENCES tutorial_progress(tutorial_progress_id) ON DELETE RESTRICT ON UPDATE CASCADE); ";
            dbcmd.CommandText = create_table_dialogs;
            dbcmd.ExecuteReader();
            dbcmd.Dispose();

            dbcmd = conn.CreateCommand();
            string create_table_mission_info =
                "CREATE TABLE mission_info(" +
                "mission_info_id  INTEGER NOT NULL," +
                "node  INTEGER," +
                "ship  INTEGER," +
                "PRIMARY KEY(mission_info_id ASC));";
            dbcmd.CommandText = create_table_mission_info;
            dbcmd.ExecuteReader();
            dbcmd.Dispose();

            dbcmd = conn.CreateCommand();
            string create_table_mission_ships =
                "CREATE TABLE mission_ships(" +
                "id INTEGER NOT NULL," +
                "node INTEGER," +
                "ship INTEGER," +
                "x_mission_info_id INTEGER," +
                "PRIMARY KEY (id ASC)," +
                "CONSTRAINT fkey0 FOREIGN KEY (x_mission_info_id) REFERENCES mission_info (mission_info_id) ON DELETE RESTRICT ON UPDATE CASCADE," +
                "FOREIGN KEY (ship) REFERENCES player_ship (id) ON DELETE RESTRICT ON UPDATE CASCADE);";
            dbcmd.CommandText = create_table_mission_ships;
            dbcmd.ExecuteReader();
            dbcmd.Dispose();

            dbcmd = conn.CreateCommand();
            string create_table_player_ship =
                "CREATE TABLE player_ship(" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "ship_type INTEGER," +
                    "rocket_equip INTEGER," +
                    "mg_equip INTEGER," +
                    "cannon_equip INTEGER," +
                    "ability_equip INTEGER," +
                    "health INTEGER" +
                ");";
            dbcmd.CommandText = create_table_player_ship;
            dbcmd.ExecuteReader();
            dbcmd.Dispose();

            dbcmd = conn.CreateCommand();
            string create_table_player_stat =
                "CREATE TABLE player_stat(" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                "nama TEXT," +
                "poin INTEGER," +
                "part INTEGER," +
                "ammo INTEGER," +
                "is_tutorial INTEGER," +
                "x_tutorial_progress_id INTEGER," +
                "is_mission_in_progress INTEGER," +
                "x_mission_info_id INTEGER," +
                "CONSTRAINT fkey0 FOREIGN KEY (x_tutorial_progress_id) REFERENCES tutorial_progress (tutorial_progress_id) ON DELETE RESTRICT ON UPDATE CASCADE," +
                "FOREIGN KEY (x_mission_info_id) REFERENCES mission_info (mission_info_id) ON DELETE RESTRICT ON UPDATE CASCADE" +
                ");";
            dbcmd.CommandText = create_table_player_stat;
            dbcmd.ExecuteReader();
            dbcmd.Dispose();

            dbcmd = conn.CreateCommand();
            string create_table_tutorial_progress =
                "CREATE TABLE tutorial_progress(" +
                "tutorial_progress_id INTEGER NOT NULL," +
                "content TEXT," +
                "is_dialog INTEGER," +
                "is_dialog_done INTEGER," +
                "PRIMARY KEY (tutorial_progress_id ASC)" +
                ");";
            ;
            dbcmd.CommandText = create_table_tutorial_progress;
            dbcmd.ExecuteReader();
            dbcmd.Dispose();

            //Insert data
            dbcmd = conn.CreateCommand();
            string insert_data =
                "INSERT INTO main.mission_info VALUES(1, null, null);" +

                "INSERT INTO main.player_ship VALUES(1, 1, 0, 1, 0, 0, 100);" +
                "INSERT INTO main.player_ship VALUES(2, 2, 0, 0, 1, 0, 100);" +
                "INSERT INTO main.player_ship VALUES(3, 3, 0, 0, 1, 0, 100);" +
                "INSERT INTO main.player_ship VALUES(4, 4, 1, 0, 1, 0, 100);" +

                "INSERT INTO main.player_stat VALUES(1, 'MyName', 4006890, 387200, 238005, 1, 1, 1, 1);" +

                "INSERT INTO main.tutorial_progress VALUES(1, 'Misi.', 0, 0);" +
                "INSERT INTO main.tutorial_progress VALUES(2, 'Lanjut.', 0, 0);" +
                "INSERT INTO main.tutorial_progress VALUES(3, 'Pilih chapter.', 0, 0);" +
                "INSERT INTO main.tutorial_progress VALUES(4, 'Pilih misi.', 0, 0);" +
                "INSERT INTO main.tutorial_progress VALUES(5, 'Pilih kapal.', 0, 0);" +
                "INSERT INTO main.tutorial_progress VALUES(6, 'Kirim kapal.', 0, 0);" +
                "INSERT INTO main.tutorial_progress VALUES(7, 'Selesai giliran.', 0, 0);" +
                "INSERT INTO main.tutorial_progress VALUES(8, 'Pilih kapal.', 0, 0);" +
                "INSERT INTO main.tutorial_progress VALUES(9, 'Pilih tujuan.', 0, 0);" +
                "INSERT INTO main.tutorial_progress VALUES(10, 'Selesai giliran.', 0, 0);"
            ;

            dbcmd.CommandText = insert_data;
            dbcmd.ExecuteNonQuery();

            conn.Close();
        }
    }

    public string GetConnection()
    {
        path = Application.persistentDataPath + "/connection.json";
        string jsonString = File.ReadAllText(path);
        Connection connection = JsonUtility.FromJson<Connection>(jsonString);
        // ^ use this if the json just contain 1 data 
        return "http://"+connection.address;
    }

    public IDbCommand GetDatabaseConnection()
    {
        path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        return myConnection.CreateCommand();
    }

    public void CloseDatabaseConnection()
    {
        myConnection.Close();
    }
}

[System.Serializable]
public class Connection
{
    public string address;

    public Connection(string address)
    {
        this.address = address;
    }
}
