using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class Dialog : MonoBehaviour
    {
        protected void Hide() => Destroy(gameObject);
    }
}