using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Mono.Data.Sqlite;
using System.Data;

public class Tutorial : MonoBehaviour
{
    public int[] tutorList;
    public GameObject[] tutorPanels;

    public Button[] buttons;

    int tutorialProgressId = 0;
    IDbConnection dbconn;
    IDbCommand dbcmd;

    //SQLITE Connection
    string conn;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tutorPanels.Length; i++)
        {
            tutorPanels[i].SetActive(false);
        }

        CheckTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckTutorial()
    {
        Connection();

        dbcmd.CommandText = "SELECT x_tutorial_progress_id FROM player_stat";
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            tutorialProgressId = reader.GetInt32(0);
        }

        reader.Close();
        dbcmd.Dispose();
        dbconn.Close();

        for (int i = 0; i < tutorList.Length; i++)
        {
            if (tutorList[i] == tutorialProgressId)
            {
                tutorPanels[i].SetActive(true);
                SetInteractableButtons(false);

                GameObject tutorText = tutorPanels[i].transform.GetChild(2).gameObject;
                Text text = tutorText.GetComponent<Text>();

                Connection();

                dbcmd.CommandText = "SELECT content FROM tutorial_progress WHERE tutorial_progress_id = "+tutorialProgressId;
                IDataReader reader2 = dbcmd.ExecuteReader();
                while (reader2.Read())
                {
                    text.text = reader2.GetString(0);
                }
                reader2.Close();
                dbcmd.Dispose();
                dbconn.Close();
            }
        }

    }

    void Connection()
    {
        conn = "URI=file:" + Application.dataPath + "/Plugins/db.db";
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        dbcmd = dbconn.CreateCommand();
    }

    public void NextTutorial()
    {
        tutorialProgressId++;

        Connection();

        dbcmd.CommandText = "UPDATE player_stat SET x_tutorial_progress_id = " + tutorialProgressId + " WHERE id = 1";
        dbcmd.ExecuteNonQuery();

        //previousTutorial.SetActive(false);

        dbcmd.Dispose();
        dbconn.Close();
    }

    void SetInteractableButtons(bool interactable)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = interactable;
        }
    }
}
