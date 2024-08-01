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
            if (collider.GetComponent<AbstractTower>() != null && collider.gameObject != selectedTower)
            {
                Debug.Log($"Overlapping detected with tower: {collider.gameObject.name}");
                return false;
            }
        }

        return true;
    }
}