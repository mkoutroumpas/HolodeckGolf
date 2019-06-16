using UnityEngine;

public class TerrainObject : MonoBehaviour
{
    private readonly static string _BallLiteral = "Ball";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == null)
            return;

        if (collision.gameObject.name.Contains(_BallLiteral) ||
            collision.gameObject.tag == _BallLiteral)
        {
            LaunchableObject launchableObject = collision.gameObject.GetComponentInChildren<LaunchableObject>();
            if (launchableObject != null)
            {
                launchableObject.ReceiveCollision(collision);
            }
        }
    }
}
