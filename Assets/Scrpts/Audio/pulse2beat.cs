using System.Collections;
using UnityEngine;

public class pulse2beat : MonoBehaviour
{
    [SerializeField] bool _useTestBeat;
    [SerializeField] float _PulseSize = 1.15f;
    [SerializeField] float _returnSpeed = 5f;
    private Vector3 _startSize;


    private void Start()
    {
        _startSize = transform.localScale;
        if ( _useTestBeat)
        {
            StartCoroutine(TestBeat());
        }
    }

    // Update is called once per frame
    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * _returnSpeed); 
    }
    public void Pulse()
    {
        transform.localScale = _startSize * _PulseSize;
    } 

    IEnumerator TestBeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Pulse();
        }
    }
}
