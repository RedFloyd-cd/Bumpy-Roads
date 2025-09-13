using System;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField] private SpriteShapeController _spriteShapeController;

    [SerializeField, Range(3f, 300f)] private int _levelLength = 50;
    [SerializeField, Range(1f, 50f)] private float _xMultiplier = 2f;
    [SerializeField, Range(1f, 50f)] private float _yMultiplier = 2f;
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;
    [SerializeField] private float _noiseStep = 0.5f;
    [SerializeField] private float _bottom = 10f;

    private void OnValidate()
    {
        if (_spriteShapeController == null)
        {
            Debug.LogError("SpriteShapeController is not assigned.");
            return;
        }

        int existingPointCount = _spriteShapeController.spline.GetPointCount();

        // Eğer zaten spline mevcut ve yeterli sayıda nokta varsa, yeniden oluşturma
        if (existingPointCount >= _levelLength + 2)
        {
            return;
        }

        GenerateTerrain(existingPointCount);
    }

    private void GenerateTerrain(int startIndex = 0)
    {
        for (int i = startIndex; i < _levelLength; i++)
        {
            Vector3 pos = transform.position + new Vector3(i * _xMultiplier, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultiplier);
            if (i < _spriteShapeController.spline.GetPointCount())
            {
                _spriteShapeController.spline.SetPosition(i, pos);
            }
            else
            {
                _spriteShapeController.spline.InsertPointAt(i, pos);
            }

            if (i != 0 && i != _levelLength - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                Vector3 leftTangent = Vector3.left * _xMultiplier * _curveSmoothness;
                Vector3 rightTangent = Vector3.right * _xMultiplier * _curveSmoothness;
                _spriteShapeController.spline.SetLeftTangent(i, leftTangent);
                _spriteShapeController.spline.SetRightTangent(i, rightTangent);
            }
        }

        // Kapatma noktalarını ekle (manuel düzenlemeleri silmez)
        Vector3 lastPos = _spriteShapeController.spline.GetPosition(_levelLength - 1);
        Vector3 bottomLeft = new Vector3(transform.position.x, transform.position.y - _bottom);
        Vector3 bottomRight = new Vector3(lastPos.x, transform.position.y - _bottom);

        if (_spriteShapeController.spline.GetPointCount() > _levelLength)
        {
            _spriteShapeController.spline.SetPosition(_levelLength, bottomRight);
        }
        else
        {
            _spriteShapeController.spline.InsertPointAt(_levelLength, bottomRight);
        }

        if (_spriteShapeController.spline.GetPointCount() > _levelLength + 1)
        {
            _spriteShapeController.spline.SetPosition(_levelLength + 1, bottomLeft);
        }
        else
        {
            _spriteShapeController.spline.InsertPointAt(_levelLength + 1, bottomLeft);
        }
    }
}
