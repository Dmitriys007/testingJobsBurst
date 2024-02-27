using JobRealisationV2;
using Unity.Collections;
using UnityEngine;

namespace Assets.JobRealisationV2.TadPoles
{
    internal class TadPoleJobV2 : MonoBehaviour
    {
        private NativeArray<PosData> dataPosIn;
        private NativeArray<PosData> dataPosOut;
        [SerializeField] private int index;

        /// <summary>
        /// Прямая ссылка на свой Transform
        /// </summary>
        private Transform cashed;

        public void Init(ref NativeArray<PosData> dataPosIn, ref NativeArray<PosData> dataPosOut, int index)
        {
            this.dataPosIn = dataPosIn;
            this.dataPosOut = dataPosOut;
            this.index = index;

            cashed = transform;
            cashed.LookAt(cashed.position + dataPosOut[index].speedVector);
        }

        private void FixedUpdate()
        {
            cashed.position = dataPosOut[index].position;            
            cashed.LookAt(cashed.position + dataPosOut[index].speedVector);
            dataPosIn[index] = dataPosOut[index];
        }

        private void OnCollisionEnter(Collision collision)
        {
            dataPosIn[index] = new PosData { position = cashed.position, speedVector = collision.contacts[0].normal * dataPosOut[index].speedVector.magnitude };
        }
    }
}
