namespace Utils.EnumTypes
{
    // 이동 방향
    public enum Direction
    {
        Front,
        Back,
        Right,
        Left
    }

    // 플레이어 상태
    public enum PlayerState
    {
        Idle,  // 기본
        Walk   // 걷기
    }

    // 손님 상태
    public enum CustomerState
    {
        Idle,     // 기본
        Walk,     // 걷기
        Order,    // 주문
        Wait,     // 기다림
        Eat,      // 먹기
        Drink,    // 마시기
        ReJoice,  // 기뻐하기
        Angry,    // 화내기
        Truth     // 진상짓
    }

    // 직원 상태
    public enum StaffState
    {
        Idle,  // 기본
        Walk   // 걷기
    }

    // 아이템 타입
    public enum ItemType
    {
        Ingredient,  // 식재료
        Food         // 음식
    }
}