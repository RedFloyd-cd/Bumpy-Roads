using UnityEngine;
using TMPro;

public class DisplayDistanceText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private Transform _playerTrans;

    private Vector2 _startPosition;
    private float _traveledDistance = 0f;

    private void Start()
    {
        _startPosition = _playerTrans.position;
    }

    private void Update()
    {
        Vector2 distance = (Vector2)_playerTrans.position - _startPosition;
        distance.y = 0f;

        if (distance.x < 0)
        {
            distance.x = 0;
        }

        _traveledDistance = distance.x;
        _distanceText.text = _traveledDistance.ToString("F0") + "m";
    }

    public void SaveDistance()
    {
        PlayerPrefs.SetFloat("LastDistance", _traveledDistance);

        // Eğer yeni mesafe en yüksek mesafeden büyükse, kaydet
        if (_traveledDistance > PlayerPrefs.GetFloat("BestDistance", 0f))
        {
            PlayerPrefs.SetFloat("BestDistance", _traveledDistance);
        }

        PlayerPrefs.Save();
    }
}