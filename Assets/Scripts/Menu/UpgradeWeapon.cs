using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;

public class UpgradeWeapon : MonoBehaviour
{
    public Button[] tombolKapalPlayer;
    public Button[] senjata;

    public GameObject upgradeProcessPanel;
    public Text gold;
    public Text part;
    public Text ammo;
    public GameObject confirmWindow;
    public Text confirmText;

    int chosenShip;
    int chosenWeapon;

    int goldMultiplier;
    int partMultiplier;
    int ammoMultiplier;

    int level = 0;

    void Start()
    {
        upgradeProcessPanel.SetActive(false);
        confirmWindow.SetActive(false);
        DisableUpgrade();
        CekKapalPlayer();
    }

    void DisableUpgrade()
    {
        for (int i = 0; i < senjata.Length; i++)
        {
            senjata[i].interactable = false;
        }

        for (int i = 0; i < tombolKapalPlayer.Length; i++)
        {
            tombolKapalPlayer[i].interactable = false;
        }
    }

    public void CheckUpgrade(int id)
    {
        if (id == 0)
        {
            chosenShip = id;
            senjata[0].interactable = false;
            senjata[1].interactable = true;
            senjata[2].interactable = false;
        }
        else if (id == 1)
        {
            chosenShip = id;
            senjata[0].interactable = false;
            senjata[1].interactable = true;
            senjata[2].interactable = false;
        }
        else if (id == 2)
        {
            chosenShip = id;
            senjata[0].interactable = false;
            senjata[1].interactable = false;
            senjata[2].interactable = true;
        }
        else if (id == 3)
        {
            chosenShip = id;
            senjata[0].interactable = true;
            senjata[1].interactable = false;
            senjata[2].interactable = true;
        }
        else if (id == 4)
        {
            chosenShip = id;
            senjata[0].interactable = true;
            senjata[1].interactable = false;
            senjata[2].interactable = true;
        }
    }

    public void UpgradeSenjata(int tipeSenjata)
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        string commandString = "";

        chosenWeapon = tipeSenjata;
        if (tipeSenjata == 0)
        {
            commandString = "SELECT rocket_equip FROM player_ship WHERE id = "+chosenShip;
        }
        else if (tipeSenjata == 1)
        {
            commandString = "SELECT mg_equip FROM player_ship WHERE id = "+chosenShip;
        }
        else if (tipeSenjata == 2)
        {
            commandString = "SELECT cannon_equip FROM player_ship WHERE id = " + chosenShip;
        }

        myCommand.CommandText = commandString;
        IDataReader reader = myCommand.ExecuteReader();

        if (reader.Read())
        {
            level = reader.GetInt32(0);
        }
        reader.Close();
        myCommand.Dispose();

        goldMultiplier = 6650 * level;
        partMultiplier = 4550 * level;
        ammoMultiplier = 5600 * level;

        gold.text = "" + goldMultiplier;
        part.text = "" + partMultiplier;
        ammo.text = "" + ammoMultiplier;

        upgradeProcessPanel.SetActive(true);

        myCommand.Dispose();
        myConnection.Close();
    }

    public void ProsesUpgrade()
    {
        string path_sqlite = "URI=file:" + Application.persistentDataPath + "/database.db";
        IDbConnection myConnection = new SqliteConnection(path_sqlite);
        myConnection.Open();
        IDbCommand myCommand = myConnection.CreateCommand();

        string sqlQuery = "SELECT poin, part, ammo FROM player_stat";
        myCommand.CommandText = sqlQuery;
        IDataReader myReader = myCommand.ExecuteReader();

        int currentGold = 0;
        int currentPart = 0;
        int currentAmmo = 0;

        while (myReader.Read())
        {
            currentGold = myReader.GetInt32(0);
            currentPart = myReader.GetInt32(1);
            currentAmmo = myReader.GetInt32(2);
        }
        myReader.Dispose();

        if (currentGold < goldMultiplier || currentPart < partMultiplier || currentAmmo < ammoMultiplier)
        {
            confirmText.text = "bahan tidak mencukupi";
            confirmWindow.SetActive(true);
        }
        else
        {
            string commandString = "";

            if (chosenWeapon == 0)
            {
                commandString = "UPDATE player_ship SET rocket_equip = " + (level + 1) + " WHERE id = " + chosenShip;
            }
            else if (chosenWeapon == 1)
            {
                commandString = "UPDATE player_ship SET mg_equip = " + (level + 1) + " WHERE id = " + chosenShip;
            }
            else if (chosenWeapon == 2)
            {
                commandString = "UPDATE player_ship SET cannon_equip = " + (level + 1) + " WHERE id = " + chosenShip;
            }
            myCommand.CommandText = commandString;
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();

            myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UPDATE player_stat SET poin = " + goldMultiplier + ", part = " + partMultiplier + ", ammo = " + ammoMultiplier;
            myCommand.ExecuteNonQuery();

            //berhasil upgrade screen
            confirmText.text = "berhasil upgrade";
            confirmWindow.SetActive(true);
        }

        myCommand.Dispose();
        myConnection.Close();
    }

    public void CloseWindow()
    {
        confirmWindow.SetActive(false);
        upgradeProcessPanel.SetActive(false);
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
                tombolKapalPlayer[reader.GetInt32(0) - 1].interactable = true;
            }
        }
        reader.Close();
        myCommand.Dispose();
        myConnection.Close();
    }
}
