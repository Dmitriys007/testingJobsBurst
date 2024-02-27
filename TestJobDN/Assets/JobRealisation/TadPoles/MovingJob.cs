using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace JobRealisation.TadPoles
{
    [BurstCompile]
    public struct MovingJob : IJob
    {
        public NativeArray<Vector3> inputData;
        public NativeArray<Vector3> outputData;

        public void Execute()
        {
            outputData[0] = inputData[0] + inputData[1];
        }
    }
}
