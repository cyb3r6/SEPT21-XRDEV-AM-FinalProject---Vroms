using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Digger;

public class Projectile : MonoBehaviour
{
    public BrushType brush = BrushType.Sphere;
    public ActionType action = ActionType.Dig;
    [Range(0, 7)]
    public int textureIndex;
    [Range(0.5f, 10f)]
    public float size = 4f;
    [Range(0f, 1f)]
    public float opacity = 0.5f;
    public bool autoRemoveTrees = true;
    public bool autoRemoveDetails = true;
    public GameObject explosion;

    private DiggerMasterRuntime diggerMasterRuntime;

    void OnEnable()
    {
        diggerMasterRuntime = FindObjectOfType<DiggerMasterRuntime>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var terrain = collision.collider.GetComponent<Terrain>();
        if (terrain)
        {
            var hitPosition = collision.GetContact(0).point;

            diggerMasterRuntime.Modify(hitPosition, brush, action, textureIndex, opacity, size, autoRemoveDetails, autoRemoveTrees);
            explosion.SetActive(true);
            Destroy(this.gameObject, 0.5f);
        }
    }

}
