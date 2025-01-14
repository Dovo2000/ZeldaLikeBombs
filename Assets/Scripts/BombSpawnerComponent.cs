using System.Collections.Generic;
using UnityEngine;

public class BombSpawnerComponent : MonoBehaviour
{
    public List<GameObject> _prefabs;

    [SerializeField] private float _throwingForce = 10f;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _dropTransform;

    private GameObject _objectSpawned;
    private bool _isReleased = false;

    private void Update()
    {
        if (!_isReleased && _objectSpawned != null)
        {
            _objectSpawned.transform.position = _spawnPosition.position;
            _objectSpawned.transform.rotation = _spawnPosition.rotation;
        }
    }


    public void WantToDoSecondaryAction(bool wantToDrop)
    {
        if (_objectSpawned == null)
        {
            return;
        }
        else
        {
            if (!_isReleased)
            {
                if (wantToDrop)
                {
                    DropObject();
                }
                else
                {
                    ReleaseObject();
                }
            }
            else
            {
                _objectSpawned.GetComponent<BombBehaviour>().Explode();
                _objectSpawned = null;
            }
        }
    }

    public void SpawnObject(int prefabIndex)
    {
        if (_objectSpawned != null)
        {
            Destroy(_objectSpawned);
        }
        _isReleased = false;
        _prefabs[prefabIndex].GetComponent<Rigidbody>().isKinematic = true;
        _objectSpawned = Instantiate(_prefabs[prefabIndex], _spawnPosition.position, _spawnPosition.rotation, null);
    }

    /*
     Spawn -> follow spawn position -> release (reparent to object Container, activate rb)
     */
    private void ReleaseObject()
    {
        if (_objectSpawned != null && !_isReleased)
        {
            _objectSpawned.GetComponent<Rigidbody>().isKinematic = false;

            // Arc throw
            _objectSpawned.GetComponent<Rigidbody>().AddForce(_spawnPosition.forward * _throwingForce, ForceMode.Impulse);
            _isReleased = true;
        }
    }

    private void DropObject()
    {
        if(_objectSpawned != null && !_isReleased)
        {
            _objectSpawned.transform.SetPositionAndRotation(_dropTransform.position, _dropTransform.rotation);
            _objectSpawned.GetComponent <Rigidbody>().isKinematic = false;

            _isReleased = true;
        }
    }
}
