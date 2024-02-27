using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UIElements;

namespace JobRealisationV2
{
    [BurstCompile]
    public struct MovingDataJob : IJobParallelFor
    {
        public NativeArray<PosData> dataPosIn; 
        public NativeArray<PosData> dataPosOut;
        public float maxDistance;
        public float deltaTime;

        public void Execute(int index)
        {
            Vector3 newPosition = dataPosIn[index].position + dataPosIn[index].speedVector * deltaTime;

            if (CheckOutOfRange(newPosition))
                dataPosOut[index] = new PosData() { position = dataPosIn[index].position - dataPosIn[index].speedVector * deltaTime, speedVector = - dataPosIn[index].speedVector };
            else
                dataPosOut[index] = new PosData() { position = newPosition, speedVector = dataPosIn[index].speedVector };
        }

        /// <summary>
        /// Проверка на выход за пределы области
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool CheckOutOfRange(Vector3 pos)
        {
            if (Mathf.Abs(pos.x) >= maxDistance)
                return true;

            if (Mathf.Abs(pos.y) >= maxDistance)
                return true;

            if (Mathf.Abs(pos.z) >= maxDistance)
                return true;

            return false;
        }
    }
}
