using UnityEngine;

public class OverlapValidator : MonoBehaviour
{
    public static OverlapValidator Instance { get; private set; }

    [SerializeField] private float _checkRadius = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public bool CheckOverlapping(Vector3 position, GameObject selectedTower)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _checkRadius);

        foreach (var collider in colliders)
        {
            if (collider is SphereCollider && collider.gameObject.layer == LayerMask.NameToLayer("TargetDetection"))
            {
                var baseTower = collider.GetComponentInParent<AbstractBaseTower>();

                if (baseTower != null && baseTower.gameObject != selectedTower)
                {
                    Debug.Log($"Overlapping detected with tower: {baseTower.gameObject.name}");
                    return false;
                }
            }
        }
        return true;
    }
}