using UnityEngine;

namespace ClassicRealisation.Tadpoles
{
    /// <summary>
    /// Классическое движение для сверки производительности
    /// </summary>
    public class TadPole : MonoBehaviour
    {
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
            cashed.position += cashed.forward * Time.fixedDeltaTime * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            cashed.LookAt(cashed.position - cashed.forward);
        }
    }
}