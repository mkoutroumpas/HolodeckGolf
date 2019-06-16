using UnityEngine;

public class TerrainObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == null)
            return;

        if (c.gameObject.name.Contains(Common.ObjectFilterLiteral) ||
            c.gameObject.tag.Contains(Common.ObjectFilterLiteral))
        {
            LaunchableObject lO = c.gameObject.GetComponentInChildren<LaunchableObject>();
            if (lO != null)
            {
                lO.ReceiveCollision(c);
            }
        }
    }
}
