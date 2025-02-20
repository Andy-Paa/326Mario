using UnityEngine;

public class cctrl : MonoBehaviour
{
    public float acceleration = 10f;  // 加速
    public float maxSpeed = 5f;       // 最大速度
    public float jumpImpulse = 8f;    // 跳跃力度
    public float jumpBoostForce = 3f; // 二段跳力度
    public int maxAirJumps = 1;       // 允许的空中跳跃次数

    private Rigidbody rb;
    public bool isGrounded;
    private int airJumps = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 获取玩家输入
        float horizontalAmount = Input.GetAxis("Horizontal");

        // 1. 处理跳跃 & 二段跳
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
                airJumps = 0; // 重置空中跳跃计数
            }
            else if (airJumps < maxAirJumps)
            {
                rb.AddForce(Vector3.up * jumpBoostForce, ForceMode.VelocityChange);
                airJumps++;
            }
        }

        // 2. 角色停止时，逐渐衰减速度
        if (Mathf.Approximately(horizontalAmount, 0f))
        {
            Vector3 decayedVelocity = rb.linearVelocity;
            decayedVelocity.x *= 1f - Time.deltaTime * 4f;
            rb.linearVelocity = decayedVelocity;
        }
        else
        {
            // 3. 角色旋转朝向移动方向
            float yawRotation = (horizontalAmount > 0f) ? 90f : -90f;
            Quaternion rotation = Quaternion.Euler(0f, yawRotation, 0f);
            transform.rotation = rotation;
        }

        // 4. 角色水平移动
        rb.linearVelocity += Vector3.right * horizontalAmount * Time.deltaTime * acceleration;

        // 限制水平速度
        float horizontalSpeed = rb.linearVelocity.x;
        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);

        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.x = horizontalSpeed;
        rb.linearVelocity = newVelocity;

        // 5. 地面检测（Raycast 从脚底检测）
        Collider c = GetComponent<Collider>();
        Vector3 startPoint = transform.position + Vector3.down * c.bounds.extents.y;
        float castDistance = 0.05f; // 适当偏移，避免浮点误差
        isGrounded = Physics.Raycast(startPoint, Vector3.down, castDistance);

        // 可视化 Raycast 结果（绿色=地面，红色=空中）
        Color color = isGrounded ? Color.green : Color.red;
        Debug.DrawLine(startPoint, startPoint + castDistance * Vector3.down, color, 0f, false);
    }
}
