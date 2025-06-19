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
        Idle,  // �⺻
        Walk   // �ȱ�
    }

    public enum CustomerState
    {
        Idle,     // �⺻
        Walk,     // �ȱ�
        Wait,     // �ټ���
        Sit,      // �ɱ�
        Eat,      // �Ա�
        Drink,    // ���ñ�
        ReJoice,  // �⻵�ϱ�
        Angry,    // ȭ����
        Truth     // ������
    }

    public enum StaffState
    {
        Idle,  // �⺻
        Walk   // �ȱ�
    }
}