using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.Networking;
using PlayerDataClass;

public class PlayerStat : MonoBehaviour
{
    public GameObject playerLevel;
    public GameObject playerName;
    public GameObject playerPoin;
    public GameObject playerPart;
    public GameObject playerAmmo;

    public GameObject scripts;
    DatabaseHandler dbHandler;

    // Start is called before the first frame update
    void Start()
    {
        dbHandler = scripts.GetComponent<DatabaseHandler>();
        //StartCoroutine(GetPlayerStat());
        GetPlayerStats();
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        public static T[] getJsonArray<T>(string json)
        {
            string newJson = "{ \"items\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.Items;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
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

    public void GetPlayerStats()
    {
        dbHandler.MakeSqliteDatabase();

        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();
        string sqlQuery = "SELECT * FROM player_stat";
        myCommand.CommandText = sqlQuery;
        IDataReader myReader = myCommand.ExecuteReader();
        while (myReader.Read())
        {
            int id = myReader.GetInt32(0);
            string nama = myReader.GetString(1);
            int poin = myReader.GetInt32(2);
            int part = myReader.GetInt32(3);
            int ammo = myReader.GetInt32(4);

            // Debug.Log(id+", "+nama+", "+poin+", "+part+", "+ammo);
            playerName.GetComponent<TMPro.TextMeshProUGUI>().text = " " + nama;
            playerPoin.GetComponent<TMPro.TextMeshProUGUI>().text = " " + poin;
            playerPart.GetComponent<TMPro.TextMeshProUGUI>().text = " " + part;
            playerAmmo.GetComponent<TMPro.TextMeshProUGUI>().text = " " + ammo;
        }
        myReader.Close();
        myCommand.Dispose();
        myConnection.Close();
    }
}
