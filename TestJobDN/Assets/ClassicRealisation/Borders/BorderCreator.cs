using UnityEngine;

namespace ClassicRealisation.Borders
{
    public class BorderCreator : MonoBehaviour
    {
        [SerializeField] private Transform borderPrefab;

        private Transform[] bordersCubes;

        public void CreateBorders(float maxDistance)
        {
            bordersCubes = new Transform[6];
            for (int i = 0; i < 6; i++)
                bordersCubes[i] = Instantiate(borderPrefab, transform);

            bordersCubes[0].position = Vector3.right * maxDistance;
            bordersCubes[0].localScale = new Vector3(1.0f, maxDistance * 2.0f, maxDistance * 2.0f);

            bordersCubes[1].position = -Vector3.right * maxDistance;
            bordersCubes[1].localScale = new Vector3(1.0f, maxDistance * 2.0f, maxDistance * 2.0f);

            bordersCubes[2].position = Vector3.forward * maxDistance;
            bordersCubes[2].localScale = new Vector3(maxDistance * 2.0f, maxDistance * 2.0f, 1.0f);

            bordersCubes[3].position = -Vector3.forward * maxDistance;
            bordersCubes[3].localScale = new Vector3(maxDistance * 2.0f, maxDistance * 2.0f, 1.0f);

            bordersCubes[4].position = Vector3.up * maxDistance;
            bordersCubes[4].localScale = new Vector3(maxDistance * 2.0f, 1.0f, maxDistance * 2.0f);

            bordersCubes[5].position = -Vector3.up * maxDistance;
            bordersCubes[5].localScale = new Vector3(maxDistance * 2.0f, 1.0f, maxDistance * 2.0f);
        }
    }
}