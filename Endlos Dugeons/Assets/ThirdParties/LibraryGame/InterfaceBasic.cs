using UnityEngine;

interface ISwitchable
{
    void TurnOn();
    void TurnOff();
}

public interface IDamageable
{
    void TakeDamage(float amount);
}

// Enum định nghĩa các trạng thái khả dụng
public enum ObjectState
{
    Normal,
    Dead
}

// Lớp abstract Object
public abstract class BaseGameObject : MonoBehaviour, IDamageable
{
    // Thuộc tính hp
    protected float m_HP;

    // Thuộc tính state sử dụng enum
    protected ObjectState m_State;

    // Phương thức abstract để xử lý khi object bị tấn công
    public abstract void TakeDamage(float damage);

    // Phương thức abstract để xử lý khi object thay đổi trạng thái
    public abstract void ChangeState(ObjectState newState);

    // Phương thức abstract để xử lý khi object chết
    public abstract void Die();
}

public class EnemyBase : BaseGameObject
{
    // Triển khai phương thức TakeDamage()
    public override void TakeDamage(float damage)
    {
        if (m_State != ObjectState.Dead)
        {
            m_HP -= damage;

            if (m_HP <= 0 && m_State != ObjectState.Dead)
            {
                ChangeState(ObjectState.Dead);
            }
            // Logic xử lý khi enemy bị tấn công
        }
    }

    // Triển khai phương thức ChangeState()
    public override void ChangeState(ObjectState newState)
    {
        m_State = newState;

        // Logic khi trạng thái của enemy thay đổi
    }

    // Triển khai phương thức Die()
    public override void Die()
    {
        ChangeState(ObjectState.Dead);
        // Logic khi enemy chết
        Destroy(gameObject);
    }
}