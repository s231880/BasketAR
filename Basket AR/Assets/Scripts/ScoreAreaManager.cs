using UnityEngine;

namespace BBAR
{
    public class ScoreAreaManager : MonoBehaviour
    {
        private Vector3 m_StartCollionPosition;
        private Vector3 m_FinalCollisionPosition;

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log("Collision Entered with: " + other.gameObject.name);

            if (other.gameObject.name == "Ball" || other.gameObject.name == "FlameBall")
            {
                m_StartCollionPosition = other.gameObject.transform.position;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == "Ball" || other.gameObject.name == "FlameBall")
            {
                m_FinalCollisionPosition = other.gameObject.transform.position;
                CheckIfTheUserScoredAPoint();
            }
            m_StartCollionPosition = m_FinalCollisionPosition = Vector3.zero;
        }

        private void CheckIfTheUserScoredAPoint()
        {
            if (m_FinalCollisionPosition.y < m_StartCollionPosition.y)
                GameManager.Instance.UpdateScored();
        }
    }
}