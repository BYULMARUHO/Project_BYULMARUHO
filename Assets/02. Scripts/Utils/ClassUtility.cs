using System.Collections.Generic;

namespace Utils.ClassUtility
{
    public class PlayerDataList
    {
        public List<PlayerData> Players;
    }

    [System.Serializable]
    public class PlayerData
    {
        public int index;
        public string name;
        public string gender;
        public float moveSpeed;
        public float cookSpeed;
        public float cookSkill;
        public float wipingSpeed;
        public float belonging;
        public float service;
    }
}