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

    public bool hasBoat1;

    public int chosen_ship; //kapal yang dipilih di armada buat battle
    public int rocket_level; //level rocket
    public int mg_level; //level mg
    public int cannon_level; //level cannon


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
