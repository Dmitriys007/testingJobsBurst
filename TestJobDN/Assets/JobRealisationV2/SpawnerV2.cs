using Assets.JobRealisationV2.TadPoles;
using ClassicRealisation.Borders;
using JobRealisationV2;
using System.Collections;
using Unity.Collections;
using UnityEngine;

namespace Assets.JobRealisationV2
{
    public class SpawnerV2 : MonoBehaviour
    {
        [SerializeField] private JobUpdater jobUpdaterPrefab;

        /// <summary>
        /// Какие объекты будем спавнить
        /// </summary>
        [SerializeField] private TadPoleJobV2 spawnPrefab;

        /// <summary>
        /// Сколько экземпляров спавним всего
        /// </summary>
        [SerializeField] private int maxSpawned = 1500;

        /// <summary>
        /// Максимум спавнов за кадр
        /// </summary>
        [SerializeField] private int spawnPerFrame = 20;

        /// <summary>
        /// Дистанция от центра до границы поля симмуляции
        /// </summary>
        [SerializeField] private float borderDistance = 45.0f;

        /// <summary>
        /// Максимальная скорость объектов
        /// </summary>
        [SerializeField] private float maxSpeed;

        /// <summary>
        /// Сколько уже заспавнено
        /// </summary>
        private int curentSpawned = 0;

        private IEnumerator Start()
        {
            JobUpdater updater = Instantiate(jobUpdaterPrefab);
            updater.Init(maxSpawned, borderDistance, maxSpeed);

            NativeArray<PosData> dataPosIn = updater.dataPosIn;
            NativeArray<PosData> dataPosOut = updater.dataPosOut;
            
            yield return null;

            while (curentSpawned < maxSpawned)
                for (int i = 0; i < spawnPerFrame; i++)
                    if (curentSpawned < maxSpawned)
                    {
                        Instantiate(spawnPrefab, dataPosIn[i].position, Quaternion.identity).Init(ref dataPosIn, ref dataPosOut, curentSpawned);
                        curentSpawned++;
                        yield return null;
                    }
                    else
                        break;

            Debug.Log($"Spawn complete: {curentSpawned}");
        }
    }
}
