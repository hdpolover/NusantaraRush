using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerDataClass
{
    [System.Serializable]
    public class PlayerStatData
    {
        public string id;
        public string nama;
        public string poin;
        public string part;
        public string ammo;
        public string is_tutorial;
        public string x_tutorial_progress_id;
        public string is_mission_in_progress;
        public string x_mission_info_id;
    }
}
