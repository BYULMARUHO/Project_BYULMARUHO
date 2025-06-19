namespace Utils.EnumTypes
{
    public enum Direction
    {
        Front,
        Back,
        Right,
        Left
    }

    public enum PlayerState
    {
        Idle,  // ±âº»
        Walk   // °È±â
    }

    public enum CustomerState
    {
        Idle,     // ±âº»
        Walk,     // °È±â
        Wait,     // ÁÙ¼­±â
        Sit,      // ¾É±â
        Eat,      // ¸Ô±â
        Drink,    // ¸¶½Ã±â
        ReJoice,  // ±â»µÇÏ±â
        Angry,    // È­³»±â
        Truth     // Áø»óÁþ
    }

    public enum StaffState
    {
        Idle,  // ±âº»
        Walk   // °È±â
    }
}