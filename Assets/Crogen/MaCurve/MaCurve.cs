namespace Crogen.MaCurve
{
    public abstract class MaCurve<T0, T1>
    {
        internal T0 target;
        internal T1 startPoint;
        internal T1 endPoint;

        internal bool isActive;
        
        internal float startTime;
        internal float endTime;
        
        internal float duration;
        internal float currentTime;

        internal EasingType easingType;

        internal MaCurveCallback onDie;
        public abstract void Move();
    }
}