using UnityEngine;

public class PongObject : MonoBehaviour
{
    private UnityEngine.Vector2 position;
    private float speed;

    public UnityEngine.Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public virtual void UpdatePosition()
    {
        // Basismethode zur Positionsaktualisierung
        transform.position = position;
    }
}
