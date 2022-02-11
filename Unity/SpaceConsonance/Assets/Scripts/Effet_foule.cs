using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effet_foule : MonoBehaviour
{
    // Game Object
    private AudioSource _audioSource; //Auto
    [SerializeField] private GameObject _gameobject;

    [Space(5)]
    // Update timing 
    [SerializeField] private float _UpdateStep = 0.01f;
    private float currentUpdateTime = 0f;

    // Data Lenght 
    private float _clipLoudness;
    private float[] clipSampleData;

    [SerializeField] private int _sampleDataLenght = 220;

    [Header("--- Size ---")]
    // Size Multiplicateur 
    [SerializeField] private float _minSizeFactor = 2.5f;
    [SerializeField] private float _maxSizeFactor = 3.5f;
    private float _sizeFactor;

    [Space(5)]
    // Size Max/Min


    [SerializeField] private float _minsize = 0.9f;
    [SerializeField] private float _maxsize = 3f;
   

    void Start()
    {
        _audioSource = FindObjectOfType<AudioSource>();
        clipSampleData = new float[_sampleDataLenght];
        _sizeFactor = Random.Range(_minSizeFactor, _maxSizeFactor);
    }

    void Update()
    {
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= _UpdateStep)
        {
            currentUpdateTime = 0f;
            _audioSource.clip.GetData(clipSampleData, _audioSource.timeSamples);
            _clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                _clipLoudness += Mathf.Abs(sample);
            }
   
            _clipLoudness /= _sampleDataLenght;

            _clipLoudness *= _sizeFactor;
            _clipLoudness = Mathf.Clamp(_clipLoudness, _minsize, _maxsize);
            _gameobject.transform.localScale = new Vector3(_minsize, _clipLoudness, _minsize);

             _sizeFactor = Random.Range(_minSizeFactor, _maxSizeFactor);
        }
    }
}
