using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JobRealisation.TadPoles
{
    public class TadPoleJob : MonoBehaviour
    {
        NativeArray<Vector3> dataPosIn;
        NativeArray<Vector3> dataPosOut;
        /// <summary>
        /// Максимальная скорость
        /// </summary>
        [SerializeField] private float maxSpeed = 3.0f;
        [SerializeField] private float minSpeed = 0.5f;

        /// <summary>
        /// Скорость
        /// </summary>
        private float speed;

        /// <summary>
        /// Прямая ссылка на свой Transform
        /// </summary>
        private Transform cashed;

        private void Awake()
        {
            speed = Random.Range(minSpeed, maxSpeed);
            cashed = transform;
            cashed.rotation = Quaternion.Euler(new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f).normalized * 180.0f);
        }

        private void FixedUpdate()
        {
            dataPosIn[0] = cashed.position;
            dataPosIn[1] = cashed.forward * speed * Time.fixedDeltaTime;

            MovingJob moving = new MovingJob() { inputData = dataPosIn, outputData = dataPosOut };
            moving.Schedule().Complete();
            cashed.position = dataPosOut[0];                        
        }

        private void OnTriggerEnter(Collider other)
        {
            cashed.LookAt(cashed.position - cashed.forward);
        }

        private void OnEnable()
        {
           dataPosIn = new NativeArray<Vector3>(2, Allocator.TempJob);
           dataPosOut = new NativeArray<Vector3>(1, Allocator.TempJob);
        }

        private void OnDisable()
        {
            dataPosIn.Dispose();
            dataPosOut.Dispose();
        }
    }
}
