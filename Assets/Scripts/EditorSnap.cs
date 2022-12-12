using UnityEngine;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    [SerializeField] float snapX = 0.5f;
    [SerializeField] float snapY = 0.5f;
    [SerializeField] float snapZ = 0.5f;

    void Update()
    {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / snapX) * snapX;
        snapPos.y = Mathf.RoundToInt(transform.position.y / snapY) * snapY;
        snapPos.z = Mathf.RoundToInt(transform.position.z / snapZ) * snapZ;

        transform.position = new Vector3(snapPos.x, snapPos.y, snapPos.z);
    }
}
