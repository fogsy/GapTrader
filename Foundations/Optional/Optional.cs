﻿using System;

namespace Foundations.Optional
{
    public abstract class Optional<T>
    {
        public abstract Optional<T> IfExistsThen(Action<T> action);

        public abstract void IfEmpty(Action whenNone);
    }
}
