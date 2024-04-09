using DG.Tweening;
using UnityEngine;

namespace BuilderStory.Loader
{
    public class LoadRenderer : MonoBehaviour
    {
        private const float RotationSpeed = 1f;
        private const int RotateLoops = -1;
        private readonly Vector3 _rotation = new Vector3(0, 0, -360);

        private Tweener _tweener;

        private void OnEnable()
        {
            _tweener = transform
                .DORotate(_rotation, RotationSpeed, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(RotateLoops, LoopType.Incremental);
        }

        private void OnDisable()
        {
            _tweener?.Kill();
        }
    }
}
