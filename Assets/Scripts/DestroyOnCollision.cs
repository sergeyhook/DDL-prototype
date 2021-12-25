using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{

private void OnCollisionEnter(Collision self)
    {
        if (self.collider.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
