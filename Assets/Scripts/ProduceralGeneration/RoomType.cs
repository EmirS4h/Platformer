using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int roomType;

    public void DestroyRoom()
    {
        Destroy(gameObject);
    }
}
