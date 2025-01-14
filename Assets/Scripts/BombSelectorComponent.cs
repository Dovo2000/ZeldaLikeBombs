using UnityEngine;

public class BombSelectorComponent : MonoBehaviour
{
    public float pauseTimeScale = 0.1f;
    public int iconOffset = 125;
    public int currentIndex = 0;

    private int _maxIconCount;
    private RectTransform _transform;

    private void Awake()
    {
        _maxIconCount = transform.childCount;
        _transform = GetComponent<RectTransform>();
    }

    public void ActivateUI(bool needToActivate)
    {
        gameObject.SetActive(needToActivate);
        Time.timeScale = needToActivate ? pauseTimeScale : 1.0f;
    }

    public void ChangeSelection(int indexVariation) // Index variation should be -1 or 1 to add or substract to the currentIndex
    {
        currentIndex = Mathf.Clamp(currentIndex + indexVariation, 0, _maxIconCount - 1);
        Debug.Log(currentIndex);

        _transform.localPosition =  new Vector3(currentIndex * -iconOffset, _transform.localPosition.y, _transform.localPosition.z);
    }
}
