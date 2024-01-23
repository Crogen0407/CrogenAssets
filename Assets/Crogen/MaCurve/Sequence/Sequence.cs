using System;
using System.Collections.Generic;

namespace Crogen.MaCurve.Sequence
{
    public static class Sequence
    {
        public static MaCurveCore Append(this MaCurveCore target, MaCurveCore lateMaCurveCore)
        {
            target.OnDie += () =>
            {
                
            };
            return lateMaCurveCore;
        }
        
        public static MaCurveCore AppendCallback(this MaCurveCore target, MaCurveCallback maCurveCallback)
        {
            target.OnDie += () =>
            {
                maCurveCallback?.Invoke();
            };
            return target;
        }
    }
}