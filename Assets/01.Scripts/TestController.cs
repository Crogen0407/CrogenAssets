using UnityEngine;

namespace _01.Scripts
{
    public class TestController : MonoBehaviour
    {
        [SerializeField] private GameObject obj;
        private void Update()
        {
            if (Input.anyKeyDown)
            {
                Instantiate(obj);
                // this.Pop(PoolType.pf_Cube, new Vector3(
                //     Random.Range(-10, 10),
                //     Random.Range(-10, 10),
                //     Random.Range(-10, 10)), Quaternion.identity);
            }
        }
    }
}