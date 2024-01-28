using DG.Tweening;
using UnityEngine;

namespace BuilderStory
{
    public class StructureTip : MonoBehaviour
    {
        private const float YOriginPosition = 3f;
        private const float YOffsetPosition = 2.5f;
        private const float FloatingDuration = 1f;
        private const int Loops = -1;

        private Tweener _jumping;

        private void OnDisable()
        {
            _jumping?.Kill();
        }

        public void Init()
        {
            transform.localPosition = new Vector3(0f, YOriginPosition, 0f);

            _jumping = transform
                .DOLocalMoveY(YOffsetPosition, FloatingDuration)
                .SetLoops(Loops, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }
    }
}
