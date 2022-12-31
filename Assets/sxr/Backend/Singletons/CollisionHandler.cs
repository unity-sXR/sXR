using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    public bool ObjectsWithinDistance(GameObject obj1, GameObject obj2, float distance) {
        return Vector3.Distance(obj1.transform.position, obj2.transform.position) < distance; }

    public bool ObjectsCollidersTouching(GameObject obj1, GameObject obj2) {
        foreach (var obj in new[] {obj1, obj2})
            if (obj.GetComponents<Collider>().Length == 0 && obj.GetComponentsInChildren<Collider>().Length == 0)
                obj.AddComponent<MeshCollider>();
        
        var obj1_collider = obj1.GetComponents<Collider>().Length > 0
            ? obj1.GetComponents<Collider>()[0]
            : obj1.GetComponentsInChildren<Collider>()[0];
        var obj2_collider = obj2.GetComponents<Collider>().Length > 0
            ? obj2.GetComponents<Collider>()[0]
            : obj2.GetComponentsInChildren<Collider>()[0];

        sxr.DebugLog("Object1 closest point on bounds: "+obj1_collider.bounds.extents);
        sxr.DebugLog("Object2 closest point on bounds: "+obj1_collider.ClosestPointOnBounds(obj2_collider.transform.position));
        var closestDistance = Vector3.Distance(obj2_collider.ClosestPointOnBounds(obj1.transform.position),
            obj1.transform.position);
        if (obj1_collider.bounds.size.x >= closestDistance || obj1_collider.bounds.size.y >= closestDistance 
                                                           || obj1_collider.bounds.size.z >= closestDistance )
            return true;

        return false; }

    public static CollisionHandler Instance;
    private void Awake() {
        if (Instance == null) {Instance = this; DontDestroyOnLoad(gameObject.transform.root); }
        else Destroy(gameObject); }
}
