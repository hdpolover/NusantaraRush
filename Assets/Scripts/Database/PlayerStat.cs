using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using PlayerDataClass;
using System.IO;

public class PlayerStat : MonoBehaviour
{
    public GameObject playerLevel;
    public GameObject playerName;
    public GameObject playerPoin;
    public GameObject playerPart;
    public GameObject playerAmmo;

    public GameObject scripts;

    public GameObject setNameScreen;
    public Text Name;
    public Text menuLog;
    DatabaseHandler dbHandler;

    // Start is called before the first frame update
    void Start()
    {
        dbHandler = scripts.GetComponent<DatabaseHandler>();
        //StartCoroutine(GetPlayerStat());
        setNameScreen.SetActive(false);
        GetPlayerStats();
        CekSudahIsiNama();
    }

    public IEnumerator GetPlayerStat()
    {
        string url = dbHandler.GetConnection() + "/_nusantara_rush/get_player_stat.php";
        //string url = "http://192.168.43.58/_nusantara_rush/get_player_stat.php";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.isDone)
            {
                string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                PlayerStatData myData = JsonUtility.FromJson<PlayerStatData>(jsonResult);
                playerName.GetComponent<TMPro.TextMeshProUGUI>().text = " " + myData.nama;
                playerPoin.GetComponent<TMPro.TextMeshProUGUI>().text = " " + myData.poin;
                playerPart.GetComponent<TMPro.TextMeshProUGUI>().text = " " + myData.part;
                playerAmmo.GetComponent<TMPro.TextMeshProUGUI>().text = " " + myData.ammo;
            }
        }
    }

    void CekSudahIsiNama()
    {
        string check = Application.persistentDataPath + "/database.db";
        if (File.Exists(check))
        {
            string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
            IDbConnection myConnection = new SqliteConnection(path_sqlite);
            myConnection.Open();
            IDbCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT is_done_set_name FROM player_stat WHERE id = 1";
            IDataReader myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                if (myReader.GetInt32(0) == 0)
                {
                    setNameScreen.SetActive(true);
                }
            }
            myReader.Close();
            myCommand.Dispose();
            myConnection.Close();
        }
    }

    public void SetName()
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();
        myCommand.CommandText = "UPDATE player_stat SET is_done_set_name = 1, nama = '"+Name.text+"'";
        myCommand.ExecuteNonQuery();

        myCommand.Dispose();
        myConnection.Close();
        setNameScreen.SetActive(false);
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

            string sqlQuery = "SELECT nama, poin, part, ammo, chosen_ship_id, is_tutorial, mission_progress FROM player_stat";
            myCommand.CommandText = sqlQuery;
            IDataReader myReader = myCommand.ExecuteReader();

            int chosenShip = 0;

            while (myReader.Read())
            {
                string nama = myReader.GetString(0);
                int poin = myReader.GetInt32(1);
                int part = myReader.GetInt32(2);
                int ammo = myReader.GetInt32(3);

                //get playerstats to public
                PlayerManager.instance.playerName = nama;
                PlayerManager.instance.goldAmount = poin;
                PlayerManager.instance.partAmount = part;
                PlayerManager.instance.ammoAmount = ammo;
                chosenShip = myReader.GetInt32(4);

                if (myReader.GetInt32(5) == 1)
                {
                    PlayerManager.instance.isNew = true;
                }
                else
                {
                    PlayerManager.instance.isNew = false;
                }

                PlayerManager.instance.missionProgress = myReader.GetInt32(6);

                // Debug.Log(id+", "+nama+", "+poin+", "+part+", "+ammo);
                playerName.GetComponent<TMPro.TextMeshProUGUI>().text = " " + nama;
                playerPoin.GetComponent<TMPro.TextMeshProUGUI>().text = " " + poin;
                playerPart.GetComponent<TMPro.TextMeshProUGUI>().text = " " + part;
                playerAmmo.GetComponent<TMPro.TextMeshProUGUI>().text = " " + ammo;
            }
            myReader.Close();
            myCommand.Dispose();

            myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT rocket_equip, mg_equip, cannon_equip FROM player_ship WHERE id = "+chosenShip;
            myReader =  myCommand.ExecuteReader();

            while (myReader.Read())
            {
                PlayerManager.instance.rocket_level = myReader.GetInt32(0);
                PlayerManager.instance.mg_level = myReader.GetInt32(1);
                PlayerManager.instance.cannon_level = myReader.GetInt32(2);
            }

            PlayerManager.instance.chosen_ship = chosenShip;

            myReader.Close();
            myCommand.Dispose();
            myConnection.Close();

            menuLog.text = PlayerManager.instance.menuLog;
        }
    }
}
