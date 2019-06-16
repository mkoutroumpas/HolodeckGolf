using UnityEngine;

public class TerrainObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == null)
            return;

        if (collision.gameObject.name.Contains(Common.ObjectFilterLiteral) ||
            collision.gameObject.tag.Contains(Common.ObjectFilterLiteral))
        {
            LaunchableObject launchableObject = collision.gameObject.GetComponentInChildren<LaunchableObject>();
            if (launchableObject != null)
            {
                launchableObject.ReceiveCollision(collision);
            }
        }
    }
}
