using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BuilderStory
{
    public class MaterialItem : MonoBehaviour
    {
        private const string CountFormat = "x{0}";

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        public void Init(Sprite icon, int count)
        {
            _icon.sprite = icon;
            _count.text = string.Format(CountFormat, count);
        }
    }
}
