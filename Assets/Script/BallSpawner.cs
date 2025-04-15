using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;

    [SerializeField] private int _numberOfCOl = 3,_numberOfRow=3,_numberOfLayer=3;
    
    [SerializeField] private float _ballRadius = 0.06f;

    private void Start()
    {
        SpawnBall();
    }

    private void SpawnBall()
    {
        for(int i = 0; i < _numberOfLayer; i++)
        {
            for (int j = 0; j < _numberOfCOl; j++)
            {
                for (int k = 0; k < _numberOfRow; k++)
                {
                    var position = new Vector3( transform.position.x+k*_ballRadius,transform.position.y+i*_ballRadius,transform.position.z+j*_ballRadius);
                    Instantiate(_ballPrefab, position , Quaternion.identity, transform);

                }
            }
            
        }
    }
}
