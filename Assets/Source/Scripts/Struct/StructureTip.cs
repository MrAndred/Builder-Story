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

        private readonly Vector3 RotatePosition = new Vector3(90f, 0f, 360f);

        private Tweener _jumping;
        private Tweener _rotating;

        private void OnDisable()
        {
            _jumping?.Kill();
            _rotating?.Kill();
        }

        public void Init()
        {
            transform.localPosition = new Vector3(0f, YOriginPosition, 0f);

            _rotating = transform
                .DORotate(RotatePosition, FloatingDuration, RotateMode.FastBeyond360)
                .SetLoops(Loops, LoopType.Incremental)
                .SetEase(Ease.Linear);

            _jumping = transform
                .DOLocalMoveY(YOffsetPosition, FloatingDuration)
                .SetLoops(Loops, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }
    }
}
