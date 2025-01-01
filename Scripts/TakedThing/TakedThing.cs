using UnityEngine;

public abstract class TakedThing : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
