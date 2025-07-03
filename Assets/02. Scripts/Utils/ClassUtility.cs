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

    public class StaffDataList
    {
        public List<PlayerData> Staffs;
    }

    [System.Serializable]
    public class StaffData
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

    public class CustomerDataList
    {
        public List <CustomerData> Customers;
    }

    [System.Serializable]
    public class CustomerData
    {

    }

    public class RecipeDataList
    {
        public List<RecipeData> Recipes;
    }

    [System.Serializable]
    public class RecipeData
    {
        public int Index;
        public int RecipeID;
        public int RecipeQty;
        public int Ingre1ID;
        public int Ingre1Qty;
        public int Ingre2ID;
        public int Ingre2Qty;
        public int Ingre3ID;
        public int Ingre3Qty;
        public int Ingre4ID;
        public int Ingre4Qty;
        public string CookType;
        public int CookTime;
    }
}