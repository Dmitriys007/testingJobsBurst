using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JobRealisationV2
{
    public class JobUpdater : MonoBehaviour
    {
        /// <summary>
        /// позиции и вращение всех объектов
        /// </summary>
        public NativeArray<PosData> dataPosIn;
        public NativeArray<PosData> dataPosOut;

        private float maxDistance;

        public void Init(int countTadpoles, float maxDistance, float maxSpeed)
        {
            dataPosIn = new NativeArray<PosData>(countTadpoles, Allocator.Persistent);
            dataPosOut = new NativeArray<PosData>(countTadpoles, Allocator.Persistent);
            this.maxDistance = maxDistance;

            for (int i = 0; i < countTadpoles; i ++) // заполняем начальные данные случайными значениями
            {
                dataPosIn[i] = new PosData()
                {
                    position = new Vector3(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance)),
                    speedVector = new Vector3(Random.Range(0, 1.0f) - 0.5f, Random.Range(0, 1.0f) - 0.5f, Random.Range(0, 1.0f) - 0.5f).normalized * (Random.Range(0, maxSpeed) + 1)
                };
                if (dataPosIn[i].speedVector.magnitude < 0.1f)
                    Debug.Log("Opps");
            }
        }

        private void FixedUpdate()
        {
            MovingDataJob handler = new MovingDataJob() 
            { 
                dataPosIn = dataPosIn, 
                dataPosOut = dataPosOut, 
                maxDistance = maxDistance, 
                deltaTime = Time.fixedDeltaTime 
            };
            handler.Schedule(dataPosOut.Length, 24).Complete();
        }

        private void OnDisable()
        {
            dataPosIn.Dispose();
            dataPosOut.Dispose();
        }
    }
}
