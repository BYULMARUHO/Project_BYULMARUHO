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

    // 손님 상태
    public enum CustomerState
    {
        Idle,     // 기본
        Walk,     // 걷기
        WaitOrder,// 주문 대시
        Order,    // 주문 함
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
        Equipment,   // 도구
        Ingredient,  // 식재료
        Food         // 음식
    }

    // 기구 타입
    public enum MachineType
    {
        BeverageMachine,  // 음료 기계
        GasStove,         // 가스 버너
        FoodStand,        // 음식 거치대
        Sink              // 싱크대
    }
}