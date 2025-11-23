using UnityEngine;

public class PlayerNoiseEmitter : MonoBehaviour
{
    public float walkNoise = 1f;
    public float runNoise = 3f;
    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (cc == null) return;
        Vector3 vel = new Vector3(cc.velocity.x, 0, cc.velocity.z);
        if (vel.magnitude > 0.1f)
        {
            float vol = Input.GetKey(KeyCode.LeftShift) ? runNoise : walkNoise;
            NoiseSystem.Instance.MakeNoise(transform.position, vol, 0.6f);
        }
    }
}
