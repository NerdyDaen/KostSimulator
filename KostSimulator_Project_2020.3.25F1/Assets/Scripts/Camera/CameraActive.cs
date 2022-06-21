using UnityEngine;

public class CameraActive : MonoBehaviour
{
    public Transform Target; //target mana yg diikuti
    public Vector2 MinPos; //batasan map
    public Vector2 MaxPos;
    public float Speed; //kecepatan kamera dlm mengejar

    private void LateUpdate() //agar kamera diupdate setelah player
    {
        var targetpos = new Vector3(Target.position.x, Target.position.y, transform.position.z);
        targetpos.x = Mathf.Clamp(targetpos.x, MinPos.x, MaxPos.x); //clamp = mengembalikan nilai antara min dan max value
        targetpos.y = Mathf.Clamp(targetpos.y, MinPos.y, MaxPos.y);
        transform.position = Vector3.Lerp(transform.position, targetpos, Speed); //lerp = linear interpolasi
    }
}
