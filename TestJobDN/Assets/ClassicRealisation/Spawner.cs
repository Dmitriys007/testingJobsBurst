using ClassicRealisation.Borders;
using UnityEngine;

namespace ClassicRealisation
{
    public class Spawner : MonoBehaviour
    {
        /// <summary>
        /// Какие объекты будем спавнить
        /// </summary>
        [SerializeField] private Transform spawnPrefab;

        /// <summary>
        /// Обозначает границы поля симмуляции, за них не должны объекты вылетать
        /// </summary>
        [SerializeField] private BorderCreator borderPrefab;

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
        /// Сколько уже заспавнено
        /// </summary>
        private int curentSpawned = 0;

        private void Start()
        {
            Instantiate(borderPrefab).CreateBorders(borderDistance + 1);
        }

        private void Update()
        {
            for (int i = 0; i < spawnPerFrame; i++)
                if (curentSpawned < maxSpawned)
                {
                    Instantiate(
                        spawnPrefab, 
                        new Vector3(Random.Range(-borderDistance, borderDistance), Random.Range(-borderDistance, borderDistance), Random.Range(-borderDistance, borderDistance)),
                        Quaternion.identity);
                    curentSpawned++;
                }
        }
    }
}