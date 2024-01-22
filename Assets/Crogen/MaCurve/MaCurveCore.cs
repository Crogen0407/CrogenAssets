namespace Crogen.MaCurve
{
    public abstract class MaCurveCore
    {
        private bool _isActive;
        internal bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                if (value == false)
                {
                    MaCurveManager.activeMaCurves.Remove(this);
                    OnDie?.Invoke();
                }
            }
        }

        internal int TargetTransformID;

        internal bool Forcedinitializable;
        
        internal MaCurveCallback OnDie;
        
        public abstract void Move();
    }
}