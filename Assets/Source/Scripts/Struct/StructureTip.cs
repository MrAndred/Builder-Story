using DG.Tweening;
using UnityEngine;

namespace BuilderStory
{
    public class StructureTip : MonoBehaviour
    {
        private const float YOffsetPosition = 0.5f;
        private const float FloatingDuration = 1f;
        private const int Loops = -1;

        [SerializeField] private float YOriginPosition = 3f;

        private Tweener _jumping;

        private void OnDisable()
        {
            _jumping?.Kill();
        }

        public void Init()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, YOriginPosition, transform.localPosition.z);

            _jumping = transform
                .DOLocalMoveY(YOriginPosition - YOffsetPosition, FloatingDuration)
                .SetLoops(Loops, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }
    }
}
