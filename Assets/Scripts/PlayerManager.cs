using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    
    public string playerName;

    [Header("Resources")]
    public int goldAmount;
    public int ammoAmount;
    public int partAmount;

    [Header("Stats")]
    public bool isNew; //nentuin player baru apa bukan
    public int missionProgress; //misi terakhir player
    public string playerShipNaame;

    [Header("Chosen Ship")]
    public int chosen_ship; //kapal yang dipilih di armada buat battle
    public int rocket_level; //level rocket
    public int mg_level; //level mg
    public int cannon_level; //level cannon

    [Header("In battle data")]
    public bool isBattle;
    public int enemyOnBattle;
    public int isBattleWin;// 0 = neutral, 1 = win, 2 = lose

    [Header("menu")]
    public string menuLog = "Belum ada rekam aktivitas";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
